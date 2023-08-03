using UnityEngine;

public static class PublicStuff
{
	#region Fields
	static int highScore = -1;
	#endregion

	#region Properties
	/// <summary>
	/// High score of the player. Only overwrites if value passed is greated then before;
	/// </summary>
	public static int HighScore
	{
		get
		{
			if (highScore < 0)
			{
				if (PlayerPrefs.HasKey("HighScore"))
				{
					highScore = PlayerPrefs.GetInt("HighScore");
				}
				else
				{
					highScore = 0;
				}
			}
			return highScore;
		}
		set
		{
			if(value > HighScore)
			{
				highScore = value;
				PlayerPrefs.SetInt("HighScore", value);
			}
		}
	}
	#endregion

	#region Methods

	#endregion
}

public enum BrickType { None, Common, Hard, Hardest, Explosive, PowerUp }