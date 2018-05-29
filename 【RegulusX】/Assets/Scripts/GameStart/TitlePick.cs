using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitlePick : MonoBehaviour {
    public Vector3 PickerPos;
    public Text StartGame;
    public Text ExitGame;
    public int PickOrder;

	private AudioSource Enter;
	private AudioSource Switch;
	// Use this for initialization
	void Start () {
        PickOrder = 1;
        PickerPos = StartGame.transform.position + new Vector3(-350,0,0);
        transform.position = PickerPos;

		Enter = GameObject.Find ("Decide").GetComponent<AudioSource>();
		Switch = GameObject.Find ("Switch").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W))
        {
			Switch.Play ();
            PickerPos = StartGame.transform.position + new Vector3(-350, 0, 0);
            transform.position = PickerPos;
            PickOrder = 1;
            StartGame.fontSize = 72;
            ExitGame.fontSize = 50;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
			Switch.Play ();
            PickerPos = ExitGame.transform.position + new Vector3(-350, 0, 0);
            transform.position = PickerPos;
            PickOrder = 2;
            StartGame.fontSize = 50;
            ExitGame.fontSize = 72;
        }
        if(PickOrder == 1)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
				Enter.Play ();
                SceneManager.LoadScene("Level01");
            }
        }
        if (PickOrder == 2)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
				Enter.Play ();
                Application.Quit();
            }
        }
    }
}
