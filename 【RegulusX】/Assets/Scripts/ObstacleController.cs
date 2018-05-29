using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

	private GameObject Obstacle;
	public GameObject Player;
	public float ObstacleToPlayer = 0.0005f ;
	private float OB_Distance;

	private Animator anim;
	private AudioSource TreeSound;
	public bool SoundIsPlaying;
	public bool HitFinish = false;
	public static bool Hit = false;
	public static string HitName;

	public int AnimNum = 0;
	public bool AnimPlus;

  
	void Start () {
		Obstacle = gameObject;
		Player = GameObject.Find ("Player");
		anim = Obstacle.GetComponent<Animator>();
		TreeSound = GameObject.Find("TreeSound").GetComponent<AudioSource> ();

	}


	void Update () {

		OB_Distance =( Mathf.Abs (Obstacle.transform.position.x - Player.transform.position.x)) + (Mathf.Abs (Obstacle.transform.position.z - Player.transform.position.z));


		if (AnimPlus) {
			AnimNum++;
			Debug.Log (AnimNum);
			AnimPlus = false;
		}

		if(AnimNum == 0)
			anim.SetBool ("BeHit", false);

		if (Hit) {
			if (anim.runtimeAnimatorController != null) {
				
				anim = GameObject.Find (HitName).GetComponent<Animator> ();

				//PlayerController.moveState = false;
				anim.SetBool ("BeHit", true);
				if(SoundIsPlaying == false){
				TreeSound.Play();
					SoundIsPlaying = true;
				}if (HitFinish) {


					//CubeController.moveState = true;
					//PlayerController.moveState = true;
					SoundIsPlaying = false;
					AnimNum = 0;
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
