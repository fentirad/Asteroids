using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Thrusters : IThrusters {
    public Vector2 MoveForward(float magnitude,
                        Vector2 direction,
                        Vector2 currentVelocity,
                        float maxSpeed,
                        float thrustMultiplier) {
        Vector2 velocity = new Vector2();

        magnitude = RestrictToForwardMovement(magnitude);

		if (IsAbleToAccelerate(magnitude, currentVelocity, maxSpeed)) {
			velocity = currentVelocity + (direction * thrustMultiplier * magnitude);
		}
		else {
			velocity = Vector2.ClampMagnitude(currentVelocity, maxSpeed);
		}

        return velocity;
    }

	public float Turn(float magnitude, float currentRotation, float rotationMultiplier) {
        return currentRotation + (-magnitude * rotationMultiplier);
    }

    private bool IsAbleToAccelerate(float magnitude, Vector2 currentVelocity, float maxSpeed) {
        return (magnitude > 0) && (currentVelocity.magnitude <= maxSpeed);
    }

    private float RestrictToForwardMovement(float magnitude) {
        return Mathf.Clamp (magnitude, 0, 1);
    }
}