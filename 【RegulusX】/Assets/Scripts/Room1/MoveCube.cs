using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour {
    public Camera main_camera;
    public Ray ray;
    public GameObject[] Lock;
    public GameObject[] Cube;
    public Vector3 tempA;
    public Vector3 tempB;
    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            ray = main_camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit[] raycasthit = Physics.RaycastAll(ray, 50);
            for (int i = 0; i < raycasthit.Length; i++)
            {
                for(int k = 0; k < 9; k++)// 選擇方塊
                {
                    if (raycasthit[i].collider.gameObject == Cube[k])
                    {
                        if (!Lock[k].activeSelf) //重複點擊，則把其他鎖定圖消失
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                Lock[j].SetActive(false);
                            }
                            Lock[k].SetActive(true);
                        }
                        else
                            Lock[k].SetActive(false);
                    }
                    if (Lock[k].activeSelf && raycasthit[i].collider.gameObject == Cube[8])
                    {//下面這行是判斷是否為目標方塊上下左右的方塊(斜、跨兩格皆不行)
                        if(Mathf.Abs(Lock[k].transform.position.x - Cube[8].transform.position.x) <= 1 && Mathf.Abs(Lock[k].transform.position.z - Cube[8].transform.position.z) <= 1 && Mathf.Abs(Lock[k].transform.position.x - Cube[8].transform.position.x) + Mathf.Abs(Lock[k].transform.position.z - Cube[8].transform.position.z) != 2)
                        {

                            tempA = Cube[k].transform.position;
                            Cube[k].transform.position = Cube[8].transform.position;
                            Cube[8].transform.position = tempA;
                            tempB = Lock[k].transform.position;
                            Lock[k].transform.position = Lock[8].transform.position;
                            Lock[8].transform.position = tempB;

                            Lock[k].SetActive(false);
                        }
                    }
                }
            }
        }

	}

}
