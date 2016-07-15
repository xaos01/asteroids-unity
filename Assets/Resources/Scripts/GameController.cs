using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public int initialAsteroidCount = 1;
	public int lives = 3;
	public Text livesText;

	public int score = 0;
	public Text scoreText;

	public int level = 0;
	public Text levelText;

	public GameObject ShipPrefab;
	private AsteroidControllerScript asteroidControllerScript;
	public Asteroid.AsteroidType asteroidType;

	string status;


	Vector3 positionUpperLeft = new Vector3 (-6.0f,3.0f,1);
	Vector3 positionUpperRight = new Vector3 (6.0f,3.0f,1);
	Vector3 positionLowerLeft = new Vector3 (-6.0f,-3.0f,1);
	Vector3 positionLowerRight = new Vector3 (6.0f,-3.0f,1);
	Vector3 positionCenter = new Vector3 (0.0f,0.0f,1);

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////


	void Awake () {
		SetScoreText();
		SetLivesText();
		SetLevelText();

		asteroidControllerScript = (AsteroidControllerScript)FindObjectOfType(typeof(AsteroidControllerScript));
	}

	void Start () {
		Instantiate (ShipPrefab, positionCenter, new Quaternion());

		status = "level_init";
	}
	void Update () {

	}

	void FixedUpdate() {

		if ("level_init" == status) {
			LevelInit ();
			status = "level_start";

		} else if ("level_start" == status) {
			LevelStart ();
			status = "level_run";

		} else if ("level_run" == status) {
			int asteroidCount = asteroidControllerScript.asteroidCount;

			if (asteroidCount == 0) {
				status = "level_end";
//				Debug.Log ("GameController FixedUpdate ALL GONE: " + asteroidCount);
			}

		} else if ("level_end" == status) {
			LevelEnd();
			status = "level_init";

		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void LevelInit () {
		level++;
		SetLevelText();
	}

	void LevelStart () {

		if (1 == level) {
			//4
			asteroidControllerScript.CreateAsteroids(1, Asteroid.AsteroidType.large, positionUpperLeft);
			asteroidControllerScript.CreateAsteroids(1, Asteroid.AsteroidType.large, positionUpperRight);
			asteroidControllerScript.CreateAsteroids(1, Asteroid.AsteroidType.large, positionLowerLeft);
			asteroidControllerScript.CreateAsteroids(1, Asteroid.AsteroidType.large, positionLowerRight);
		} else if (2 == level) {
			//6
			asteroidControllerScript.CreateAsteroids(2, Asteroid.AsteroidType.large, positionUpperLeft);
			asteroidControllerScript.CreateAsteroids(2, Asteroid.AsteroidType.large, positionUpperRight);
			asteroidControllerScript.CreateAsteroids(1, Asteroid.AsteroidType.large, positionLowerLeft);
			asteroidControllerScript.CreateAsteroids(1, Asteroid.AsteroidType.large, positionLowerRight);
		} else if (3 == level) {
			//8
			asteroidControllerScript.CreateAsteroids(2, Asteroid.AsteroidType.large, positionUpperLeft);
			asteroidControllerScript.CreateAsteroids(2, Asteroid.AsteroidType.large, positionUpperRight);
			asteroidControllerScript.CreateAsteroids(2, Asteroid.AsteroidType.large, positionLowerLeft);
			asteroidControllerScript.CreateAsteroids(2, Asteroid.AsteroidType.large, positionLowerRight);
		} else if (4 == level) {
			//10
			asteroidControllerScript.CreateAsteroids(3, Asteroid.AsteroidType.large, positionUpperLeft);
			asteroidControllerScript.CreateAsteroids(3, Asteroid.AsteroidType.large, positionUpperRight);
			asteroidControllerScript.CreateAsteroids(2, Asteroid.AsteroidType.large, positionLowerLeft);
			asteroidControllerScript.CreateAsteroids(2, Asteroid.AsteroidType.large, positionLowerRight);
		} else {
			//11
			asteroidControllerScript.CreateAsteroids(3, Asteroid.AsteroidType.large, positionUpperLeft);
			asteroidControllerScript.CreateAsteroids(3, Asteroid.AsteroidType.large, positionUpperRight);
			asteroidControllerScript.CreateAsteroids(3, Asteroid.AsteroidType.large, positionLowerLeft);
			asteroidControllerScript.CreateAsteroids(2, Asteroid.AsteroidType.large, positionLowerRight);
		}
	}

	void LevelEnd () {
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void SetScoreText () {
//		Debug.Log("GameController score: " + score);
		scoreText.text = "score:" + score.ToString();
	}
	void SetLivesText () {
//		Debug.Log("GameController lives: " + lives);
		livesText.text = "lives:" + lives.ToString();
	}
	void SetLevelText () {
//		Debug.Log("GameController level: " + level);
		levelText.text = "level:" + level.ToString();
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	public void AddScore(int points) {
		Debug.Log("***** GameController AddScore points: " + points);
		score += points;
		SetScoreText();

		AudioSource audioSource = GetComponent<AudioSource>();

		// Create a list of parts.
		List<string> clipPaths = new List<string>();
		clipPaths.Add("Audio/money_one");
		clipPaths.Add("Audio/money_two");
		clipPaths.Add("Audio/money_three");

		string randomClipPath = clipPaths[Random.Range(0, clipPaths.Count)];
		AudioClip audioClip = Resources.Load(randomClipPath) as AudioClip;

		audioSource.clip = audioClip;

		if (audioSource.isPlaying) {
			audioSource.Stop ();
			audioSource.Play ();
		} else {
			audioSource.Play ();
		}

	}
}
