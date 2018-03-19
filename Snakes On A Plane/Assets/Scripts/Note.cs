using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

	public float max_X;
	public float active_X;
	public bool active;

	public Sprite active_sprite;
	// Use this for initialization
	public void Init(float max_X, float active_X, Sprite active) {
		this.max_X = max_X;
		this.active_X = active_X;
		this.active_sprite = active;
	}

	public void Update() {
		//get current x position
		float current_x = this.transform.position.x;

		if (current_x > max_X) {
			Destroy(gameObject);
		}

		if (!active && current_x > active_X) {
			this.GetComponent<SpriteRenderer>().sprite = active_sprite;
			active = true;
		}

	}
}