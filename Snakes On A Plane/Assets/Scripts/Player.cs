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
	private Color color;

    public Player (int p_id, GameObject p_game_object, Color p_color, Vector2 p_position, int p_health, Board p_board)
	{
		id = p_id;
		game_object = p_game_object;
		color = p_color;
		position = p_position;
		health = p_health;
        board = p_board;
        moved = false;
        game_object.GetComponent<Renderer>().material.color = p_color;
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

    public bool Move(Vector2 direction)
    {
        if (!moved && CanMove(direction))
        {
            position += direction;
            game_object.transform.Translate(direction);
            moved = true;
            return true;
        }
        moved = true;
        MadeBadMove();
        return false;
    }

    public void MissedBeat()
    {
        if (health > 0) health -= 1;
    }

    public void WrongBeat()
    {
        if (health > 0) health -= 1;
    }

    public void MadeBadMove()
    {
        if (health > 0) health -= 1;
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

