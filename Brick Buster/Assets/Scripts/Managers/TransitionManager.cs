using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manger for scene loading, transition and etc...
/// Singleton , Not Destroyed On Load.
/// </summary>
public class TransitionManager : MonoBehaviour
{
	#region Fields

	public static TransitionManager Instance;

	[SerializeField, Min(.1f)]
	float fadeTime = 1f;
	[SerializeField]
	Animator animator;
	[SerializeField]
	TMP_Text loadingText;
	[SerializeField]
	Slider loadBar;
	Controls inputs;
	int currentScene = 0;
	bool inTransition = false;
	#endregion

	#region Properties

	#endregion

	#region Methods
	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(transform.parent.gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(transform.parent);
		inputs = new Controls();
		inputs.AppControls.Enable();
		inputs.AppControls.Back.performed += context => HandleInput();
		Screen.orientation = ScreenOrientation.Portrait;
	}
	void HandleInput()
	{
		switch (currentScene)
		{
			case 0:
				{
					Application.Quit();
					break;
				}
			case 1:
				{
					TransitionToMenu();
					break;
				}
			case 2:
				{
					TransitionToGameplay();
					break;
				}
			default:
				break;
		}
	}
	public async void TransitionToMenu()
	{
		if (!inTransition && currentScene != 0)
		{
			inTransition = true;
			loadingText.gameObject.SetActive(true);
			loadBar.gameObject.SetActive(true);
			animator.SetTrigger("StartTransition");

			await Task.Delay((int)(fadeTime * 1000));

			AsyncOperation loading = SceneManager.LoadSceneAsync(0);
			while (!loading.isDone)
			{
				loadBar.value = loading.progress / 1f;
				await Task.Yield();
			}
			loadBar.value = 1f;
			animator.SetTrigger("EndTransition");
			currentScene = 0;
			inTransition = false;

			await Task.Delay((int)(fadeTime * 1000));

			loadBar.value = 0;
			loadingText.gameObject.SetActive(false);
			loadBar.gameObject.SetActive(false);
		}
	}
	public async void TransitionToGameplay()
	{
		if (!inTransition && currentScene != 1)
		{
			inTransition = true;
			loadingText.gameObject.SetActive(true);
			loadBar.gameObject.SetActive(true);
			animator.SetTrigger("StartTransition");

			await Task.Delay((int)(fadeTime * 1000));

			AsyncOperation loading = SceneManager.LoadSceneAsync(1);
			while (!loading.isDone)
			{
				loadBar.value = loading.progress / 1f;
				await Task.Yield();
			}

			while (GM.Instance == null)
			{
				await Task.Yield();
			}
			loadBar.value = 1f;
			animator.SetTrigger("EndTransition");
			currentScene = 1;
			inTransition = false;

			await Task.Delay((int)(fadeTime * 1000));

			loadBar.value = 0;
			loadingText.gameObject.SetActive(false);
			loadBar.gameObject.SetActive(false);
		}
	}
	#endregion
}

