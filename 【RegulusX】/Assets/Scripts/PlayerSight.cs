using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour {

	private GameObject Obstacle;
	public GameObject Player;
	public float ObstacleToPlayer = 1f ;

	// Use this for initialization
	void Start () {
		Obstacle = gameObject;
		Player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {



		
		if (Mathf.Abs (Obstacle.transform.position.x - Player.transform.position.x) + Mathf.Abs (Obstacle.transform.position.z - Player.transform.position.z) < ObstacleToPlayer
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
