﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelect : MonoBehaviour {

	// maybe read this data from a file in the future
	private string[] songnames = {"Reformat", "Industrious_Ferret", "Pookatori_and_Friends", "4", "5", "6"};
	private string[] bgs = {"volcano", "cemetery", "iceberg", "digital", "space", "village"};

	private string bg_prefix = "BG_";
	private string song_prefix = "Song_";
	private string bg_layer = "BG";
	private string default_layer = "default";
	private SongInfo[] songs;
	private int song_index = 0;
	private bool song_unselected = true;

	// Use this for initialization
	void Start () {
		songs = new SongInfo[songnames.Length];
		for(int i = 0; i < songnames.Length; i++) {
			songs[i] = new SongInfo(songnames[i], bgs[i]);
		}
		GameObject background = GameObject.Find (bg_prefix + songs[0].bg);
		background.GetComponent<SpriteRenderer> ().sortingLayerName = bg_layer;
		GameObject song_info_display = GameObject.Find (song_prefix + songs [0].name);
		song_info_display.GetComponent<UnityEngine.UI.Text> ().enabled = true;
		song_info_display.GetComponent<AudioSource> ().enabled = true;
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
				GameObject.Find ("LeftArrow").GetComponent<SpriteRenderer>().enabled = false;
				GameObject.Find ("RightArrow").GetComponent<SpriteRenderer>().enabled = false;
				GameObject.Find ("SpaceToSelect").GetComponent<UnityEngine.UI.Text>().enabled = false;
				GameObject song_info_display = GameObject.Find (song_prefix + songs [song_index].name);
				song_info_display.GetComponent<UnityEngine.UI.Text>().enabled = false;

				BoardManager board = gameObject.GetComponent (typeof(BoardManager)) as BoardManager;
				board.Init (song_info_display.GetComponent<AudioSource>());
			}
		}
	}

	void ShiftSong(int shift) {
		string old_bg = songs [song_index].bg;
		string old_name = songs [song_index].name;
		song_index = (song_index + shift + songs.Length) % songs.Length;

		GameObject new_background = GameObject.Find (bg_prefix + songs[song_index].bg);
		new_background.GetComponent<SpriteRenderer> ().sortingLayerName = bg_layer;
		GameObject old_background = GameObject.Find (bg_prefix + old_bg);
		old_background.GetComponent<SpriteRenderer> ().sortingLayerName = default_layer;

		GameObject new_song = GameObject.Find (song_prefix + songs[song_index].name);
		new_song.GetComponent<UnityEngine.UI.Text> ().enabled = true;
		new_song.GetComponent<AudioSource> ().enabled = true;
		GameObject old_song = GameObject.Find (song_prefix + old_name);
		old_song.GetComponent<UnityEngine.UI.Text> ().enabled = false;
		old_song.GetComponent<AudioSource> ().enabled = false;
	}

}
