using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float thrustSpeed;
	public float rotateSpeed;
	public float maxSpeed;
	public IThrusters thrusters;

	private Rigidbody2D rb;
	
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		SetThrusters(new Thrusters());
	}
		
	void FixedUpdate () {
		float thrust = Input.GetAxisRaw ("Vertical");
		float rotation = Input.GetAxisRaw ("Horizontal");
		Vector2 direction = new Vector2(transform.up.x, transform.up.y);

		rb.velocity = thrusters.MoveForward(thrust, direction, rb.velocity, maxSpeed, thrustSpeed);
		rb.rotation = thrusters.Turn(rotation, rb.rotation, rotateSpeed);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log("PlayerController: " + coll.gameObject.tag);
		if (coll.gameObject.tag.Equals("Hazard")) {
			//Send message to destroy this gameobject as well as all ghosts
			//Destroy(gameObject);
		}
    }

	void SetThrusters(IThrusters thrusters) {
		this.thrusters = thrusters;
	}
}
