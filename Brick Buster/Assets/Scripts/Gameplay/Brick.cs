using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
	#region Fields
	[SerializeField]
	protected int pointsOnDestroy = 1;
	[SerializeField, Range(1, 3)]
	protected int hitsToDestroy = 1;
	[SerializeField]
	protected Color color = Color.white;

	protected UnityEvent<int> destroyed = new UnityEvent<int>();
	protected int health;
	#endregion

	#region Properties

	#endregion

	#region Methods
	protected virtual void Awake()
	{
		transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
		EventManager.PointsRecived.AddInvoker(destroyed);
		health = hitsToDestroy;
	}
	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		ReciveDamage();
	}
	public virtual void ReciveDamage()
	{
		if (health > 0)
		{
			transform.GetChild(health).gameObject.SetActive(true);
		}
		health--;
		if (health == 0)
		{
			Poof();
		}
	}
	/// <summary>
	/// Suld be called for distruction of a brick.
	/// </summary>
	protected virtual void Poof()
	{
		destroyed.Invoke(pointsOnDestroy);
		Destroy(gameObject);
	}
	#endregion
}

