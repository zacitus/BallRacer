using UnityEngine;
using System.Collections;

public class NextScene : MonoBehaviour {

    public bool exit = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame () 
	{
		//Application.LoadLevel("MainMenu");
        exit = true;
	}

	public void GoToCreator ()
	{
		Application.LoadLevel("CharacterCustomizer");
	}
}
