using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float thrustSpeed;
	public float rotateSpeed;

	private Rigidbody2D rb;
	
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}
		
	void FixedUpdate () {
		float thrust = Input.GetAxis ("Vertical");
		float rotation = Input.GetAxis ("Horizontal");
		thrust = Mathf.Clamp (thrust, 0, 1);

		rb.rotation += (-rotation * rotateSpeed);

		Vector2 direction = new Vector2(transform.up.x, transform.up.y);
		rb.velocity = direction * thrustSpeed * thrust;
	}
}
