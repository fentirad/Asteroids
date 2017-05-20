using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapController : MonoBehaviour
{
	private Renderer[] renderers;
	private bool isWrappingX = false;
	private bool isWrappingY = false;
	private bool isVisible;
	private float screenHeight;
	private float screenWidth;
	private Camera mainCamera;
	private Transform[] ghosts;

	void Start ()
	{
		renderers = gameObject.GetComponentsInChildren<Renderer> ();
		mainCamera = Camera.main;
		ghosts = new Transform[8];
		CalculateScreenSize ();
		CreateGhostEntities ();
		PositionGhostEntities ();
	}

	void FixedUpdate ()
	{
		ScreenWrap ();
	}
		
	void ScreenWrap()
	{
		//0,0 is middle of screen
		float screenTop = screenHeight / 2;
		float screenBottom = -screenTop;
		float screenRight = screenWidth / 2;
		float screenLeft = -screenRight;
		

		isVisible = CheckRenderers ();

		if (isVisible)
		{
			isWrappingX = false;
			isWrappingY = false;
			return;
		}

		if (isWrappingX && isWrappingY) {
			return;
		}

		Vector3 newPosition = transform.position;

		if (newPosition.x > screenRight || newPosition.x < screenLeft)
		{
			newPosition.x = -newPosition.x;
			isWrappingX = true;
			SwapEntities ();
		}

		if (newPosition.y > screenTop || newPosition.y < screenBottom)
		{
			newPosition.y = -newPosition.y;
			isWrappingY = true;
			SwapEntities ();
		}

	}

	bool CheckRenderers()
	{
		foreach(Renderer renderer in renderers)
		{
			if (renderer.isVisible)
			{
				return true;
			}
		}
		return false;
	}

	void CalculateScreenSize()
	{
		Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint (new Vector3(0, 0, transform.position.z));
		Vector3 screenTopRight = mainCamera.ViewportToWorldPoint (new Vector3(1, 1, transform.position.z));

		screenWidth = screenTopRight.x - screenBottomLeft.x;
		screenHeight = screenTopRight.y - screenBottomLeft.y;
	}

	void CreateGhostEntities()
	{
		for(int i = 0; i < 8; i++)
		{
			Transform ghost = Instantiate(transform, Vector3.zero, Quaternion.identity) as Transform;
			Collider2D ghostCollider = ghost.GetComponent<Collider2D> ();

			ghost.name = transform.name + "-ghost-" + i;
			ghostCollider.enabled = false;
			DestroyImmediate(ghost.GetComponent<ScreenWrapController>());
			if (ghost.GetComponent<WeaponController>() != null)
			{
				DestroyImmediate(ghost.GetComponent<WeaponController>());
			}
			
			ghosts[i] = ghost;
		}
	}

	void PositionGhostEntities()
	{
		var ghostPosition = transform.position;

		// We're positioning the ghosts clockwise behind the edges of the screen.
		// Let's start with the far right.
		ghostPosition.x = transform.position.x + screenWidth;
		ghostPosition.y = transform.position.y;
		ghosts[0].position = ghostPosition;

		// Bottom-right
		ghostPosition.x = transform.position.x + screenWidth;
		ghostPosition.y = transform.position.y - screenHeight;
		ghosts[1].position = ghostPosition;

		// Bottom
		ghostPosition.x = transform.position.x;
		ghostPosition.y = transform.position.y - screenHeight;
		ghosts[2].position = ghostPosition;

		// Bottom-left
		ghostPosition.x = transform.position.x - screenWidth;
		ghostPosition.y = transform.position.y - screenHeight;
		ghosts[3].position = ghostPosition;

		// Left
		ghostPosition.x = transform.position.x - screenWidth;
		ghostPosition.y = transform.position.y;
		ghosts[4].position = ghostPosition;

		// Top-left
		ghostPosition.x = transform.position.x - screenWidth;
		ghostPosition.y = transform.position.y + screenHeight;
		ghosts[5].position = ghostPosition;

		// Top
		ghostPosition.x = transform.position.x;
		ghostPosition.y = transform.position.y + screenHeight;
		ghosts[6].position = ghostPosition;

		// Top-right
		ghostPosition.x = transform.position.x + screenWidth;
		ghostPosition.y = transform.position.y + screenHeight;
		ghosts[7].position = ghostPosition;

		// All ghost Entities should have the same rotation as the main Entity
		for(int i = 0; i < 8; i++)
		{
			ghosts[i].rotation = transform.rotation;
		}
	}

	void SwapEntities()
	{
		foreach(Transform ghost in ghosts)
		{
			if (ghost.position.x < screenWidth && ghost.position.x > -screenWidth &&
				ghost.position.y < screenHeight && ghost.position.y > -screenHeight)
			{
				transform.position = ghost.position;

				break;
			}
		}

		PositionGhostEntities();
	}
}
