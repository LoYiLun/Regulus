using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private GameObject Player;
	private GameObject Player2;
	public GameObject PlayerHome;
	public GameObject PlayerStep;
	public static bool moveState = false;
	private bool OnIce = false;
	private float MoveDistance;
	public static int FloorR;
	public static int FloorL;
	public static int FloorMax;
	public static int FloorR2;
	public static int FloorL2;
	public static int FloorMax2;
	public static Vector3 MoveTarget;
	private int FTR;
	private int FTL;
	private int BodyLimit = 5;
	private Vector3 TargetTemp;
	public static bool ICanGo = true;
	public static bool OneShot = false;


	//主角身體部位
	public static string MoveType;
	public GameObject Head;
	public GameObject HandLeft;
	public GameObject HandRight;
	public GameObject FootLeft;
	public GameObject FootRight;
	public GameObject BrainA;
	public GameObject BrainB;


	//主角移動動畫的變數

	private float speed = 7.5f;
	private int i = 1;
	private bool ReverseBody = false;
	public float ShakeBody;
	private bool SoundIsPlaying;
	private AudioSource WalkSound;

	//V2
	public static bool CubeV2 = true;

	public static int Dance = 0;
	private bool KeyBoardMode;

	void Start () {
		Player = GameObject.Find("Player");
		WalkSound = GameObject.Find("WalkSound").GetComponent<AudioSource> ();

		//V2 Cube
		if (CubeController.CubeType == 2) {
			FloorL = 1;
			FloorR = 1;
			FloorMax = 100;
			MoveTarget = new Vector3 (0.922f, 2.59f, 0.982f);
			MoveDistance = 0.1f;
			//MoveDistance = 0.404f;
		} else {
			FloorL = 4;
			FloorR = 1;
			FloorMax = 8;
			MoveTarget = new Vector3(1.29f,2.566f,0);
			MoveDistance = 0.43f;
		}

	}



	void OnCollisionEnter(Collision Noway)
	{
		if (Noway.gameObject.tag == "Obstacle") {
			Debug.Log ("撞到東西囉");
			ICanGo = false;
		}

		if (Noway.gameObject.tag == "Tree") {
			Debug.Log ("撞到樹囉");
			CubeController.MovetoWall = true;
			CubeController.moveState = false;
			ObstacleController.Hit = true;
			ObstacleController.HitName = Noway.gameObject.name;
		}

		if (Noway.gameObject.tag == "Ice")
			OnIce = true;
	}

	void SavePosition(){
		TargetTemp = MoveTarget;
		FTL = FloorL;
		FTR = FloorR;
	}

	void BodyRotate()
	{
		//方法A
			switch (MoveType) {

		case"GoDance":
			ShakeBody++;
			Head.transform.RotateAround (BrainA.transform.position, transform.TransformDirection (Vector3.forward), speed / 6 * i);
			HandLeft.transform.RotateAround (BrainA.transform.position, transform.TransformDirection(Vector3.left), speed * i);
			HandRight.transform.RotateAround (BrainA.transform.position, transform.TransformDirection(Vector3.left), -speed * i);
			FootLeft.transform.RotateAround (BrainB.transform.position, transform.TransformDirection(Vector3.left), -speed * i);
			FootRight.transform.RotateAround (BrainB.transform.position, transform.TransformDirection(Vector3.left), speed * i);
			// 走路起煙特效
			if (PlayerHome.transform.childCount < 1) {
				Player2 = Instantiate (PlayerStep);
				Player2.transform.parent = PlayerHome.transform;
				Player2.transform.localPosition = Vector3.zero;
				Player2.transform.parent = null;
				Destroy (Player2, 0.5f);
			}
			break;

		case"Forward":
				ShakeBody++;
				Head.transform.RotateAround (BrainA.transform.position, Vector3.left, speed / 6 * i);
				HandLeft.transform.RotateAround (BrainA.transform.position, Vector3.forward, speed * i);
				HandRight.transform.RotateAround (BrainA.transform.position, Vector3.forward, -speed * i);
				FootLeft.transform.RotateAround (BrainB.transform.position, Vector3.forward, -speed * i);
				FootRight.transform.RotateAround (BrainB.transform.position, Vector3.forward, speed * i);
			break;

			case"Back":
			ShakeBody++;
			Head.transform.RotateAround (BrainA.transform.position, Vector3.right, speed/6 * i);
			HandLeft.transform.RotateAround (BrainA.transform.position, Vector3.back, speed * i);
			HandRight.transform.RotateAround (BrainA.transform.position, Vector3.back, -speed * i);
			FootLeft.transform.RotateAround (BrainB.transform.position, Vector3.back, -speed * i);
			FootRight.transform.RotateAround (BrainB.transform.position, Vector3.back, speed * i);
				break;

			case"Left":
			ShakeBody++;
			Head.transform.RotateAround (BrainA.transform.position, Vector3.forward, -speed/6 * i);
			HandLeft.transform.RotateAround (BrainA.transform.position, Vector3.left, speed * i);
			HandRight.transform.RotateAround (BrainA.transform.position, Vector3.left, -speed * i);
			FootLeft.transform.RotateAround (BrainB.transform.position, Vector3.left, -speed * i);
			FootRight.transform.RotateAround (BrainB.transform.position, Vector3.left, speed * i);
				break;

			case"Right":
			ShakeBody++;
			Head.transform.RotateAround (BrainA.transform.position, Vector3.back, -speed/6 * i);
			HandLeft.transform.RotateAround (BrainA.transform.position, Vector3.right, speed * i);
			HandRight.transform.RotateAround (BrainA.transform.position, Vector3.right, -speed * i);
			FootLeft.transform.RotateAround (BrainB.transform.position, Vector3.right, -speed * i);
			FootRight.transform.RotateAround (BrainB.transform.position, Vector3.right, speed * i);
				break;



		}


		//方法B
		//HandLeft.transform.Rotate(new Vector3(speed * i, 0f, 0f));
		//HandRight.transform.Rotate(new Vector3(-speed * i, 0f, 0f));



		if (ShakeBody >= BodyLimit)
			ReverseBody = true;

		if (ReverseBody == true) {
			i *= -1;
			ShakeBody= -BodyLimit;
			ReverseBody = false;
		}


	}


	void FixedUpdate () {



		//九宮格走路
		//print (FloorR);
		/*
		if (   Player.transform.position.z > -0.6f
			&& Player.transform.position.z < 1.6f
			&& Player.transform.position.z > -0.6f
			&& Player.transform.position.z > -0.6f)*/

		if (Dance == 1) {
			MoveType = "GoDance";
			BodyRotate ();
		}
		if (Input.GetMouseButton(0) == false && CubeController.moveState == false) {
			Dance = 0;
			MoveType = null;
			BodyRotate ();
		}

		if (Input.GetKey ("w") && KeyBoardMode) {
			if (OneShot == false) {
				Player.transform.rotation = Quaternion.Euler (0f, 270f, 0f);

					SavePosition ();
					MoveTarget -= new Vector3 (MoveDistance, 0f, 0f);
					FloorR += 1;
					OneShot = true;
				MoveType = "Forward";
					moveState = true;



			}
		}
			
		if (Input.GetKey ("s") && KeyBoardMode) {
			if (OneShot == false) {
				Player.transform.rotation = Quaternion.Euler (0f, 90f, 0f);

					SavePosition ();
					MoveTarget += new Vector3 (MoveDistance, 0f, 0f);
					FloorR -= 1;
					OneShot = true;
				MoveType = "Back";
					moveState = true;


			}
		}

		if (Input.GetKey ("a") && KeyBoardMode) {
			if (OneShot == false) {
				Player.transform.rotation = Quaternion.Euler (0f, 180f, 0f);

					SavePosition ();
					MoveTarget -= new Vector3 (0f, 0f, MoveDistance);
					FloorL += 1;
					OneShot = true;
				MoveType = "Left";
					moveState = true;


			}
		}

		if (Input.GetKey ("d") && KeyBoardMode) {
			if(OneShot == false){
			Player.transform.rotation = Quaternion.Euler (0f, 0f, 0f);

					SavePosition ();
					MoveTarget += new Vector3 (0f, 0f, MoveDistance);
					FloorL -= 1;
					OneShot = true;
				MoveType = "Right";
					moveState = true;


			}
		}

		if (moveState && KeyBoardMode) {

			// 腳步聲(未設定)
			if (SoundIsPlaying == false) {
				WalkSound.Play ();
				SoundIsPlaying = true;
				}



			// 到定位時停止移動
			if (Vector3.Distance (Player.transform.position, MoveTarget) < 0.01f) {
				ICanGo = true;
				OneShot = false;
				SoundIsPlaying = false;
				moveState = false;
			}

			// 不能走時後退一步(已取消)
			if (ICanGo == false) {
				/*
				MoveTarget = TargetTemp;
				FloorR = FTR;
				FloorL = FTL;*/
			}

			// 移動到設定位置
			//BodyRotate ();
			//Player.transform.position = Vector3.MoveTowards (Player.transform.position, MoveTarget, 1.2f * Time.deltaTime);

		} 
	}
}
