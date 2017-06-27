using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float thrustSpeed;
	public float rotateSpeed;
	public float maxSpeed;

	private Rigidbody2D rb;
	
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}
		
	void FixedUpdate () {
		float thrust = Input.GetAxisRaw ("Vertical");
		float rotation = Input.GetAxis ("Horizontal");
		thrust = Mathf.Clamp (thrust, 0, 1);
		Vector2 direction = new Vector2(transform.up.x, transform.up.y);

		if (thrust > 0 && rb.velocity.magnitude <= maxSpeed) {
			rb.velocity += (direction * thrustSpeed * thrust);
		}
		else {
			rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
		}
		rb.rotation += (-rotation * rotateSpeed);
	}
}
