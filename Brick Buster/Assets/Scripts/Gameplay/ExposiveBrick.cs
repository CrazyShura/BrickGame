using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposiveBrick : Brick
{
	#region Fields
	[SerializeField]
	float radiusOfExplosion = 1f;
	[SerializeField]
	GameObject explosionEffect;
	#endregion

	#region Properties

	#endregion

	#region Methods
	protected override void Poof()
	{
		destroyed.Invoke(pointsOnDestroy);
		GameObject _explosionEffect = Instantiate(explosionEffect);
		_explosionEffect.transform.position = transform.position;
		_explosionEffect.transform.localScale = Vector3.one * radiusOfExplosion * 2;
		Destroy(_explosionEffect,.3f);
		Collider2D[] _hits = Physics2D.OverlapCircleAll(transform.position, radiusOfExplosion);
		foreach(Collider2D _hit in _hits)
		{
			if(_hit.gameObject.CompareTag("Brick"))
			{
				_hit.GetComponent<Brick>().ReciveDamage();
			}
		}
		Destroy(gameObject);
	}
	#endregion
}

