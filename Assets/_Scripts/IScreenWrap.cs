using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScreenWrap {
	ScreenBounds Bounds { get; }

	void CalculateScreenSize(Camera mainCamera, Vector3 position);
	Vector3 UpdatePosition(Vector3 objectPosition, Quaternion objectRotation);
	bool ObjectOnScreen(Vector3 position);
	void CreateGhostEntities(Transform baseTransform);
	Vector3 SwapEntities(Vector3 basePosition, Quaternion baseRotation);
	void PositionGhostEntities(Vector3 basePosition, Quaternion baseRotation);
}