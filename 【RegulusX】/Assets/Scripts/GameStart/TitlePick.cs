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
	// Use this for initialization
	void Start () {
        PickOrder = 1;
        PickerPos = StartGame.transform.position + new Vector3(-250,0,0);
        transform.position = PickerPos;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PickerPos = StartGame.transform.position + new Vector3(-250, 0, 0);
            transform.position = PickerPos;
            PickOrder = 1;
            StartGame.fontSize = 72;
            ExitGame.fontSize = 50;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PickerPos = ExitGame.transform.position + new Vector3(-250, 0, 0);
            transform.position = PickerPos;
            PickOrder = 2;
            StartGame.fontSize = 50;
            ExitGame.fontSize = 72;
        }
        if(PickOrder == 1)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Level01");
            }
        }
        if (PickOrder == 2)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Application.Quit();
            }
        }
    }
}
