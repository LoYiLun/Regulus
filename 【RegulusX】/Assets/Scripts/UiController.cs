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
    public GameObject ExitButton;
    public int EscCount;

    void Start () {
        EscCount = 0;
        ExitButton.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(EscCount == 0)
            {
                ExitButton.SetActive(true);
                EscCount = 1;
            }
            else
            {
                ExitButton.SetActive(false);
                EscCount = 0;
            }

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
            GameObject.Find("2R1").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("2R2").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("2R3").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("2R4").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("2R5").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("2R6").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("2R7").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("2R8").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("2R9").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("2R10").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("2R11").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("2R12").GetComponent<BoxCollider>().enabled = true;
        }
        else if (UiController.Talking)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
            GameObject.Find("2R1").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("2R2").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("2R3").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("2R4").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("2R5").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("2R6").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("2R7").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("2R8").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("2R9").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("2R10").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("2R11").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("2R12").GetComponent<BoxCollider>().enabled = false;
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
        SceneManager.LoadScene("TitleAnimation");
    }

}
