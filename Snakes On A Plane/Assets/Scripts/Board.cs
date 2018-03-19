using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

  private GameObject[,] board;
  private TileState[,] tile_states;
  private int[,] beats_until_respawn;
  public int tiles_x;
  public int tiles_y;
    public int cell_respawn_time;

  private Vector2 position;
  private Vector2 tile_size;
  private Vector2 board_location;

  public GameObject tile_prefab;

  public Color active_tile_color = Color.white; // remove these colorings later
  public Color intermediate_tile_color = Color.white;

  public enum TileState {
    Active,
    Intermediate,
    Dead
  }

  public void Init(Vector2 board_location, int tiles_x, int tiles_y , GameObject tile_prefab) {
    this.tiles_x = tiles_x;
    this.tiles_y = tiles_y;
    this.tile_prefab = tile_prefab;
    this.board = new GameObject[tiles_y, tiles_x];
		this.tile_states = new TileState [tiles_y, tiles_x];
        this.beats_until_respawn = new int[tiles_y, tiles_x];
        this.board_location = board_location;

    // Initialize the board
    for (int i = 0; i < tiles_y; i++) {
			for (int j = 0; j < tiles_x; j++) {
				this.board[i, j] = Instantiate(tile_prefab, new Vector2(i, j) + board_location, new Quaternion());
        this.tile_states[i, j] = TileState.Active;
                this.beats_until_respawn[i, j] = 0;
			}
		}
   }

  public TileState getTileState(int x, int y) {
    return this.tile_states[x, y];
  }

  public void setTileState(int x, int y, TileState t) {
    switch (t)
    {
		case TileState.Active: //(yangk): to set active, you can either run the anim backward (?)
			// or you can honestly probably just remake the prefab, that's prob easier
          this.board[x, y].GetComponent<Renderer> ().material.color = active_tile_color;
          this.tile_states[x, y] = TileState.Active;
          break;
		case TileState.Intermediate: //(yangk): you probably don't need this state now that we have tile anims?
			// unless you want to set them to intermediate while you're waiting for them to become dead or smth
          board[x,y].GetComponent<Renderer> ().material.color = intermediate_tile_color;
          this.tile_states[x, y] = TileState.Intermediate;
          break;
		case TileState.Dead:
		  board [x, y].GetComponent<Animator> ().enabled = true;
          this.tile_states[x, y] = TileState.Dead;
            this.beats_until_respawn[x,y] = cell_respawn_time;
          break;
    }
  }

    public void blockRegenerate()
    {
        for (int x = 0; x < beats_until_respawn.GetLength(0); x++)
        {
            for (int y = 0; y < beats_until_respawn.GetLength(1); y++)
            {
                if (beats_until_respawn[x, y] == 1)
                {
					Destroy (this.board [x, y]);
                    this.board[x, y] = Instantiate(tile_prefab, new Vector2(x, y) + board_location, new Quaternion());
                    this.board[x, y].GetComponent<Animator>().enabled = false;
                    this.tile_states[x, y] = TileState.Active;
                    beats_until_respawn[x, y]--;
                }
                else if (beats_until_respawn[x, y] > 0)
                {
                    beats_until_respawn[x, y]--;
                }
            }
        }
    }

    public bool IsPositionAllowed(Vector2 position)
    {
        bool in_board = position.x >= 0 && position.x < tiles_x && position.y >= 0 && position.y < tiles_y;

        if (!in_board)
        {
            return false;
        }
        bool already_used = getTileState((int)position.x, (int)position.y) == Board.TileState.Dead;
        return !already_used;
    }
}