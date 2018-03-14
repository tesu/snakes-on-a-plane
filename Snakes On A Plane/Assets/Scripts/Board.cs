using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

  private GameObject[,] board;
  private TileState[,] tile_states;
  public int tiles_x;
  public int tiles_y;

  private Vector2 position;
  private Vector2 tile_size;
  private Vector2 board_location;

  public GameObject tile_prefab;

  public Color[] player_colors = {Color.red, Color.blue}; // remove these simple colorings when we have real characters/animations
  public Color dead_tile_color = Color.black;
  public Color active_tile_color = Color.white;
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

    // Initialize the board
    for (int i = 0; i < tiles_y; i++) {
			for (int j = 0; j < tiles_x; j++) {
				this.board[i, j] = Instantiate(tile_prefab, new Vector2(i, j) + board_location, new Quaternion());
        this.tile_states[i, j] = TileState.Active;
			}
		}
   }

  public TileState getTileState(int x, int y) {
    return this.tile_states[x, y];
  }

  public void setTileState(int x, int y, TileState t) {
    switch (t)
    {
        case TileState.Active:
          this.board[x, y].GetComponent<Renderer> ().material.color = active_tile_color;
          this.tile_states[x, y] = TileState.Active;
          break;
        case TileState.Intermediate:
          board[x,y].GetComponent<Renderer> ().material.color = intermediate_tile_color;
          this.tile_states[x, y] = TileState.Intermediate;
          break;
        case TileState.Dead:
          board[x,y].GetComponent<Renderer> ().material.color = dead_tile_color;
          this.tile_states[x, y] = TileState.Dead;
          break;
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