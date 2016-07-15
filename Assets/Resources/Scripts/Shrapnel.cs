using UnityEngine;
using System.Collections;

public class Shrapnel : MonoBehaviour {

	private Rigidbody2D rigidbody2D;
	private float shrapnelLifeSeconds = 10.0f;

	private float flashLengthSeconds = 1.0f;

	private GameController gameController;

	public int pointValue = 10;

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void Awake(){
		//get a ref to our rigidbody2D
		rigidbody2D = GetComponent<Rigidbody2D>();

		gameController = (GameController)FindObjectOfType(typeof(GameController));

	}

	void Start() {
//		float magnitude = Random.Range(20f, 20f);
		float magnitude = 300f;

//		float torque = Random.Range(minTorque, maxTorque);
		float torque = 0.4f;

		float x = Random.Range(-1.0f, 1.0f);
		float y = Random.Range(-1.0f, 1.0f);

		rigidbody2D.AddTorque(torque);

		Vector2 randomVector = new Vector2 (x, y);

		//		Debug.Log("Asteroid start -- magnitude:" + magnitude);

		Debug.Log(string.Format("{0} Start randomVector: {1} magnitude: {2}", GetType(), randomVector, magnitude));
		rigidbody2D.AddForce(randomVector * magnitude);
		rigidbody2D.AddTorque(torque);

		Destroy (gameObject, shrapnelLifeSeconds);
	}

	void Update() {

	}
		
	void FixedUpdate() {

	}

	void OnDestroy(){
    
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void OnCollisionEnter2D(Collision2D collision) {
		GameObject collisionGameObject = collision.gameObject;
		string tag = collisionGameObject.tag;
		Debug.Log(string.Format("{0} OnCollisionEnter2D: {1}", GetType(), tag));

//		float flashLengthSeconds = .05f;

//		Color flashColor = Color.yellow;
//		StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));

		if ("tag_asteroid_large" == tag) {
			collisionAsteroid ();
		} else if ("tag_asteroid_medium" == tag) {
			collisionAsteroid ();
		} else if ("tag_asteroid_small" == tag) {
			collisionAsteroid ();
		} else if ("tag_ship" == tag) {
			collisionShip ();
		} else if ("tag_shrapnel" == tag) {
			collisionShrapnel ();
		} else {
			collisionUnknown ();
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		GameObject colliderGameObject = collider.gameObject;
		string tag = colliderGameObject.tag;
		Debug.Log(string.Format("{0} OnTriggerEnter2D: {1}", GetType(), tag));

		if ("tag_bullet_ship" == tag) {
			collisionBulletShip ();
		} else {
			collisionUnknown ();
		}

	}

	void OnTriggerExit2D(Collider2D collider) {
		GameObject colliderGameObject = collider.gameObject;
		string tag = colliderGameObject.tag;
		Debug.Log(string.Format("{0} OnTriggerExit2D: {1}", GetType(), tag));
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void collisionUnknown() {
		Debug.Log("OnCollisionEnter2D Hit! unknown type: " + tag);
		Color flashColor = Color.blue;
		StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));
	}
	void collisionAsteroid() {
		Color flashColor = Color.cyan;
		StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));

		//		AudioSource audioSource = GetComponent<AudioSource>();

		//		if (audioSource.isPlaying) {
		//			audioSource.Stop ();
		//			audioSource.Play ();
		//		} else {
		//			audioSource.Play ();
		//		}

		//		GameObject shrapnel = Instantiate (shrapnelPrefab, transform.position, new Quaternion()) as GameObject;
	}

	void collisionBulletShip() {
		Color flashColor = Color.green;
		StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));

		gameController.AddScore(pointValue);

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
	void collisionShrapnel() {
		Color flashColor = Color.white;
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

	IEnumerator FlashMaterial(Color color, float waitTime) {
		Renderer renderer = GetComponent<Renderer>();
		Color grayColor = new Color(0.9F, 0.9F, 0.9F, 1.0F);
		//		Color magentaColor = Color.magenta;

		renderer.material.color = color;
		yield return new WaitForSeconds(waitTime);
		renderer.material.color = grayColor;
	}

}
