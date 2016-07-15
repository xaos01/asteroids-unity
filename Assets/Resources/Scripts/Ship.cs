using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public float rotateSpeed = 256.0f;
	public float thrustSpeed = 3.0f;
	public float zeroSpeed = 0.0f;

	private float flashLengthSeconds = 1.0f;

	public ParticleSystem ThrustParticleEffect;
	public GameObject BulletPrefab;

	public float bulletSpawnPosition = 1.0f;

	private Renderer renderer;
	private Rigidbody2D rigidbody2D;
	private GameController gameController;

	void Awake () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		renderer = GetComponent<Renderer>();

		//get a ref to the gameController so we can talk to it
		gameController = (GameController)FindObjectOfType(typeof(GameController));

	}

	void Start() {
		
	}

	void Update() {
	
	}

	void FixedUpdate() {

		if (Input.GetKey (KeyCode.A)) {
			rigidbody2D.angularVelocity = rotateSpeed;
		} else if (Input.GetKey (KeyCode.D)) {
			rigidbody2D.angularVelocity = -rotateSpeed;
		} else {
			rigidbody2D.angularVelocity = zeroSpeed;
		}

		if (Input.GetKey (KeyCode.W)) {
			rigidbody2D.AddForce (transform.up * thrustSpeed);
			ThrustParticleEffect.Play();
		} else {
			ThrustParticleEffect.Stop();
		}

		if(Input.GetKeyDown(KeyCode.Space)) {
			Instantiate(BulletPrefab, transform.position, transform.rotation);
		}

	}

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void OnCollisionEnter2D(Collision2D collision) {
		GameObject collisionGameObject = collision.gameObject;
		string tag = collisionGameObject.tag;
		Debug.Log(string.Format("{0} OnCollisionEnter2D: {1}", GetType(), tag));

		if ("tag_asteroid_large" == tag) {
			collisionAsteroid ();

//			Color flashColor = Color.red;
//			StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));
		} else if ("tag_asteroid_medium" == tag) {
			collisionAsteroid ();

//			Color flashColor = Color.red;
//			StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));
		} else if ("tag_asteroid_small" == tag) {
			collisionAsteroid ();

//			Color flashColor = Color.red;
//			StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));
		} else if ("tag_shrapnel" == tag) {
			collisionShrapnel ();

			//			Color flashColor = Color.yellow;
			//			StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));
		} else if ("tag_loot" == tag) {
			collisionLoot ();
//			gameController.AddScore(collisionGameObject.pointValue);

			Destroy (collisionGameObject);
			//			Color flashColor = Color.yellow;
			//			StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		GameObject collisionGameObject = collision.gameObject;
		string tag = collisionGameObject.tag;
		Debug.Log(string.Format("{0} OnCollisionExit2D: {1}", GetType(), tag));
	}





	void OnTriggerEnter2D(Collider2D collider) {
		GameObject colliderGameObject = collider.gameObject;
		string tag = colliderGameObject.tag;
		Debug.Log(string.Format("{0} OnCollisionEnter2D: {1}", GetType(), tag));
	}

	void OnTriggerExit2D(Collider2D collider) {
		GameObject colliderGameObject = collider.gameObject;
		string tag = colliderGameObject.tag;
		Debug.Log(string.Format("{0} OnTriggerExit2D: {1}", GetType(), tag));
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void collisionLoot() {
		Color flashColor = Color.green;
		StartCoroutine(FlashMaterial(flashColor, flashLengthSeconds));

		AudioSource audioSource = GetComponent<AudioSource>();

		if (audioSource.isPlaying) {
			audioSource.Stop ();
			audioSource.Play ();
		} else {
			audioSource.Play ();
		}
	}

	void collisionAsteroid() {
		Color flashColor = Color.red;
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

	void collisionShrapnel() {
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

	IEnumerator FlashMaterial(Color color, float waitTime) {
		Renderer renderer = GetComponent<Renderer>();
		Color grayColor = new Color(0.9F, 0.9F, 0.9F, 1.0F);
//		Color magentaColor = Color.magenta;

		renderer.material.color = color;
		yield return new WaitForSeconds(waitTime);
		renderer.material.color = grayColor;
	}

}
	