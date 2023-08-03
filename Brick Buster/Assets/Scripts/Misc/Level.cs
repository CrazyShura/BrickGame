using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
	#region Fields
	[SerializeField]
	BrickType[] layout = new BrickType[144];
	#endregion

	#region Properties
	public BrickType[] Layout { get => layout;}
	#endregion

	#region Methods
	public void ChangeValueAt(int index, BrickType type)
	{
		layout[index] = type;
	}
	#endregion
}

