using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public enum AsteroidType {
		small,
		medium,
		large
	}
	public AsteroidType asteroidType;

//	public AudioClip audioClip;
//	public AudioSource audioSource;

	private float flashLengthSeconds = 1.0f;

	public int pointValue = 0;

	public float minTorque = -10.0f;
	public float maxTorque = 10.0f;

	public float minMagnitude = 25.0f;
	public float maxMagnitude = 50.0f;

	private int countChildren;

	private int minChildren = 2;
	private int maxChildren = 5;

	private AsteroidControllerScript asteroidController;
	private GameController gameController;

	private Rigidbody2D rigidbody2D;

	public GameObject Explosion;
	public GameObject childAsteroidPrefab;
	public GameObject shrapnelPrefab;
	public GameObject lootPrefab;

	public float inheritedMagnitude = 0.0f;

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void Awake(){
		//get a ref to our rigidbody2D
		rigidbody2D = GetComponent<Rigidbody2D>();

		//get a ref to the gameController so we can talk to it
		gameController = (GameController)FindObjectOfType(typeof(GameController));

		//get a ref to the asteroidController so we can talk to it
		asteroidController = (AsteroidControllerScript)FindObjectOfType(typeof(AsteroidControllerScript));

		//tell the asteroidController to increment his count of all asteroids
		asteroidController.asteroidCount++;

		//set a random number of children this asteroid will birth if killed
		countChildren = Random.Range(minChildren, maxChildren);





	}

	void OnDestroy(){
		asteroidController.asteroidCount--;
//		Debug.Log("Asteroid OnDestroy Count: " + asteroidController.asteroidCount);
	}

	void Start() {

		Vector2 parentVelocity = rigidbody2D.velocity;
		float parentMagnitude = parentVelocity.magnitude;
		float magnitude = Random.Range(minMagnitude, maxMagnitude);

//		Debug.Log("Asteroid start -- inheritedMagnitude: " + inheritedMagnitude);
//		Debug.Log("Asteroid start -- randomMagnitude: " + magnitude);

		if (inheritedMagnitude > magnitude) {
//			Debug.Log("Asteroid start -- inheritedMagnitude greater than randomMagnitude");
//			magnitude = inheritedMagnitude;
		} else {
//			Debug.Log("Asteroid start -- inheritedMagnitude is less than randomMagnitude");
//			magnitude = Random.Range(minForce, maxForce);
		}

//		float magnitude = Random.Range(minForce, maxForce);
		float torque = Random.Range(minTorque, maxTorque);

		float x = Random.Range(-1.0f, 1.0f);
		float y = Random.Range(-1.0f, 1.0f);

		rigidbody2D.AddTorque(torque);

		Vector2 randomVector = new Vector2 (x, y);

//		Debug.Log("Asteroid start -- magnitude:" + magnitude);

		rigidbody2D.AddForce(randomVector * magnitude);


	}
	
	void Update() {

	}
		
	void FixedUpdate() {

	}
		
	void OnCollisionEnter2D(Collision2D collision) {
		GameObject collisionGameObject = collision.gameObject;
		string tag = collisionGameObject.tag;
		Debug.Log(string.Format("{0} OnCollisionEnter2D: {1}", GetType(), tag));

		if ("tag_asteroid_large" == tag) {
			collisionAsteroid ();
			rollForShrapnel ();
//			rollForLoot ();

		} else if ("tag_asteroid_medium" == tag) {
			collisionAsteroid ();
			rollForShrapnel ();
//			rollForLoot ();

		} else if ("tag_asteroid_small" == tag) {
			collisionAsteroid ();
			rollForShrapnel ();
//			rollForLoot ();

		} else if ("tag_shrapnel" == tag) {
			collisionShrapnel ();
		} else if ("tag_ship" == tag) {
			collisionShip ();
		} else if ("tag_loot" == tag) {
			collisionLoot ();
		} else if ("tag_bullet_ship" == tag) {
			//			collisionBulletShip ();
//			rollForLoot ();

		} else {
			Debug.Log(string.Format("*****{0} OnCollisionEnter2D collisionUnknown: {1}*****", GetType(), tag));
			collisionUnknown ();
		}
	}


	void OnTriggerEnter2D(Collider2D collider) {
		GameObject colliderGameObject = collider.gameObject;
		string tag = colliderGameObject.tag;
		Debug.Log(string.Format("{0} OnTriggerEnter2D: {1}", GetType(), tag));

		if("tag_bullet_ship" == tag) {
			Color flashColor = Color.red;
			StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));


			Explosion.GetComponent<Explosion>().asteroidType = asteroidType;
			Instantiate(Explosion, transform.position, new Quaternion());


			gameController.AddScore(pointValue);
//			gameController.AddScore(pointValue);
			rollForLoot ();

//			if (childAsteroidPrefab != null) {

				if (AsteroidType.small == asteroidType) {

				} else {
					BirthChildren ();
				}
