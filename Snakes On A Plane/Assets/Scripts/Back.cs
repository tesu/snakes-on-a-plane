using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Back"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");
		}
	}
}
