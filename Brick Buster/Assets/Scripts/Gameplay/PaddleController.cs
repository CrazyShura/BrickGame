using UnityEngine;

public class PaddleController : MonoBehaviour
{
	#region Fields
	[SerializeField]
	Joystick joystick;

	float speed = 1f;
	float paddleSize = 1f;
	float positionLimit; // what absolute max x coordinate that paddle can have.

	float screenSizeInUnits;


	#endregion

	#region Properties
	public float Speed
	{
		get => speed;
		set 
		{
			speed = Mathf.Clamp(value, 1, 10);
		}
	}

	#endregion

	#region Methods
	private void Awake()
	{
		screenSizeInUnits = Camera.main.orthographicSize * Screen.width / Screen.height;
		RecalculatePositionLimit();
	}
	private void Start()
	{
		GM.Instance.Player = this;
	}
	private void Update()
	{
		if (joystick.Horizontal != 0)
		{
			Vector3 _desiredPosition = transform.position + Vector3.right * joystick.Horizontal * speed * Time.deltaTime;
			if (Mathf.Abs(_desiredPosition.x) <= positionLimit)
			{
				transform.position = _desiredPosition;
			}
		}
	}
	void RecalculatePositionLimit()
	{
		positionLimit = screenSizeInUnits - paddleSize / 2;
	}
	/// <summary>
	/// Calculates and returns vector at wich ball suld move after a collision.
	/// </summary>
	/// <param name="from">A point of contact wiht a paddle</param>
	public Vector2 CalculateBounceVector(Vector2 from)
	{
		Vector2 _res = from - (Vector2)transform.position;
		return _res.normalized;
	}
	#endregion
}

