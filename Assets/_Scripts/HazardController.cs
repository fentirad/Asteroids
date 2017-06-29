using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log("HazardController: " + coll.gameObject.name);
    }
}
