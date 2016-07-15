using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour {
	private float flashLengthSeconds = 1.0f;

	private Rigidbody2D rigidbody2D;
	private AudioSource audioSource;
	public GameObject Explosion;
	private GameController gameController;

	private float lifeInSeconds = 30.0f;

	public int pointValue = 50;

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void Awake(){
		//get a ref to our rigidbody2D
		rigidbody2D = GetComponent<Rigidbody2D>();

		audioSource = GetComponent<AudioSource>();

		gameController = (GameController)FindObjectOfType(typeof(GameController));

//		if (audioSource.isPlaying) {
//			audioSource.Stop ();
//			audioSource.Play ();
//		} else {
//			audioSource.Play ();
//		}

	}

	void Start() {
		//		float magnitude = Random.Range(20f, 20f);
		float magnitude = 0.0f;

		//		float torque = Random.Range(minTorque, maxTorque);
		float torque = 0.002f;

		float x = Random.Range(-1.0f, 1.0f);
		float y = Random.Range(-1.0f, 1.0f);

		rigidbody2D.AddTorque(torque);

		Vector2 randomVector = new Vector2 (x, y);

		//		Debug.Log("Asteroid start -- magnitude:" + magnitude);

		Debug.Log(string.Format("{0} Start randomVector: {1} magnitude: {2}", GetType(), randomVector, magnitude));
		rigidbody2D.AddForce(randomVector * magnitude);

		Debug.Log(string.Format("{0} Start torque: {1}", GetType(), torque));
		rigidbody2D.AddTorque(torque);




		Destroy (gameObject, lifeInSeconds);

	}
		
	void Update() {

	}
		
	void FixedUpdate() {

	}

	void OnDestroy(){
		Instantiate(Explosion, transform.position, new Quaternion());

	}

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void OnCollisionEnter2D(Collision2D collision) {
		GameObject otherGameObject = collision.gameObject;
		string tag = otherGameObject.tag;
//		Debug.Log(string.Format("{0} OnCollisionEnter2D: {1}", GetType(), tag));

		if ("tag_bullet_ship" == tag) {
			collisionBulletShip (otherGameObject);
		} else if ("tag_ship" == tag) {
			collisionShip (otherGameObject);
		} else if ("tag_asteroid_large" == tag) {
			collisionAsteroid (otherGameObject);
			//			Destroy (gameObject);
		} else if ("tag_asteroid_medium" == tag) {
			collisionAsteroid (otherGameObject);
			//			Destroy (gameObject);
		} else if ("tag_asteroid_small" == tag) {
			collisionAsteroid (otherGameObject);
			//			Destroy (gameObject);
		} else {
			Debug.Log(string.Format("{0} OnCollisionEnter2D UNKNOWN: {1}", GetType(), tag));
		}

	}

	void OnTriggerEnter2D(Collider2D collider) {
		GameObject otherGameObject = collider.gameObject;
		string tag = otherGameObject.tag;
//		Debug.Log(string.Format("{0} OnTriggerEnter2D: {1}", GetType(), tag));

		if ("tag_bullet_ship" == tag) {
			collisionBulletShip (otherGameObject);
		} else if ("tag_ship" == tag) {
			collisionShip (otherGameObject);
		} else if ("tag_asteroid_large" == tag) {
			collisionAsteroid (otherGameObject);
//			Destroy (gameObject);
		} else if ("tag_asteroid_medium" == tag) {
			collisionAsteroid (otherGameObject);
//			Destroy (gameObject);
		} else if ("tag_asteroid_small" == tag) {
			collisionAsteroid (otherGameObject);
//			Destroy (gameObject);
		} else {
			Debug.Log(string.Format("{0} OnTriggerEnter2D UNKNOWN: {1}", GetType(), tag));
		}
	}

//	void OnTriggerExit2D(Collider2D collider) {
//		GameObject colliderGameObject = collider.gameObject;
//		string tag = colliderGameObject.tag;
//		Debug.Log(string.Format("{0} OnTriggerExit2D: {1}", GetType(), tag));
//	}
		
	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void collisionShip(GameObject colliderGameObject) {

		gameController.AddScore (pointValue);
		Debug.Log(string.Format("{0} AddScore pointValue: {1}", GetType(), pointValue));

		Color flashColor = Color.green;
		StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));

//		AudioSource audioSource = GetComponent<AudioSource>();

		if (audioSource.isPlaying) {
//			audioSource.Stop ();
//			audioSource.Play ();
		} else {
			audioSource.Play ();
		}

		Destroy (gameObject);
	}

	void collisionBulletShip(GameObject colliderGameObject) {
		Destroy (gameObject);
		Destroy (colliderGameObject);
	}

	void collisionAsteroid(GameObject colliderGameObject) {
		Destroy (gameObject);
	}

	IEnumerator FlashMaterial(Color color, float waitTime) {
		Renderer renderer = GetComponent<Renderer>();
		Color grayColor = new Color(0.9F, 0.9F, 0.9F, 1.0F);
		//		Color magentaColor = Color.magenta;

		renderer.material.color = color;
		yield return new WaitForSeconds(waitTime);
		renderer.material.color = grayColor;
	}


}
