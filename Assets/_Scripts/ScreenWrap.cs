using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScreenWrap : IScreenWrap {
	private ScreenBounds bounds;
	private float screenHeight;
	private float screenWidth;
	private Transform[] ghosts;

	public ScreenBounds Bounds {
		get { return bounds; }
	}

	public ScreenWrap() {
		this.ghosts = new Transform[8];
	}

	public void CalculateScreenSize(Camera mainCamera, Vector3 position) {
		Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint (new Vector3(0, 0, position.z));
		Vector3 screenTopRight = mainCamera.ViewportToWorldPoint (new Vector3(1, 1, position.z));

		this.screenWidth = screenTopRight.x - screenBottomLeft.x;
		this.screenHeight = screenTopRight.y - screenBottomLeft.y;

		this.bounds.top = screenHeight / 2;
		this.bounds.bottom = screenHeight / -2;
		this.bounds.right = screenWidth / 2;
		this.bounds.left = screenWidth / -2;
	}

	public Vector3 WrapObject(Vector3 objectPosition, Quaternion objectRotation) {
		bool isVisible = this.ObjectOnScreen(objectPosition);

		if (isVisible) {
			return objectPosition;
		}

		if (objectPosition.x > Bounds.right || 
			objectPosition.x < Bounds.left) {
			
			objectPosition = this.SwapEntities(objectPosition, objectRotation);
		}

		if (objectPosition.y > Bounds.top || 
			objectPosition.y < Bounds.bottom) {
			
			objectPosition = this.SwapEntities(objectPosition, objectRotation);
		}

		return objectPosition;
	}

	public bool ObjectOnScreen(Vector3 position) {
		return 	position.x < Bounds.right && 
				position.x > Bounds.left &&
				position.y < Bounds.top && 
				position.y > Bounds.bottom;
	}

	public void CreateGhostEntities(Transform baseTransform) {
		for( int i = 0; i < ghosts.Length; i++ ) {

			Transform ghost = GameObject.Instantiate<Transform>(baseTransform, Vector3.zero, Quaternion.identity);
			Collider2D ghostCollider = ghost.GetComponent<Collider2D> ();

			ghost.name = baseTransform.name + "-ghost-" + i;
			ghostCollider.enabled = false;
			GameObject.DestroyImmediate(ghost.GetComponent<ScreenWrapController>());

			if (ghost.GetComponent<WeaponController>() != null) {
				GameObject.DestroyImmediate(ghost.GetComponent<WeaponController>());
			}

			ghosts[i] = ghost;
		}
	}

	public Vector3 SwapEntities(Vector3 basePosition, Quaternion baseRotation) {
		foreach(Transform ghost in ghosts) {

			if (ObjectOnScreen(ghost.position)) {
				basePosition = ghost.position;
				break;
			}
		}

		PositionGhostEntities(basePosition, baseRotation);

		return basePosition;
	}

	public void PositionGhostEntities(Vector3 basePosition, Quaternion baseRotation) {
		var ghostPosition = basePosition;

		// We're positioning the ghosts clockwise behind the edges of the screen.
		// Let's start with the far right.
		ghostPosition.x = basePosition.x + screenWidth;
		ghostPosition.y = basePosition.y;
		ghosts[0].position = ghostPosition;

		// Bottom-right
		ghostPosition.x = basePosition.x + screenWidth;
		ghostPosition.y = basePosition.y - screenHeight;
		ghosts[1].position = ghostPosition;

		// Bottom
		ghostPosition.x = basePosition.x;
		ghostPosition.y = basePosition.y - screenHeight;
		ghosts[2].position = ghostPosition;

		// Bottom-left
		ghostPosition.x = basePosition.x - screenWidth;
		ghostPosition.y = basePosition.y - screenHeight;
		ghosts[3].position = ghostPosition;

		// Left
		ghostPosition.x = basePosition.x - screenWidth;
		ghostPosition.y = basePosition.y;
		ghosts[4].position = ghostPosition;

		// Top-left
		ghostPosition.x = basePosition.x - screenWidth;
		ghostPosition.y = basePosition.y + screenHeight;
		ghosts[5].position = ghostPosition;

		// Top
		ghostPosition.x = basePosition.x;
		ghostPosition.y = basePosition.y + screenHeight;
		ghosts[6].position = ghostPosition;

		// Top-right
		ghostPosition.x = basePosition.x + screenWidth;
		ghostPosition.y = basePosition.y + screenHeight;
		ghosts[7].position = ghostPosition;

		// All ghost Entities should have the same rotation as the main Entity
		for(int i = 0; i < 8; i++) {
			ghosts[i].rotation = baseRotation;
		}
	}
}

[System.Serializable]
public struct ScreenBounds {
	public float top;
	public float bottom;
	public float left;
	public float right;

}