using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

	public float max_X;

	// Use this for initialization
	public void Init(float max_X) {
		this.max_X = max_X;
	}

	public void Update() {
		//get current x position
		float current_x = this.transform.position.x;

		if (current_x > max_X) {
			Destroy(gameObject);
		}

	}
}