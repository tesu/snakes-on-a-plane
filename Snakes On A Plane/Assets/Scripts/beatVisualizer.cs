using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatVisualizer : MonoBehaviour {

	public GameObject note_prefab;
	public float note_spawn_shift_left;
	public float note_spawn_shift_up;
	public Vector2 note_spawn_shift;
	public Note[] notes;
	// Use this for initialization
	void Start () {
		note_spawn_shift = new Vector2(note_spawn_shift_left, note_spawn_shift_up);

	}
	
	// Update is called once per frame
	void Update () {
	  GameObject note = Instantiate(note_prefab);
		note.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2);
		note.transform.position = (Vector2) this.transform.position + note_spawn_shift + new Vector2(Random.Range(-0.5f, 0.5f),0);
	}
}
