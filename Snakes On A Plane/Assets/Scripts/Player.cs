using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public int id;
	public int health;
    private bool moved;
    private GameObject game_object;
    private Vector2 position;
    private Board board;
    private BoardManager bm;
    private Direction facing;

    private enum Direction { down, right, up, left };

    public Player (int p_id, GameObject p_game_object, Vector2 p_position, int p_health, Board p_board, BoardManager p_bm)
	{
		id = p_id;
		game_object = p_game_object;
		position = p_position;
		health = p_health;
        board = p_board;
        moved = false;
        facing = Direction.down;
        bm = p_bm;
	}

    public void ResetMoved()
    {
        if (!moved) MissedBeat();
        moved = false;
    }

    private bool CanMove(Vector2 direction)
    {
        return board.IsPositionAllowed(position + direction);
    }

    private void Face(Direction d)
    {
        while (facing != d)
        {
            TurnRight();
        }
    }

    private void TurnRight()
    {
        game_object.transform.Rotate(0, 0, 90);
        switch (facing)
        {
            case Direction.down:
                facing = Direction.right;
                break;
            case Direction.right:
                facing = Direction.up;
                break;
            case Direction.up:
                facing = Direction.left;
                break;
            case Direction.left:
                facing = Direction.down;
                break;
        }
    }

    public bool Move(Vector2 direction)
    {
        if (direction.x > 0) Face(Direction.right);
        if (direction.x < 0) Face(Direction.left);
        if (direction.y > 0) Face(Direction.up);
        if (direction.y < 0) Face(Direction.down);

        if (!moved && CanMove(direction))
        {
            position += direction;
            game_object.transform.Translate(direction, Space.World);
            moved = true;
            board.setTileState(X(), Y(), Board.TileState.Dead);
            return true;
        }

        moved = true;
        MadeBadMove();
        return false;
    }

    public void MissedBeat()
    {
        if (!bm.IsInvulnerable() && health > 0) health -= 1;
    }

    public void WrongBeat()
    {
        if (!bm.IsInvulnerable() && health > 0) health -= 1;
    }

    public void MadeBadMove()
    {
        if (!bm.IsInvulnerable() && health > 0) health -= 1;
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public int X()
    {
        return (int)position.x; 
    }

    public int Y()
    {
        return (int)position.y;
    }
}

