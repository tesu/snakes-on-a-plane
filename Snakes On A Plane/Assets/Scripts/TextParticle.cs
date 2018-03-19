using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextParticle : MonoBehaviour {

	// Use this for initialization
	float max_init_speed = 300;
	int frames = 0;
	int lifetime = 40;

	

	void Start () {
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-max_init_speed, max_init_speed), Random.Range(-max_init_speed, max_init_speed));
		
	}
	
	public void SetText (string t) {
	this.GetComponent<UnityEngine.UI.Text>().text = t;
	}
	public void SetColor (Color c) {
		this.GetComponent<UnityEngine.UI.Text>().color = c;
	}


	// Update is called once per frame
	void Update () {
		if (frames > lifetime) {
			Destroy(gameObject);
		}
		
		frames++;
	}
}
