using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAnimation : MonoBehaviour {


	public  bool GoToScene;
	public  bool ChangeAlpha;
	public GameObject White;

	// Use this for initialization
	void Start () {
		White = GameObject.Find ("White");
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GoToScene)
			SceneManager.LoadScene("GameStart");
		if(ChangeAlpha)
			White.GetComponent<Renderer>().material.color += new Color(0f,0f,0f,0.05f);
	}
}
