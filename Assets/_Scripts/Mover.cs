using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speed;

	void Start() {
		Rigidbody2D moverRigidbody = gameObject.GetComponent<Rigidbody2D> ();

		moverRigidbody.velocity = transform.up * speed;
	}
}
