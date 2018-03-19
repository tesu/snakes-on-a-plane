using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextParticle : MonoBehaviour {

	// Use this for initialization
	float max_init_speed = 5;
	int frames = 0;
	int lifetime = 40;

	public Sprite text_good;
	public Sprite text_miss;
	public Sprite text_okay;
	public Sprite text_perfect;

	void Start () {
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-max_init_speed, max_init_speed), Random.Range(-max_init_speed, max_init_speed));
		
	}
	
	public void SetText (Music.Accuracy m) {
		switch (m)
			{
				case Music.Accuracy.okay:
					this.GetComponent<SpriteRenderer>().sprite = text_okay;
					break;
				case Music.Accuracy.good:
					this.GetComponent<SpriteRenderer>().sprite = text_good;
					break;
				case Music.Accuracy.perfect:
					this.GetComponent<SpriteRenderer>().sprite = text_perfect;
					break;
				case Music.Accuracy.miss:
					this.GetComponent<SpriteRenderer>().sprite = text_miss;
					break;
			}
	}


	// Update is called once per frame
	void Update () {
		if (frames > lifetime) {
			Destroy(gameObject);
		}
		
		frames++;
	}
}
