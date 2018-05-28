using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class UiController : MonoBehaviour {
    public static Flowchart istalking;
    public static Flowchart Mission;
    MissionController missionctor;
    public GameObject EndButton;

    void Awake () {
        GameObject.Find("EndButton").SetActive(false);
        istalking = GameObject.Find("IsTalking").GetComponent<Flowchart>();
        Mission = GameObject.Find("Mission").GetComponent<Flowchart>();
        MissionController missionctor = GetComponent<MissionController>();
    }

    // Update is called once per frame
    void Update () { 
        LockPlayer();
        ContinuedButton();
        if (Input.GetKeyDown(KeyCode.O))
        {
            Flowchart.BroadcastFungusMessage("EscGameOperation");
        }
    }
    public static bool Talking
    {
        get { return istalking.GetBooleanVariable("IsTalking"); }
    }
    public static bool FirstChapterEnd
    {
        get { return Mission.GetBooleanVariable("End"); }
    }

    void LockPlayer()
    {
        if(!UiController.Talking)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
        }
        else if (UiController.Talking)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
        }
    }

    void ContinuedButton()
    {
        if (UiController.FirstChapterEnd)
        {
            EndButton.SetActive(true);
        }
    }

    public void ClickToMenu()
    {
        SceneManager.LoadScene("GameStart");
    }

}
