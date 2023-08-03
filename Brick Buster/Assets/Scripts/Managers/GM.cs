using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manager for gameplay. Keep trak of balls, score, lives and how game is going in general.
/// Sigleton.
/// </summary>
public class GM : MonoBehaviour
{
	#region Fields
	public static GM Instance;

	[SerializeField]
	Ball ballPrefab;
	[SerializeField]
	List<Brick> bricks; //Suld follow the order of BrickType enum
	[SerializeField]
	List<Level> levelList;

	EdgeCollider2D stageCollider;
	GameObject levelParent;
	List<Ball> trackedBalls = new List<Ball>();
	int lives = 3;
	int score = 0;
	int levelNum = 0;
	UnityEvent gameOverEvent = new UnityEvent();
	UnityEvent liveLost = new UnityEvent();

	PaddleController player;
	#endregion

	#region Properties
	[Range(1f, 10f)]
	public float BallSpeed = 2f;
	[Range(1f, 10f)]
	public float PaddelSpeed = 3f;
	public PaddleController Player
	{
		get { return player; }
		set
		{
			if (player == null)
			{
				player = value;
				player.Speed = PaddelSpeed;
			}
			else
			{
				Debug.LogError("Trying to assign more then one paddle. No change been made");
			}
		}
	}

	public int Score { get => score; }
	public int Lives { get => lives; }
	#endregion

	#region Methods
	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError("Trying to make more then one Game Master (GM).");
			return;
		}
		Instance = this;

		stageCollider = GetComponent<EdgeCollider2D>();
		EventManager.BallLost.AddListener(AnotherOneBitesTheDust);
		EventManager.PointsRecived.AddListener(WeGettingPaid);
		EventManager.LiveLost.AddInvoker(liveLost);
		EventManager.GameOver.AddInvoker(gameOverEvent);
		levelParent = new GameObject();
	}
	private void Start()
	{
		Camera _mainCamera = Camera.main;

		//Initialize collider for a stage
		float _screenHorizontalSizeInUnits = _mainCamera.orthographicSize * Screen.width / Screen.height;
		float _screenVerticalSizeInUnits = _mainCamera.orthographicSize;
		Vector2[] _colliderVerticies = new Vector2[5];
		_colliderVerticies[0] = new Vector2(-_screenHorizontalSizeInUnits, -_screenVerticalSizeInUnits);
		_colliderVerticies[1] = new Vector2(-_screenHorizontalSizeInUnits, _screenVerticalSizeInUnits);
		_colliderVerticies[2] = new Vector2(_screenHorizontalSizeInUnits, _screenVerticalSizeInUnits);
		_colliderVerticies[3] = new Vector2(_screenHorizontalSizeInUnits, -_screenVerticalSizeInUnits);
		_colliderVerticies[4] = new Vector2(-_screenHorizontalSizeInUnits, -_screenVerticalSizeInUnits);
		stageCollider.points = _colliderVerticies;

		LevelStart();
	}
	void LevelStart()
	{
		BuildLevel(levelList[levelNum]);
		SpawnBall();
	}
	/// <summary>
	/// Reads a level scriptable object and builds a level.
	/// </summary>
	/// <param name="level"></param>
	void BuildLevel(Level level)
	{
		for (int _x = 0; _x < 16; _x++)
		{
			for (int _y = 0; _y < 9; _y++)
			{
				Brick _brickToSpawn = null;
				switch (level.Layout[_y + _x * 9])
				{
					case BrickType.None:
						break;
					case BrickType.Common:
						_brickToSpawn = bricks[0];
						break;
					case BrickType.Hard:
						_brickToSpawn = bricks[1];
						break;
					case BrickType.Hardest:
						_brickToSpawn = bricks[2];
						break;
					case BrickType.Explosive:
						_brickToSpawn = bricks[3];
						break;
					case BrickType.PowerUp:
						_brickToSpawn = bricks[4];
						break;
				}
				if (_brickToSpawn != null)
				{
					_brickToSpawn = Instantiate(_brickToSpawn, levelParent.transform);
					Vector3 _position = new Vector3(-2f + _y * .5f, 4 - _x * .25f);
					_brickToSpawn.transform.position = _position;
				}
			}
		}
	}
	/// <summary>
	/// Spawns a ball wiht a 1.5 sec delay before it starts moving;
	/// </summary>
	async void SpawnBall()
	{
		Ball _ball = Instantiate(ballPrefab);
		_ball.transform.position = Vector3.down * 2;
		await Task.Delay(1500);
		_ball.Go(Vector2.up + Vector2.right * Random.Range(-.5f, .5f));
	}
	public void AddBall(Ball ball)
	{
		trackedBalls.Add(ball);
	}
	/// <summary>
	/// Removes ball from tracking list.
	/// </summary>
	void AnotherOneBitesTheDust(Ball ball)
	{
		trackedBalls.Remove(ball);
		if (trackedBalls.Count == 0)
		{
			if (lives > 0)
			{
				lives--;
				liveLost.Invoke();
				SpawnBall();
			}
			else
			{
				PublicStuff.HighScore = score;
				gameOverEvent.Invoke();
			}
		}
	}
	/// <summary>
	/// Adds amount of points to score.
	/// </summary>
	void WeGettingPaid(int amount)
	{
		score += amount;
		if (levelParent.transform.childCount == 1) // HACK Checks if there is only one brick left on the field and not 0, since event is triggered before the brick is broken. Suld be all good but can lead to wierdness.
		{
			foreach (Ball _ball in trackedBalls)
			{
				Destroy(_ball.gameObject);
			}
			trackedBalls.Clear();
			if (levelNum == levelList.Count - 1)
			{
				levelNum = 0;
			}
			else
			{
				levelNum++;
			}
			LevelStart();
		}
	}
	public void PowerUpActivation()
	{
		int _temp = Random.Range(0, 2);
		switch (_temp)
		{
			case 0: // Multiball powerup;
				{
					Ball[] _oldBalls = trackedBalls.ToArray();
					foreach (Ball _ball in _oldBalls)
					{
						Ball _newBall = Instantiate(ballPrefab);
						_newBall.transform.position = _ball.transform.position;
						if (_ball.LastFrameVelocity.magnitude == 0)
						{
							_newBall.Go(Vector2.up + Vector2.right * Random.Range(-.5f, .5f));
						}
						else
						{
							_newBall.Go(_ball.LastFrameVelocity * -1);
						}
					}
					break;
				}
			case 1: // Exposive ball powerup
				{
					foreach (Ball _ball in trackedBalls)
					{
						_ball.ExplosionActivated = true;
					}
					break;
				}
			default:
				break;
		}
	}
	#endregion
}

