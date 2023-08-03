using TMPro;
using UnityEngine;

public class GameplayHandler : MonoBehaviour
{
	#region Fields
	[SerializeField]
	GameObject pauseMenu, gameOver;
	[SerializeField]
	TMP_Text scoreGameOver, scoreRuntime, lives;
	#endregion

	#region Properties

	#endregion

	#region Methods
	private void Awake()
	{
		EventManager.GameOver.AddListener(GameOver);
		EventManager.PointsRecived.AddListener(UpdateScore);
		EventManager.LiveLost.AddListener(UpdateLives);
	}
	private void Start()
	{
		UpdateLives();
	}
	public void GoToMenu()
	{
		TransitionManager.Instance.TransitionToMenu();
		Time.timeScale = 1f;
	}
	public void Pause()
	{
		Time.timeScale = 0f;
		pauseMenu.SetActive(true);
	}
	public void Resume()
	{
		Time.timeScale = 1f;
		pauseMenu.SetActive(false);
	}
	public void GameOver()
	{
		Time.timeScale = 0f;
		scoreGameOver.text = "Your score: " + GM.Instance.Score.ToString();
		gameOver.SetActive(true);
	}
	public void UpdateScore(int unused)
	{
		scoreRuntime.text = GM.Instance.Score.ToString();
	}
	public void UpdateLives()
	{
		lives.text = GM.Instance.Lives.ToString();
	}
	#endregion
}