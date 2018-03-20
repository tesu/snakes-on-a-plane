using System;

public class SongInfo
{

	public string name;
	public string bg;
	public int bpm;
	public float initial_offset;

	public SongInfo (string song_name, string song_bg, int song_bpm, float song_initial_offset)
	{
		name = song_name;
		bg = song_bg;
		bpm = song_bpm;
		initial_offset = song_initial_offset;
	}
}

