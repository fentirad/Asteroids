using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapController : MonoBehaviour {
	public IScreenWrap screenWrap;

	void Awake() {
		SetScreenWrap(new ScreenWrap());
	}

	void Start () {
		screenWrap.CalculateScreenSize(Camera.main, transform.position);
		screenWrap.CreateGhostEntities(transform);
		screenWrap.PositionGhostEntities(transform.position, transform.rotation);
	}

	void FixedUpdate () {
		transform.position = screenWrap.WrapObject(transform.position, transform.rotation);
	}

	void SetScreenWrap(IScreenWrap screenWrap) {
		this.screenWrap = screenWrap;
	}
}
