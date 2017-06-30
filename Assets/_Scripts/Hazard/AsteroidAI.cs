using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//simple hazard ai: just move at some velocity
public class AsteroidAI : MonoBehaviour {

	public float speed;

	void Start () {
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D> ();

		rb.rotation = Random.Range(0f, 360f);


		rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * speed;
	}
	
	void FixedUpdate () {

	}
}
