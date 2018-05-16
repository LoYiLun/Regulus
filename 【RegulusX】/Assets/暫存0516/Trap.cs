using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	public GameObject trap;
	public GameObject ps;
	public Transform target;
	Transform myTransform;


	// Use this for initialization
	void Start () {
		ps = GameObject.Find ("Knight");
		myTransform = transform;
		ps.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {






		myTransform.rotation = Quaternion.Slerp(myTransform.rotation,Quaternion.LookRotation(target.position - myTransform.position), 3*Time.deltaTime);

	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other.name);
		if (other.GetComponent<Collider> ().gameObject.name == "Player") {
			trap.SetActive (false);
			ps.SetActive (true);

		}

	}
}
