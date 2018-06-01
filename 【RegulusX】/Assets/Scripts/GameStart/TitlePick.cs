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
	public bool Choose;
	public int Select = 0;
	public bool KeyMode = true;
	// Use this for initialization
	void Start () {
        PickOrder = 1;
        PickerPos = StartGame.transform.position + new Vector3(-50,0,0);
		GameObject.Find("TitlePicker").transform.position = PickerPos;

		//音效
		Enter = GameObject.Find ("Decide").GetComponent<AudioSource>();
		Switch = GameObject.Find ("Switch").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (KeyMode);

		if (Select == 0 && KeyMode == false) {
			GameObject.Find ("TitlePicker").transform.position = StartGame.transform.position + new Vector3 (-5000, 0, 0);
			StartGame.fontSize = 50;
			ExitGame.fontSize = 50;
		} else if (Select == 1 && KeyMode == false) {
			GameObject.Find ("TitlePicker").transform.position = StartGame.transform.position + new Vector3 (-50, 0, 0);
			ExitGame.fontSize = 50;
		} else if (Select == 2 && KeyMode == false) {
			GameObject.Find ("TitlePicker").transform.position = ExitGame.transform.position + new Vector3 (-50, 0, 0);
			StartGame.fontSize = 50;
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            PickerPos = StartGame.transform.position + new Vector3(-50, 0, 0);
			GameObject.Find("TitlePicker").transform.position = PickerPos;
            PickOrder = 1;
            StartGame.fontSize = 72;
            ExitGame.fontSize = 50;
			KeyMode = true;
			Switch.Play ();

        }
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            PickerPos = ExitGame.transform.position + new Vector3(-50, 0, 0);
			GameObject.Find("TitlePicker").transform.position = PickerPos;
            PickOrder = 2;
            StartGame.fontSize = 50;
            ExitGame.fontSize = 72;
			KeyMode = true;
			Switch.Play ();

        }
        if(PickOrder == 1)
        {
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Choose)
            {
				Enter.Play ();
                SceneManager.LoadScene("Level01");
            }
        }
        if (PickOrder == 2)
        {
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Choose)
            {
				Enter.Play ();
                Application.Quit();
            }
        }
	}

	public void ToChoose(){
		Choose = true;

	}
}
