using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;
using UnityEngine.UI;

public class UiController : MonoBehaviour {
    public static Flowchart istalking;
    public static Flowchart Mission;
    public static Flowchart Begining;
    MissionController missionctor;
    public GameObject EndButton;
    public GameObject ExitButton;
    public GameObject NextButton;
    public GameObject LastButton;
    public GameObject teach1;
    public GameObject teach2;
    public GameObject GameOperation;
    public int EscCount;
    public Text PageText;
    public int PageNumer;

    void Start () {
        PageNumer = 1;
        EscCount = 0;
        PageText.text = "";
        ExitButton.SetActive(false);
        GameObject.Find("EndButton").SetActive(false);
        GameObject.Find("NextPageBtn").SetActive(false);
        GameObject.Find("LastPageBtn").SetActive(false);
        istalking = GameObject.Find("IsTalking").GetComponent<Flowchart>();
        Mission = GameObject.Find("Mission").GetComponent<Flowchart>();
        Begining = GameObject.Find("Begining").GetComponent<Flowchart>();
        MissionController missionctor = GetComponent<MissionController>();
    }
    // Update is called once per frame
    void Update () {
        BtnSetActive();
        LockPlayer();
        ContinuedButton();
        if (Input.GetKeyDown(KeyCode.O) && UiController.CG1End)
        {
            Flowchart.BroadcastFungusMessage("EscGameOperation");
            if (UiController.Teach)
            {
                NextButton.SetActive(false);
                LastButton.SetActive(false);
                teach1.SetActive(false);
                teach2.SetActive(false);
                GameOperation.SetActive(false);
                PageText.text = "";
                PageNumer = 0;
            }
            else if (!UiController.Teach && !teach1.activeSelf)
            {
                teach1.SetActive(true);
                NextButton.SetActive(true);
                PageNumer = 1;
                PageText.text = "1/3";
            }
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
    public static bool Page
    {
        get { return Begining.GetBooleanVariable("PageVisable"); }
    }
    public static bool Teach
    {
        get { return Begining.GetBooleanVariable("GameOperation"); }
    }
    public static bool CG1End
    {
        get { return Begining.GetBooleanVariable("CG1End"); }
    }

    void LockPlayer()
    {
        if(!UiController.Talking || !UiController.Page)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
			GameObject.Find("FloorSon").GetComponent<BoxCollider>().enabled = false;
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
        else if (UiController.Talking||UiController.Page)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
			GameObject.Find("FloorSon").GetComponent<BoxCollider>().enabled = true;
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

    public void BtnSetActive()
    {
        if (UiController.Page)
        {

            if (PageNumer == 1 && !GameOperation.activeSelf)
            {
                NextButton.SetActive(true);
                PageText.text = "1/3";
            }
        }
    }

    public void PageNumberAdd()
    {
        if (PageNumer == 1)
        {
            teach1.SetActive(false);
            teach2.SetActive(true);
            LastButton.SetActive(true);
            NextButton.SetActive(true);
            PageText.text = "2/3";
        }
        else if(PageNumer == 2)
        {
            teach2.SetActive(false);
            GameOperation.SetActive(true);
            NextButton.SetActive(false);
            LastButton.SetActive(true);
            PageText.text = "3/3";
        }
        PageNumer = Mathf.Clamp((PageNumer+1), 1, 3);
    }

    public void PageNumberSub()
    {
        if (PageNumer == 2)
        {
            teach2.SetActive(false);
            teach1.SetActive(true);
            LastButton.SetActive(false);
            NextButton.SetActive(true);
            PageText.text = "1/3";
        }
        else if (PageNumer == 3)
        {
            GameOperation.SetActive(false);
            teach2.SetActive(true);
            LastButton.SetActive(true);
            NextButton.SetActive(true);
            PageText.text = "2/3";
        }
        PageNumer = Mathf.Clamp((PageNumer - 1), 1, 3);
    }
}
