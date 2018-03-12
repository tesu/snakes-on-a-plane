using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note : MonoBehaviour {

	public int lifetime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lifetime--;

		if (lifetime <= 0) {
			Destroy(this);
		}
	}
}
