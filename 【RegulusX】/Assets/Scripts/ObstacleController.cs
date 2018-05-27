using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

	private GameObject Obstacle;
	public GameObject Player;
	public float ObstacleToPlayer = 0.0005f ;
	private float OB_Distance;

	private Animator anim;
	public bool HitFinish = false;
	public static bool Hit = false;
	public static string HitName;


	void Start () {
		Obstacle = gameObject;
		Player = GameObject.Find ("Player");
		anim = Obstacle.GetComponent<Animator>();

	}


	void Update () {

		OB_Distance =( Mathf.Abs (Obstacle.transform.position.x - Player.transform.position.x)) + (Mathf.Abs (Obstacle.transform.position.z - Player.transform.position.z));


		if (Hit) {
			if (anim.runtimeAnimatorController != null) {
				Debug.Log ("002");
				anim = GameObject.Find (HitName).GetComponent<Animator> ();
				PlayerController.moveState = false;
				anim.SetBool ("BeHit", true);

				if (HitFinish) {
					anim.SetBool ("BeHit", false);
					PlayerController.moveState = true;
					HitFinish = false;
					Hit = false;

				}
			}
		}

		
		if (OB_Distance < ObstacleToPlayer
			&& Obstacle.transform.position.y >= Player.transform.position.y
			&& Obstacle.transform.position.x >= Player.transform.position.x
			&& Obstacle.transform.position.z >= Player.transform.position.z
			) 
		{
			Obstacle.GetComponent<Renderer> ().material = Resources.Load ("Ghost", typeof(Material)) as Material;
		} else {
			Obstacle.GetComponent<Renderer> ().material = Resources.Load ("Tree", typeof(Material)) as Material;
		}
	}





}
