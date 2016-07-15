using UnityEngine;
using System.Collections;

public class BulletShip : MonoBehaviour {
	public float bulletSpeed = 2.0f;
	public float bulletLifeSpan = 2.0f;

	private ParticleSystem particleComponent;

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void Awake(){
		particleComponent = GetComponent<ParticleSystem>();

		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.AddForce(transform.up * bulletSpeed);
	}

	// Use this for initialization
	void Start() {
		Destroy(gameObject, bulletLifeSpan);
	}

	// Update is called once per frame
	void Update() {
		particleComponent.Emit(5);
	}

	void FixedUpdate() {

	}

}
