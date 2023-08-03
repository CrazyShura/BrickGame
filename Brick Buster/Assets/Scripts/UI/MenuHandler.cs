using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
	#region Fields
	[SerializeField]
	TMP_Text highScore;
	#endregion

	#region Properties

	#endregion

	#region Methods
	private void Start()
	{
		highScore.text = "Your HighScore is: " + PublicStuff.HighScore;
	}
	public void GoToGameplay()
	{
		TransitionManager.Instance.TransitionToGameplay();
	}
	public void GoToMenu()
	{
		TransitionManager.Instance.TransitionToMenu();
	}
	public void Exit()
	{
		Application.Quit();
	}
	#endregion
}

