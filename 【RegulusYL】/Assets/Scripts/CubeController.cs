using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

	private GameObject AllCube;
	private GameObject BeTouchedObj;
	private GameObject CubeMom;
	private GameObject FloorMom;
	public static GameObject R_Button;
	private GameObject Player;
	private GameObject stab;
	private string RotateNum ;
	//方塊的座標：L為左，R為右，D為下
	public int CubeL;
	public int CubeR;
	public int CubeD;
	//轉動軸與方向
	private int DX = 0;
	private int DY = 0;
	private int DZ = 0;
	private int A, B, K;
	private float speed = 8f;

	private Vector3 target = new Vector3();
	private bool moveState = false;
	private bool StopMouse = false;
	public int floortemp;
	public int childtemp;
	private float i;

	//--------------------------------------------------------
	void Start () {
		AllCube = GameObject.Find("AllCube");
		CubeMom = GameObject.Find("CubeMom");
		FloorMom = GameObject.Find("FloorMom");
		R_Button = GameObject.Find("R_Button");
		Player = GameObject.Find("Player");
		stab = GameObject.Find("stab");


	}

	void WatchMode (){
		R_Button.SetActive (false);
		Player.SetActive (false);
	}

	void FixedUpdate ()
	{

		//轉動場景
			if (Input.GetKey ("w")) {
			AllCube.transform.RotateAround (Vector3.zero, Vector3.back, speed/4 * Time.deltaTime);
			WatchMode ();
			}
			if (Input.GetKey ("a")) {
			AllCube.transform.RotateAround (Vector3.zero, Vector3.up, speed/4 * Time.deltaTime);
			WatchMode ();
			}
			if (Input.GetKey ("s")) {
			AllCube.transform.RotateAround (Vector3.zero, Vector3.forward, speed/4 * Time.deltaTime);
			WatchMode ();
			}
			if (Input.GetKey ("d")) {
			AllCube.transform.RotateAround (Vector3.zero, Vector3.down, speed/4 * Time.deltaTime);
			WatchMode ();
			}
			if (Input.GetKey ("q")) {
			AllCube.transform.RotateAround (Vector3.zero, Vector3.right, speed/4 * Time.deltaTime);
			WatchMode ();
			}
			if (Input.GetKey ("e")) {
			AllCube.transform.RotateAround (Vector3.zero, Vector3.left, speed/4 * Time.deltaTime);
			WatchMode ();
			}
			if (Input.GetKey ("space")) {
				AllCube.transform.rotation = Quaternion.Euler (0, 0, 0);
				Player.SetActive (true);
				R_Button.SetActive (true);
				R_Button.transform.rotation = Quaternion.Euler (0, 0, 0);
			}


		//發出射線 
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		if (Input.GetMouseButton (0) && Physics.Raycast (ray, out hitInfo) && StopMouse == false) {
			Debug.DrawLine (Camera.main.transform.position, hitInfo.transform.position, Color.yellow, 0.1f, true);
			//Debug.Log (hitInfo.transform.name);
			BeTouchedObj = hitInfo.collider.gameObject;
			if(Input.GetMouseButtonDown(0))
			RotateNum = BeTouchedObj.name;
			stab.transform.position = new Vector3 (hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
			if (hitInfo.transform.gameObject.tag == "floor") {
				moveState = true;
				target = new Vector3 (hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
			}
		}

		if(Input.GetMouseButton(0) == false)
			stab.transform.position = new Vector3 (0,0,0);

		//角色移動
		Vector3 targetDir = target - Player.transform.position ;
		Vector3 newDir = Vector3.RotateTowards(Player.transform.forward, targetDir, speed/120*Time.deltaTime, 0.0F);

		if(moveState){
			if(Vector3.Distance(Player.transform.position,target)<0.35f){
				moveState = false;
			}
			Player.transform.rotation = Quaternion.LookRotation(newDir);
			Player.transform.position = Vector3.MoveTowards(Player.transform.position,target,speed/120*Time.deltaTime);

		}





	//--------------------------------------------------------
		//點擊轉動按鈕
		switch(RotateNum){
		case"Start":
			StopMouse = true;
			PlayerController.OneShot = true;
			R_Button.SetActive (false);
			if (CubeMom.transform.childCount >= 9 + childtemp)
				CubeMom.transform.Rotate (new Vector3 (DX * speed * Time.deltaTime * 2, DY * speed * Time.deltaTime * 2, DZ * speed * Time.deltaTime * 2));
			//Rotateto90用來計算已轉動的角度值，解決0~90與360~270不同變化的問題。
			var RotateTo90 = Mathf.Abs (
				                 Mathf.Abs (CubeMom.transform.eulerAngles.x) +
				                 Mathf.Abs (CubeMom.transform.eulerAngles.y) +
				                 Mathf.Abs (CubeMom.transform.eulerAngles.z) - 180);

			if (RotateTo90 >= 1 && RotateTo90 <= 179) {
				speed *= 1.2f;
			}

			if (RotateTo90 >= 10 && RotateTo90 <= 170) {
				speed *= 0.79f;
			}

			if ( RotateTo90>= 90 && RotateTo90 <= 90.5f )
			{
				CubeMom.transform.rotation = Quaternion.Euler(DX * 90, DY * 90, DZ * 90);
				this.transform.parent = AllCube.transform;
				Player.transform.parent = null;
				FloorMom.transform.parent = null;
				speed = 8f;
				if(CubeMom.transform.childCount == 0)
					RotateNum = "Stop";
			}
			break;

		case "Stop":
			childtemp = 0;
			CubeMom.transform.rotation = Quaternion.Euler (0, 0, 0);
			PlayerController.MoveTarget = Player.transform.position;
			PlayerController.OneShot = false;
			R_Button.SetActive (true);
			StopMouse = false;
			RotateNum = null;
			break;


		/* 方法A
		case"R1":
			if (this.CubeL == 2) {
				this.transform.parent = CubeMom.transform;
				if (CubeMom.transform.childCount == 9) {
					CubeMom.transform.Rotate (new Vector3 (0f, 0f, 0.5f));
					if (CubeMom.transform.eulerAngles.z > 85) {
						CubeMom.transform.rotation = Quaternion.Euler (0f, 0f, 90f);
						this.transform.parent = null;
						if(CubeMom.transform.childCount == 0)
						RotateNum = null;
					}
				}
			}
			break;
		*/




		//方法B，取得轉動後新座標
		case"R1":
			if (this.CubeL == 2 && Player.transform.position.z > -0.6) {
				K = A = this.CubeD - 1;
				A = B = this.CubeR - 1;
				B = -K;
				this.CubeD = A + 1;
				this.CubeR = B + 1;
				DZ = 1;
				DX = DY = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			} else {
				RotateNum = null;
			}
				break;

		case"R2":
			if (this.CubeL == 1) {
				if (Player.transform.position.z > -0.6 && Player.transform.position.z < 0.8) {
					RotateNum = null;
					break;
				}
				
				K = A = this.CubeD - 1;
				A = B = this.CubeR - 1;
				B = -K;
				this.CubeD = A + 1;
				this.CubeR = B + 1;
				DZ = 1;
				DX = DY = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			}break;

		case"R3":
			if (this.CubeL == 0 && Player.transform.position.z < 0.8) {
				
				K = A = this.CubeD - 1;
				A = B = this.CubeR - 1;
				B = -K;
				this.CubeD = A + 1;
				this.CubeR = B + 1;
				DZ = 1;
				DX = DY = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			} else {
				RotateNum = null;
			}
			break;

		case"R4":
			if (this.CubeD == 0) {
				
				K = A = this.CubeL - 1;
				A = B = this.CubeR - 1;
				B = -K;
				this.CubeL = A + 1;
				this.CubeR = B + 1;
				DY = -1;
				DX = DZ = 0;
				this.transform.parent = CubeMom.transform;
				FloorMom.transform.parent = CubeMom.transform;//EX1
				Player.transform.parent = CubeMom.transform;
				childtemp = 2;

				floortemp = PlayerController.FloorL;
				PlayerController.FloorL = PlayerController.FloorR;
				PlayerController.FloorR = 8 - floortemp;

				
				RotateNum = "Start";
			} else
				RotateNum = null;break;

		case"R5":
			if (this.CubeD == 1) {
				
				K = A = this.CubeL - 1;
				A = B = this.CubeR - 1;
				B = -K;
				this.CubeL = A + 1;
				this.CubeR = B + 1;
				DY = -1;
				DX = DZ = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			} else {
				RotateNum = null;
			}break;

		case"R6":
			if (this.CubeD == 2) {
				
				K = A = this.CubeL - 1;
				A = B = this.CubeR - 1;
				B = -K;
				this.CubeL = A + 1;
				this.CubeR = B + 1;
				DY = -1;
				DX = DZ = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			} else {
				RotateNum = null;
			}break;

		case"R7":
			if (this.CubeR == 2 && Player.transform.position.x > -0.6) {
				
				K = A = this.CubeL - 1;
				A = B = this.CubeD - 1;
				B = -K;
				this.CubeL = A + 1;
				this.CubeD = B + 1;
				DX = 1;
				DY = DZ = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			} else {
				RotateNum = null;
			}break;

		case"R8":
			if (this.CubeR == 1) {
				if (Player.transform.position.x > -0.6 && Player.transform.position.x < 0.8) {
					RotateNum = null;
					break;
				}
				
				K = A = this.CubeL - 1;
				A = B = this.CubeD - 1;
				B = -K;
				this.CubeL = A + 1;
				this.CubeD = B + 1;
				DX = 1;
				DY = DZ = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			}break;

		case"R9":
			if (this.CubeR == 0 && Player.transform.position.x < 0.8) {
				
				K = A = this.CubeL - 1;
				A = B = this.CubeD - 1;
				B = -K;
				this.CubeL = A + 1;
				this.CubeD = B + 1;
				DX = 1;
				DY = DZ = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			} else {
				RotateNum = null;
			}break;

		case"R10":
			if (this.CubeL == 0 && Player.transform.position.z < 0.8) {
				
				K = A = this.CubeR - 1;
				A = B = this.CubeD - 1;
				B = -K;
				this.CubeR = A + 1;
				this.CubeD = B + 1;
				DZ = -1;
				DX = DY = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			}  else {
				RotateNum = null;
			}break;

		case"R11":
			if (this.CubeL == 1) {
				if (Player.transform.position.z > -0.6 && Player.transform.position.z < 0.8) {
					RotateNum = null;
					break;
				}
				
				K = A = this.CubeR - 1;
				A = B = this.CubeD - 1;
				B = -K;
				this.CubeR = A + 1;
				this.CubeD = B + 1;
				DZ = -1;
				DX = DY = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			}break;

		case"R12":
			if (this.CubeL == 2 && Player.transform.position.z > -0.6) {
				
				K = A = this.CubeR - 1;
				A = B = this.CubeD - 1;
				B = -K;
				this.CubeR = A + 1;
				this.CubeD = B + 1;
				DZ = -1;
				DX = DY = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			} else {
				RotateNum = null;
			}break;

		case"R13":
			if (this.CubeD == 2) {
				
				K = A = this.CubeR - 1;
				A = B = this.CubeL - 1;
				B = -K;
				this.CubeR = A + 1;
				this.CubeL = B + 1;
				DY = 1;
				DX = DZ = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			} else {
				RotateNum = null;
			}break;

		case"R14":
			if (this.CubeD == 1) {
				
				K = A = this.CubeR - 1;
				A = B = this.CubeL - 1;
				B = -K;
				this.CubeR = A + 1;
				this.CubeL = B + 1;
				DY = 1;
				DX = DZ = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			}  else {
				RotateNum = null;
			}break;

		case"R15":
			if (this.CubeD == 0) {
				
				K = A = this.CubeR - 1;
				A = B = this.CubeL - 1;
				B = -K;
				this.CubeR = A + 1;
				this.CubeL = B + 1;
				DY = 1;
				DX = DZ = 0;
				this.transform.parent = CubeMom.transform;
				FloorMom.transform.parent = CubeMom.transform;
				Player.transform.parent = CubeMom.transform;//EX2
				childtemp = 2;

				floortemp = PlayerController.FloorR;
				PlayerController.FloorR = PlayerController.FloorL;
				PlayerController.FloorL = 8 - floortemp;

				
				RotateNum = "Start";
			} else
				RotateNum = null;break;
				
		case"R16":
			if (this.CubeR == 0 && Player.transform.position.x < 0.8) {
				
				K = A = this.CubeD - 1;
				A = B = this.CubeL - 1;
				B = -K;
				this.CubeD = A + 1;
				this.CubeL = B + 1;
				DX = -1;
				DY = DZ = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			} else {
				RotateNum = null;
			}break;

		case"R17":
			if (this.CubeR == 1) {
				if (Player.transform.position.x > -0.6 && Player.transform.position.x < 0.8) {
					RotateNum = null;
					break;
				}
				
				K = A = this.CubeD - 1;
				A = B = this.CubeL - 1;
				B = -K;
				this.CubeD = A + 1;
				this.CubeL = B + 1;
				DX = -1;
				DY = DZ = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			}break;

		case"R18":
			if (this.CubeR == 2 && Player.transform.position.x > -0.6) {
				
				K = A = this.CubeD - 1;
				A = B = this.CubeL - 1;
				B = -K;
				this.CubeD = A + 1;
				this.CubeL = B + 1;
				DX = -1;
				DY = DZ = 0;
				this.transform.parent = CubeMom.transform;

				
				RotateNum = "Start";
			} else {
				RotateNum = null;
			}break;
		}

	}

	void OnCollisionEnter(Collision other)
	{    
		if (other.gameObject.tag == "Obstacle") {
			BeTouchedObj = null;
			moveState = false;
		}
	}
}
