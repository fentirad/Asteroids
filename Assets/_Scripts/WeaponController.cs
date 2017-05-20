using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
	public float fireRate;
	public GameObject shot;
	public Transform shotSpawn;

	private float nextFire;

	void Start () {
		nextFire = 0;
	}
	
	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			Fire ();
		}
	}

	void Fire() {
		nextFire = Time.time + fireRate;
		Instantiate<GameObject>(shot, shotSpawn.position, shotSpawn.rotation);
	}
}
