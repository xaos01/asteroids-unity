using UnityEngine;
using System.Collections;

public class AsteroidControllerScript : MonoBehaviour {

	public int asteroidCount;

//	public GameObject AsteroidLargePrefab;
//	public GameObject AsteroidPrefab;
	public Asteroid AsteroidLargePrefab;
	public Asteroid AsteroidMediumPrefab;
	public Asteroid AsteroidSmallPrefab;

	private Asteroid Asteroid;


	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void Awake(){
	}

//	public void CreateLargeAsteroids (int asteroidsCount) {
//		CreateAsteroids (asteroidsCount, Asteroid.AsteroidType.large);
//	}
//	public void CreateMediumAsteroids (int asteroidsCount) {
//		CreateAsteroids (asteroidsCount, Asteroid.AsteroidType.medium);
//	}
//	public void CreateSmallAsteroids (int asteroidsCount) {
//		CreateAsteroids (asteroidsCount, Asteroid.AsteroidType.small, );
//	}
		
	public void CreateAsteroids (int asteroidsCount, Asteroid.AsteroidType asteroidType, Vector3 position) {
//		Debug.Log("CreateAsteroids ofType: " + asteroidType);

		Asteroid preFab = AsteroidLargePrefab;




		if (Asteroid.AsteroidType.large == asteroidType) {
			preFab = AsteroidLargePrefab;
		} else if (Asteroid.AsteroidType.medium == asteroidType) {
			preFab = AsteroidMediumPrefab;
		} else if (Asteroid.AsteroidType.small == asteroidType) {
			preFab = AsteroidSmallPrefab;
		} else {
			Debug.Log("CreateAsteroids unknown type: " + asteroidType);
		}

		for (int i = 0; i < asteroidsCount; i++) {


//			Asteroid.GetComponent<Asteroid>().asteroidType = "large";

//			Instantiate (Asteroid, transform.position, new Quaternion());
//			Instantiate (Asteroid, transform.position, new Quaternion());

//			AsteroidPrefab.GetComponent<Asteroid>().asteroidType = asteroidType;
//			AsteroidLargePrefab.GetComponent<Asteroid>().asteroidType = asteroidType;
//			AsteroidPrefab.GetComponent<Asteroid>().asteroidType = Asteroid.AsteroidType.large;

			Instantiate (preFab, position, new Quaternion());
//			Instantiate (preFab, transform.position, new Quaternion());
		}

	}
	void Start () {

	}

	void Update () {
	
	}

	void FixedUpdate() {

	}
}
