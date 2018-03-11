using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	public int dimension;
	public GameObject tile_prefab;
	public GameObject player_prefab; // for different players just have two different prefabs here
	public Color[] player_colors = {Color.red, Color.blue}; // remove these simple colorings when we have real characters/animations
	public Color dead_tile_color = Color.black;
    public float initial_offset;
    public float seconds_per_beat;
    public float delta_time;

	private GameObject[,] board; // just x, y coordinates
	private GameObject[] players; // if we end up with a lot of player-related variables, just make a Player class
	private Vector2[] player_positions;
	private Dictionary<string, Vector2> directions;
    private float time_offset;

	// Use this for initialization
	void Start () {
		// initialize tiles
		board = new GameObject[dimension, dimension];
		for (int i = 0; i < dimension; i++) {
			for (int j = 0; j < dimension; j++) {
				board[i, j] = Instantiate(tile_prefab, new Vector2(i, j), new Quaternion());
			}
		}
		// initialize players
		players = new GameObject[2];
		player_positions = new Vector2[] {new Vector2(0, 0), new Vector2(dimension-1, dimension-1)};
		for (int i = 0; i < 2; i++) {
			players[i] = Instantiate (player_prefab, player_positions[i], new Quaternion ());
			players [i].GetComponent<Renderer> ().material.color = player_colors[i];
		}
		// initialize directions
		directions = new Dictionary<string, Vector2>();
		directions.Add ("Left", new Vector2 (-1, 0));
		directions.Add ("Right", new Vector2 (1, 0));
		directions.Add ("Up", new Vector2 (0, 1));
		directions.Add ("Down", new Vector2 (0, -1));

        GetComponent<AudioSource>().Play();
        time_offset = initial_offset;
	}
	
	// Update is called once per frame
	void Update () {
        time_offset += Time.deltaTime;
        while (time_offset >= seconds_per_beat) time_offset -= seconds_per_beat;
		// take player inputs
		for (int i = 0; i < 2; i++) {
			foreach (string key in directions.Keys) {
				if (Input.GetButtonDown ("P" + i + key)) {
					TryMovePlayer (i, key);
				}
			}
		}
	}

	void TryMovePlayer(int p_num, string key) {
        if (time_offset > delta_time && time_offset < seconds_per_beat - delta_time)
        {
            Debug.Log(time_offset);
            return;
        }

		Vector2 new_position = player_positions [p_num] + directions [key];
		if (IsPositionAllowed (new_position)) {
			board [(int)player_positions [p_num].x, (int)player_positions [p_num].y].GetComponent<Renderer> ().material.color = dead_tile_color;
			player_positions [p_num] += directions [key];
			players[p_num].transform.Translate (directions [key]);
		}
	}

	bool IsPositionAllowed(Vector2 position) {
		// for now, just checks in the dimension x dimension square, and not already used
		bool in_board = position.x >= 0 && position.x < dimension && position.y >= 0 && position.y < dimension;
		if (!in_board) {
			return false;
		}
		bool already_used = board [(int)position.x, (int)position.y].GetComponent<Renderer> ().material.color == dead_tile_color;
		return !already_used;
	}
}
