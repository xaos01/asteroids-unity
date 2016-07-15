using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour {

	public enum ObjectType {
		asteroid
	}
		
	public ObjectType objectType;

	public Asteroid.AsteroidType asteroidType;

	public AudioClip explosionAudioClip;

	public string explosionAudioClipFilename;

	private float emissionRateConstant;

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void Start() {
		if (asteroidType == Asteroid.AsteroidType.small) {
			emissionRateConstant = 30.0f;
			explosionAudioClipFilename = "Audio/fire.high";
		} else if (asteroidType == Asteroid.AsteroidType.medium) {
			emissionRateConstant = 60.0f;
			explosionAudioClipFilename = "Audio/explode.high";
		} else if (asteroidType == Asteroid.AsteroidType.large) {
			emissionRateConstant = 100.0f;
			explosionAudioClipFilename = "Audio/explode.low";
		} else {
			Debug.Log("ObjectType unknown");
		}

		explosionAudioClip = Resources.Load<AudioClip>(explosionAudioClipFilename) ;
		AudioSource audioSource = GetComponent<AudioSource>();
		audioSource.PlayOneShot(explosionAudioClip);

		ParticleSystem particleSystem = GetComponent<ParticleSystem>();

		var particleSystemEmission = particleSystem.emission;
		var emissionRate = new ParticleSystem.MinMaxCurve(emissionRateConstant);

		particleSystemEmission.rate = emissionRate;

		Destroy (gameObject, (particleSystem.duration + particleSystem.startLifetime));
//		Destroy (gameObject, 2f);
	}

	void Update() {

	}

	void FixedUpdate() {
		
	}

	void OnDestroy(){

	}

}
