using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelect : MonoBehaviour {

	private string[] songnames = {"song 1", "song 2", "song 3", "song 4", "song 5", "song 6"};
	private string[] bgs = {"volcano", "cemetery", "iceberg", "digital", "space", "village"};
	private SongInfo[] songs;
	private int song_index = 0;
	private bool song_unselected = true;

	// Use this for initialization
	void Start () {
		songs = new SongInfo[songnames.Length];
		for(int i = 0; i < songnames.Length; i++) {
			songs[i] = new SongInfo(songnames[i], bgs[i]);
		}
		GameObject background = GameObject.Find ("BG_" + songs[0].bg);
		background.GetComponent<SpriteRenderer> ().sortingLayerName = "BG";
	}
	
	// Update is called once per frame
	void Update () {
		if (song_unselected) {
			if (Input.GetButtonDown ("SongSelectLeft")) {
				ShiftSong (-1);
			}
			if (Input.GetButtonDown ("SongSelectRight")) {
				ShiftSong (1);
			}
			if (Input.GetButtonDown ("SongSelectEnter")) {
				song_unselected = false;
				BoardManager board = gameObject.GetComponent (typeof(BoardManager)) as BoardManager;
				board.Init ();
			}
		}
	}

	void ShiftSong(int shift) {
		string old_bg = songs [song_index].bg;
		song_index = (song_index + shift + songs.Length) % songs.Length;
		GameObject new_background = GameObject.Find ("BG_" + songs[song_index].bg);
		new_background.GetComponent<SpriteRenderer> ().sortingLayerName = "BG";
		GameObject old_background = GameObject.Find ("BG_" + old_bg);
		old_background.GetComponent<SpriteRenderer> ().sortingLayerName = "default";
	}

}
