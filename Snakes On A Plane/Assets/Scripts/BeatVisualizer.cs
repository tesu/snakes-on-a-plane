using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatVisualizer : MonoBehaviour {

	//Bind Relevant Prefabs
	public GameObject note_prefab;
	public GameObject staff_prefab;
	public GameObject cleff_prefab;
	public GameObject strikezone_prefab;

	//Instances of prefabs;
	private GameObject staff;
	private GameObject cleff;
	private GameObject strikezone;

	private Vector2 position_unity_units; //points somewhere
	private Vector2 relative_cleff_position_unity_units = new Vector2(-3.6f, -0.075f);
	private Vector2 staff_extends;

	private float note_spawn_left = -3.5f;
	private float note_destroy_right = 3.5f;
	
	private float note_tolerance_secs;
	private float note_tolerance_length;
	private float note_velocity_x;
	public float seconds_in_advance;
	private Note[] notes;

	public void Init(Vector2 position_unity_units, float note_tolerance_secs) {
		//create game objects and staff
		this.staff = Instantiate(staff_prefab, position_unity_units, new Quaternion());
		this.staff.GetComponent<SpriteRenderer>().sortingLayerName = "BeatVis";
		this.staff_extends = this.staff.GetComponent<SpriteRenderer>().sprite.bounds.extents;

		this.cleff = Instantiate(cleff_prefab, position_unity_units + relative_cleff_position_unity_units, new Quaternion());
		this.cleff.GetComponent<SpriteRenderer>().sortingLayerName = "Notes";

		this.strikezone = Instantiate(strikezone_prefab, position_unity_units+ new Vector2(note_destroy_right, 0), new Quaternion());
		//we need to set the width of the strikezone proportional to the delta time, relative to the total note travel time.
		this.strikezone.GetComponent<SpriteRenderer>().sortingLayerName = "BeatVisSecondary";
		this.note_tolerance_length = this.strikezone.GetComponent<SpriteRenderer>().sprite.bounds.extents.x*2;
		
		// note velocity dependent on how long we want the notes to be allowed.
		note_velocity_x =  note_tolerance_length/note_tolerance_secs;
		// seconds in advance is how long we will need to know about notes in advance.
		seconds_in_advance = (note_destroy_right - (note_tolerance_length) - note_spawn_left)/note_velocity_x;
		//set internal variables
		this.note_tolerance_secs = note_tolerance_secs;
		this.position_unity_units = position_unity_units;
	}

	public IEnumerator CreateNote(float delay) {
		yield return new WaitForSeconds(delay);
		GameObject note = Instantiate(note_prefab);
		note.GetComponent<Rigidbody2D>().velocity = new Vector2(note_velocity_x, 0);
		note.GetComponent<SpriteRenderer>().sortingLayerName = "Notes";
		
		float note_verticality = ((float)Mathf.Round(Random.Range(4f, -4f)))*staff_extends.y/4.5f ;
		note.transform.position = position_unity_units + new Vector2(note_spawn_left, note_verticality);

		
		Note noteScript = note.AddComponent<Note>() as Note;
		noteScript.Init(this.position_unity_units.x + note_destroy_right + note_tolerance_length/2);
	}

	void Update () {
	}
}