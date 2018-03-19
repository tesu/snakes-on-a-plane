using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatVisualizer : MonoBehaviour {

	//Bind Relevant Prefabs
	public GameObject note_prefab;
	public GameObject staff_prefab;
	public GameObject cleff_prefab;
	public GameObject strikebar_prefab;

	//Instances of prefabs;
	private GameObject staff;
	private GameObject cleff;
	private GameObject strikebar;

	private Vector2 position_unity_units;
	private Vector2 relative_cleff_position_unity_units = new Vector2(-3.6f, -0.075f);
	private Vector2 staff_extends;

	// note_spawn_left is where the note will 
	private float note_spawn_left = -3.5f;
	private float note_destroy_right = 4.0f;
	private float note_hit_right = 3.0f;
	private float note_vertical_offset = 0.17f;

	private float note_tolerance_secs;
	private float note_tolerance_length = 1;
	private float note_velocity_x;
	public float seconds_in_advance;
	private Note[] notes;
	
	public Sprite active_note;

	public void Init(Vector2 position_unity_units, float note_tolerance_secs) {
		//create game objects and staff
		this.staff = Instantiate(staff_prefab, position_unity_units, new Quaternion());
		this.staff.GetComponent<SpriteRenderer>().sortingLayerName = "BeatVis";
		this.staff_extends = this.staff.GetComponent<SpriteRenderer>().sprite.bounds.extents;

		this.cleff = Instantiate(cleff_prefab, position_unity_units + relative_cleff_position_unity_units, new Quaternion());
		this.cleff.GetComponent<SpriteRenderer>().sortingLayerName = "Notes";

		this.strikebar = Instantiate(strikebar_prefab, position_unity_units+ new Vector2(note_hit_right, 0), new Quaternion());
	
		// note velocity is dependent on how long we want the notes to be allowed.
		note_velocity_x =  note_tolerance_length/note_tolerance_secs;
		// seconds in advance is how long we will need to know about notes in advance.
		seconds_in_advance = (note_hit_right - note_spawn_left)/note_velocity_x;
		//set internal variables
		this.note_tolerance_secs = note_tolerance_secs;
		this.position_unity_units = position_unity_units;
	}

	public IEnumerator CreateNote(float delay) {
		yield return new WaitForSeconds(delay);
		GameObject note = Instantiate(note_prefab);
		note.GetComponent<Rigidbody2D>().velocity = new Vector2(note_velocity_x, 0);
		note.GetComponent<SpriteRenderer>().sortingLayerName = "Notes";
		
		float note_verticality = ((float)Mathf.Round(Random.Range(4f, -4f)))*staff_extends.y/4f + note_vertical_offset;
		note.transform.position = position_unity_units + new Vector2(note_spawn_left, note_verticality);

		float edge = this.position_unity_units.x + note_destroy_right + note_tolerance_length/2;
		float measure_edge = this.position_unity_units.x + note_destroy_right - note_tolerance_length/2;

		Note noteScript = note.AddComponent<Note>() as Note;
		noteScript.Init(edge, measure_edge ,active_note);
	}

	void Update () {
	}
}