using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
	#region Fields
	[SerializeField]
	float fallSpeed = 2f;
	#endregion

	#region Properties

	#endregion

	#region Methods
	private void Start()
	{
		Rigidbody2D _rgbd= GetComponent<Rigidbody2D>();
		_rgbd.velocity = Vector3.down * fallSpeed;
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			GM.Instance.PowerUpActivation();
			Destroy(gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("DeathLine"))
		{
			Destroy(gameObject);
		}
	}
	#endregion
}

