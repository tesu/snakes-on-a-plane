using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManager : MonoBehaviour {
    public GameObject end_text;

    // Use this for initialization
    void Start () {
		if (StaticValues.winner == 1)
        {
            end_text.GetComponent<UnityEngine.UI.Text>().text = "Red wins!";
        }
        else
        {
            end_text.GetComponent<UnityEngine.UI.Text>().text = "Blue wins!";
        }
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
