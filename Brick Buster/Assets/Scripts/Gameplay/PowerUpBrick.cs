using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBrick : Brick
{
	#region Fields
	[SerializeField]
	PickUp pickUpPrefab;
	#endregion

	#region Properties

	#endregion

	#region Methods
	protected override void Poof()
	{
		Instantiate(pickUpPrefab).transform.position = transform.position;
		base.Poof();
	}
	#endregion
}

