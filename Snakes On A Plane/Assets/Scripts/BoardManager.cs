using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public int max_health;
    public int dimension;
    public GameObject[] player_prefabs; // for different players just have two different prefabs here
    public float initial_offset;
    public int bpm;

	public Vector2 beat_visualizer_location;
	public Vector2 board_location;

	private BeatVisualizer b_vis;
	private Board board;
	private Player[] players;
    private Music music;
    private List<string> dataCollection;
	private bool active = false;

    private Dictionary<string, Vector2> directions;

	// Use this for initialization
	public void Init (AudioSource audio, GameObject tile_prefab) {
		// initialize tiles
		board = gameObject.GetComponent(typeof(Board)) as Board;
		board.Init(board_location, dimension, dimension, tile_prefab);

		// initialize players
		players = new Player[2];
		Vector2[] player_positions = new Vector2[] {new Vector2(0, 0), new Vector2(dimension-1, dimension-1)};
		for (int i = 0; i < 2; i++) {
			GameObject player_object = Instantiate (player_prefabs[i], player_positions[i] + board_location, new Quaternion ());
			players [i] = new Player (i, player_object, player_positions [i], max_health, board);
			GameObject health_bar = GameObject.Find ("HealthBar" + i);
			health_bar.GetComponent<HealthBar> ().Init ();
		}
			
		// initialize directions
		directions = new Dictionary<string, Vector2>();
		directions.Add ("Left", new Vector2 (-1, 0));
		directions.Add ("Right", new Vector2 (1, 0));
		directions.Add ("Up", new Vector2 (0, 1));
		directions.Add ("Down", new Vector2 (0, -1));

        music = new Music(audio, bpm, initial_offset);
		
		// initialize beat visualizer
		b_vis = gameObject.GetComponent(typeof(BeatVisualizer)) as BeatVisualizer;
		b_vis.Init(beat_visualizer_location, music.GetLeeway());
        
        dataCollection = new List<string>();

		active = true;
	}

	// Update is called once per frame
	void Update () {
		if (!active) {
			return;
		}
        music.UpdateTime(Time.deltaTime);
        if (music.IsNewBeat()) EveryBeat();
        
		// take player inputs
		for (int i = 0; i < 2; i++) {
			foreach (string key in directions.Keys) {
				if (Input.GetButtonDown ("P" + i + key)) {
                    dataCollection.Add(key);
					TryMovePlayer (i, key);
				}
			}
		}
	}

    void EveryBeat() {
        for (int i = 0; i < 2; i++) {
            board.setTileState(players[i].X(), players[i].Y(), Board.TileState.Dead);
            players[i].ResetMoved();

            if (!players[i].IsAlive())
            {
                StaticValues.winner = 1 - i;
                UnityEngine.SceneManagement.SceneManager.LoadScene("EndScreen");
            }
        }

		//temporary feedback for notes on the tester
		float[] timings = music.GetBeatsForNextNSeconds(b_vis.seconds_in_advance + music.secondsPerBeat);
		foreach (float timing in timings) {
			if (timing > b_vis.seconds_in_advance) {
				StartCoroutine(b_vis.CreateNote(timing - b_vis.seconds_in_advance));
			}
		}
    }

    public int GetPlayerHealth(int p_num)
    {
        return players[p_num].health;
    }

	void TryMovePlayer(int p_num, string key) {
        if (!music.WithinLeeway())
        {
            players[p_num].MissedBeat();
            return;
        }
		if (players[p_num].Move(directions[key])) {
			board.setTileState(players[p_num].X(), players[p_num].Y(), Board.TileState.Dead);
		}
	}
}
