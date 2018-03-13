using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public float max_health;
    public int dimension;
    public GameObject tile_prefab;
    public GameObject player_prefab; // for different players just have two different prefabs here
    public Color[] player_colors = {Color.red, Color.blue}; // remove these simple colorings when we have real characters/animations
    public Color dead_tile_color = Color.black;
    public float initial_offset;
    public int bpm;

	private Board board;
	private Player[] players;
    private Music music;

    private Dictionary<string, Vector2> directions;

	// Use this for initialization
	void Start () {
		// initialize tiles
		board = new Board(dimension, dimension, tile_prefab);
		// initialize players

		players = new Player[2];
		Vector2[] player_positions = new Vector2[] {new Vector2(0, 0), new Vector2(dimension-1, dimension-1)};
		for (int i = 0; i < 2; i++) {
			GameObject player_object = Instantiate (player_prefab, player_positions[i], new Quaternion ());
			players [i] = new Player (i, player_object, player_colors [i], player_positions [i], max_health);
			players [i].game_object.GetComponent<Renderer> ().material.color = players [i].color;
		}
			
		// initialize directions
		directions = new Dictionary<string, Vector2>();
		directions.Add ("Left", new Vector2 (-1, 0));
		directions.Add ("Right", new Vector2 (1, 0));
		directions.Add ("Up", new Vector2 (0, 1));
		directions.Add ("Down", new Vector2 (0, -1));

        music = new Music(GetComponent<AudioSource>(), bpm, initial_offset);
	}

	// Update is called once per frame
	void Update () {
        music.UpdateTime(Time.deltaTime);
        if (music.IsNewBeat()) EveryBeat();
        
		// take player inputs
		for (int i = 0; i < 2; i++) {
			foreach (string key in directions.Keys) {
				if (Input.GetButtonDown ("P" + i + key)) {
					TryMovePlayer (i, key);
				}
			}
		}
	}

    void EveryBeat() {
        for (int i = 0; i < 2; i++) {
            if (!IsPositionAllowed(players[i].position)) {
                players[i].health -= 1;
                if (players[i].health <= 0) {
                    Destroy(players[i].game_object);
                    Destroy(this);
                }
            }
            board.setTileState((int)players [i].position.x, (int)players[i].position.y, Board.TileState.Dead);
        }
    }

    public float GetPlayerHealth(int p) {
        return players[p].health;
    }

	void TryMovePlayer(int p_num, string key) {
        if (!music.WithinLeeway()) return;

		Vector2 new_position = players[p_num].position + directions [key];
		if (IsPositionAllowed (new_position)) {
			board.setTileState((int)players[p_num].position.x, (int)players[p_num].position.y, Board.TileState.Dead);
			players[p_num].position += directions [key];
			players[p_num].game_object.transform.Translate (directions [key]);
		}
	}

	bool IsPositionAllowed(Vector2 position) {
		// for now, just checks in the dimension x dimension square, and not already used
		bool in_board = position.x >= 0 && position.x < board.tiles_x && position.y >= 0 && position.y < board.tiles_y;

		if (!in_board) {
			return false;
		}
		bool already_used = board.getTileState((int)position.x, (int)position.y) == Board.TileState.Dead;
		return !already_used;
	}
}
