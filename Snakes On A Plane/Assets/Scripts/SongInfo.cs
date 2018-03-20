using System;
using UnityEngine;

public class SongInfo
{

	public string name;
	public string bg;
	public int bpm;
	public float initial_offset;
	public GameObject tile_prefab;

	public SongInfo (string song_name, string song_bg, GameObject song_tile_prefab, int song_bpm, float song_initial_offset)
	{
		name = song_name;
		bg = song_bg;
		tile_prefab = song_tile_prefab;
		bpm = song_bpm;
		initial_offset = song_initial_offset;
	}
}

