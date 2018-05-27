using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToGhost : MonoBehaviour {

	public GameObject White;

	// Use this for initialization
	void Start () {
		White = GameObject.Find("White");
		White.GetComponent<Renderer>().material.color = new Color(0f,0f,0f,100f);
	}
	
	// Update is called once per frame
	void Update () {
		if(White.GetComponent<Renderer>().material.color != new Color(0f,0f,0f,100f))
		White.GetComponent<Renderer>().material.color += new Color(0f,0f,0f,-0.05f);
	}
}
