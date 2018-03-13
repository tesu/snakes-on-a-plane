using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManager : MonoBehaviour {
    public GameObject end_text;

    // Use this for initialization
    void Start () {
		if (StaticValues.winner == 1)
        {
            end_text.GetComponent<UnityEngine.UI.Text>().text = "Blue won!";
        }
        else
        {
            end_text.GetComponent<UnityEngine.UI.Text>().text = "Red won!";
        }
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("snakes");
    }
}
