using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Methods
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		for (int _x = 0; _x < 16; _x++)
		{
			GUILayout.BeginHorizontal();
			for (int _y = 0; _y < 9; _y++)
			{
				string _temp = "";
				switch (((Level)target).Layout[_y + _x * 9])
				{
					case BrickType.None:
						_temp= "0";
						break;
					case BrickType.Common:
						_temp = "1";	
					break;
					case BrickType.Hard:
						_temp = "2";
						break;
					case BrickType.Hardest:
						_temp = "3";
						break;
					case BrickType.Explosive:
						_temp = "4";
						break;
					case BrickType.PowerUp:
						_temp = "5";
						break;
				}
				if (GUILayout.Button(_temp))
				{
					BrickType _type = ((Level)target).Layout[_y + _x * 9];
					if(_type == BrickType.PowerUp)
					{
						((Level)target).ChangeValueAt(_y + _x * 9, 0);
					}
					else
					{
						((Level)target).ChangeValueAt(_y + _x * 9, _type + 1);
					}
				}
			}
			GUILayout.EndHorizontal();
		}
		if (GUILayout.Button("Save"))
		{
			EditorUtility.SetDirty(target);
			AssetDatabase.Refresh();
			AssetDatabase.SaveAssets();
		}
	}
	#endregion
}

