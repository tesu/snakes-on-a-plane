using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public int id;
	public GameObject game_object;
	public Color color;
	public Vector2 position;
	public float health;

	public Player (int p_id, GameObject p_game_object, Color p_color, Vector2 p_position, float p_health)
	{
		id = p_id;
		game_object = p_game_object;
		color = p_color;
		position = p_position;
		health = p_health;
	}
}

