using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Submit"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("snakes");
		}
		if (Input.GetButtonDown("Rules"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("RulesScene");
		}
		if (Input.GetButtonDown("Credits"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("CreditsScene");
		}
    }
}
