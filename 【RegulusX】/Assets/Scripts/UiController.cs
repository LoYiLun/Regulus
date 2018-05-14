using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class UiController : MonoBehaviour {
    public static Flowchart istalking;
    MissionController missionctor;

    void Awake () {
        //GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
        istalking = GameObject.Find("IsTalking").GetComponent<Flowchart>();
        MissionController missionctor = GetComponent<MissionController>();
    }

    // Update is called once per frame
    void Update () { 
        LockPlayer();
    }
    public static bool Talking
    {
        get { return istalking.GetBooleanVariable("IsTalking"); }
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

}