//			}

			Destroy(gameObject);
			Destroy(colliderGameObject);




		} else if ("tag_ship" == tag) {
			Debug.Log("OnTriggerEnter2D Hit! tag_ship");
		} else if ("tag_asteroid_large" == tag) {
			Debug.Log("OnTriggerEnter2D Hit! tag_asteroid_large");
		} else if ("tag_asteroid_medium" == tag) {
			Debug.Log("OnTriggerEnter2D Hit! tag_asteroid_medium");
		} else if ("tag_asteroid_small" == tag) {
			Debug.Log("OnTriggerEnter2D Hit! tag_asteroid_small");
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////



	void rollForLoot() {
		float chance = 0.2f;
		float diceRoll = Random.Range(0.0f, 1.0f);
		if (diceRoll < chance) {
			GameObject shrapnel = Instantiate (lootPrefab, transform.position, new Quaternion()) as GameObject;
		}
	}
	void rollForShrapnel() {
		float chance = 0.2f;
		float diceRoll = Random.Range(0.0f, 1.0f);
		if (diceRoll < chance) {
			GameObject shrapnel = Instantiate (shrapnelPrefab, transform.position, new Quaternion()) as GameObject;
		}
	}

	void collisionUnknown() {
		Color flashColor = Color.blue;
		StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));
	}

	void collisionLoot() {
		Color flashColor = Color.blue;
		StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));

		AudioSource audioSource = GetComponent<AudioSource>();

//		if (audioSource.isPlaying) {
//			audioSource.Stop ();
//			audioSource.Play ();
//		} else {
//			audioSource.Play ();
//		}

	}

	void collisionAsteroid() {
		Color flashColor = Color.cyan;
		StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));

		AudioSource audioSource = GetComponent<AudioSource>();

		if (audioSource.isPlaying) {
			audioSource.Stop ();
			audioSource.Play ();
		} else {
			audioSource.Play ();
		}

//		rollForShrapnel ();
//		rollForLoot ();

//		float chanceForShrapnel = 0.8f;
//		float shrapnelDiceRoll = Random.Range(0.0f, 1.0f);
//		if (shrapnelDiceRoll > chanceForShrapnel) {
//			GameObject shrapnel = Instantiate (shrapnelPrefab, transform.position, new Quaternion()) as GameObject;
//		}
	}

	void collisionShrapnel() {
		Color flashColor = Color.cyan;
		StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));

		//		AudioSource audioSource = GetComponent<AudioSource>();
		//
		//		if (audioSource.isPlaying) {
		//			audioSource.Stop ();
		//			audioSource.Play ();
		//		} else {
		//			audioSource.Play ();
		//		}
	}

	void collisionShip() {
		Color flashColor = Color.red;
		StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));

		//		AudioSource audioSource = GetComponent<AudioSource>();
		//
		//		if (audioSource.isPlaying) {
		//			audioSource.Stop ();
		//			audioSource.Play ();
		//		} else {
		//			audioSource.Play ();
		//		}
	}
	void collisionBulletShip() {
//		Color flashColor = Color.red;
//		StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));
//
//		gameController.AddScore(asteroidPoints);
//		//			Debug.Log("Asteroid OnTriggerEnter2D tag_bullet_ship score: " + gameController.score);
//
//		Explosion.GetComponent<Explosion>().asteroidType = asteroidType;
//		Instantiate(Explosion, transform.position, new Quaternion());
//
//
//		if (childAsteroidPrefab != null) {
//			//				Child.GetComponent<Asteroid>().asteroidType = "large";
//			//				AsteroidType selfType = asteroidType;
//
//			if (AsteroidType.small == asteroidType) {
//				//im a small asteroid. i dont have children
//
//			} else {
//				BirthChildren ();
//			}
//		}
//
//		Destroy(gameObject);
//		Destroy(GetComponent<Collider>().gameObject);

	}

	void BirthChildren() {
		Vector2 parentVelocity = rigidbody2D.velocity;
		float parentMagnitude = parentVelocity.magnitude;
		Debug.Log("parentMagnitude magnitude: " + parentMagnitude);

		for (int i = 0; i < countChildren; i++) {
			GameObject childAsteroid = Instantiate (childAsteroidPrefab, transform.position, new Quaternion()) as GameObject;
//			childAsteroid.GetComponent<Asteroid> ().inheritedMagnitude = parentMagnitude;
		}
	}

	IEnumerator FlashMaterial(Color flashColor, float waitTime) {
		Renderer renderer = GetComponent<Renderer>();
//		Color currentColor = renderer.material.color;
		Color grayColor = new Color(0.9F, 0.9F, 0.9F, 1.0F);
		//		Color magentaColor = Color.magenta;

		renderer.material.color = flashColor;
		yield return new WaitForSeconds(waitTime);
		renderer.material.color = grayColor;
//		renderer.material.color = currentColor;
	}
}
