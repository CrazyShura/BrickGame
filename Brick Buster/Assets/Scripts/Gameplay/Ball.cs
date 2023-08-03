using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
	#region Fields
	[SerializeField]
	GameObject explosionEffect;
	[SerializeField]
	float radiusOfExplosion;
	[SerializeField]
	Color normalColor, exposiveColor;

	Rigidbody2D rgbd;
	SpriteRenderer grfxRenderer;
	UnityEvent<Ball> ballLost = new UnityEvent<Ball>();

	Vector2 lastFrameVelocity;
	bool explosionActivated = false;
	#endregion

	#region Properties
	public Vector2 LastFrameVelocity { get => lastFrameVelocity; }
	public bool ExplosionActivated
	{
		get => explosionActivated;
		set
		{
			if (value)
			{
				grfxRenderer.color = exposiveColor;
				explosionActivated = true;
			}
			else
			{
				grfxRenderer.color = normalColor;
				explosionActivated = false;
			}
		}
	}
	#endregion

	#region Methods
	private void Awake()
	{
		rgbd = GetComponent<Rigidbody2D>();
		grfxRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
		grfxRenderer.color = normalColor;
		GM.Instance.AddBall(this);
		EventManager.BallLost.AddInvoker(ballLost);
	}
	private void FixedUpdate()
	{
		if (rgbd.velocity.magnitude > GM.Instance.BallSpeed)
		{
			rgbd.velocity = rgbd.velocity.normalized * GM.Instance.BallSpeed;
		}

		lastFrameVelocity = rgbd.velocity;
	}
	/// <summary>
	/// Sets initial move vector for a ball;
	/// </summary>
	/// <param name="where">Initial move vector</param>
	public void Go(Vector2 where)
	{
		rgbd.velocity = where.normalized * GM.Instance.BallSpeed; //This might give you an error in editor (Rigidbody is null). Becaouse this method is called inside async mathod, and if you can stop editor before the await is complete scene will reset but call will still be made. Its just a editor thing though and suld not couse any trouble.
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			rgbd.velocity = GM.Instance.Player.CalculateBounceVector(collision.GetContact(0).point) * GM.Instance.BallSpeed;
		}
		else
		{
			if (ExplosionActivated && collision.transform.CompareTag("Brick"))
			{
				GameObject _explosionEffect = Instantiate(explosionEffect);
				_explosionEffect.transform.position = transform.position;
				_explosionEffect.transform.localScale = Vector3.one * radiusOfExplosion * 2;
				Destroy(_explosionEffect, .3f);
				Collider2D[] _hits = Physics2D.OverlapCircleAll(transform.position, radiusOfExplosion);
				foreach (Collider2D _hit in _hits)
				{
					if (_hit.gameObject.CompareTag("Brick"))
					{
						_hit.GetComponent<Brick>().ReciveDamage();
					}
				}
				ExplosionActivated = false;
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("DeathLine"))
		{
			ballLost.Invoke(this);
			Destroy(gameObject);
		}
	}
	#endregion
}

