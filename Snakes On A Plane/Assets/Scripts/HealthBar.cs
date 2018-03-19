using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
  public BoardManager manager;
  public Sprite base_sprite;
  public Sprite white_sprite;
  public int player_num;
  public bool growRight;
  public float flash_length = 0.1f;
  private float original_size;
  private bool active = false;

  // Use this for initialization
  public void Init () {
    original_size = gameObject.transform.localScale.x;
	active = true;
  }

  private float edgeX() {
    if (growRight) {
      return gameObject.GetComponent<Renderer>().bounds.min.x;
    }
    else {
      return gameObject.GetComponent<Renderer>().bounds.max.x;
    }
  }

  private float calculateScaleX() {
    float a = original_size * manager.GetPlayerHealth(player_num) / manager.max_health;
    if (a <= 0) {
      return 0;
    }
    return a;
  }

  // Update is called once per frame
  void Update () {
	if (!active) {
		return;
	}
	float oldX = edgeX ();

	Vector3 s = gameObject.transform.localScale;
	gameObject.transform.localScale = new Vector3 (calculateScaleX (), s.y, s.z);

	float newX = edgeX ();
	float difference = newX - oldX;
	if (newX != oldX) {
	  StartCoroutine (FlashWhite ());
	}
	transform.Translate (new Vector3 (-difference, 0f, 0f));
  }

  IEnumerator FlashWhite() {
	gameObject.GetComponent<SpriteRenderer> ().sprite = white_sprite;
	yield return new WaitForSeconds(flash_length);
	gameObject.GetComponent<SpriteRenderer> ().sprite = base_sprite;
  }
}
