// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public interface IThrusters {
    Vector2 MoveForward(float magnitude,
                        Vector2 direction,
                        Vector2 currentVelocity,
                        float maxSpeed,
                        float thrustMultiplier);
	float Turn(float magnitude, float currentRotation, float rotationMultiplier);
}