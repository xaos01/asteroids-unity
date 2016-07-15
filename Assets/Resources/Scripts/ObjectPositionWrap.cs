using UnityEngine;
using System.Collections;

public class ObjectPositionWrap : MonoBehaviour {
	public float minX = -7.0f;
	public float maxX = 7.0f;

	public float minY = -4.0f;
	public float maxY = 4.0f;

	void Start () {
	
	}
	
	void Update () {
	
	}

	void FixedUpdate() {
		float x = transform.position.x;
		float y = transform.position.y;

		if(x < minX) {
			x = maxX;
		} else if(x > maxX) {
			x = minX;
		}

		if(y < minY) {
			y = maxY;

		} else if(y > maxY) {
			y = minY;

		}

		transform.position = new Vector3 (x, y, transform.position.z);
	}

}
