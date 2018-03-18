using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class EndScreenManager : MonoBehaviour {
    public GameObject end_text;
    public GameObject red_score;
	public GameObject blue_score;
	public GameObject pinkWin;
	public GameObject pinkLoss;
	public GameObject blueWin;
	public GameObject blueLoss;

    // Use this for initialization
    void Start () {
		if (StaticValues.winner == 1)
        {
			end_text.GetComponent<UnityEngine.UI.Text>().text = "Pink wins!";
			pinkWin.GetComponent<SpriteRenderer> ().sortingOrder = 1;
			pinkLoss.GetComponent<SpriteRenderer> ().sortingOrder = -1;
			blueWin.GetComponent<SpriteRenderer> ().sortingOrder = -1;
			blueLoss.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		}
        else
        {
			end_text.GetComponent<UnityEngine.UI.Text>().text = "Blue wins!";
			pinkWin.GetComponent<SpriteRenderer> ().sortingOrder = -1;
			pinkLoss.GetComponent<SpriteRenderer> ().sortingOrder = 1;
			blueWin.GetComponent<SpriteRenderer> ().sortingOrder = 1;
			blueLoss.GetComponent<SpriteRenderer> ().sortingOrder = -1;
		
        }
        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        {
            {"winner", StaticValues.winner }
        });
        red_score.GetComponent<UnityEngine.UI.Text>().text = "Pink: " + StaticValues.score[0];
        blue_score.GetComponent<UnityEngine.UI.Text>().text = "Blue: " + StaticValues.score[1];
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Submit"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("snakes");
        }
    }
}
