using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float initialRotationSpeed = 10f;
    public float rotationSpeedIncreaseRate = 0.1f;
    public RotatingDirection direction;
    private float currentRotationSpeed;
    private float desiredDirection;


    private void Start()
    {
        currentRotationSpeed = initialRotationSpeed;
        SetDirection();
    }
     

    public void RotateShaft()
    {
        float deltaTime = Time.deltaTime;
        currentRotationSpeed += rotationSpeedIncreaseRate * Mathf.Log(currentRotationSpeed + 1) * deltaTime;
        transform.Rotate(Vector3.right*desiredDirection, currentRotationSpeed * deltaTime);

        
    
    }

    void SetDirection()
    {
        switch (direction)
        {
            case RotatingDirection.Left:
                desiredDirection = -1f;
                break;
            case RotatingDirection.Right:
                desiredDirection = 1f;
                break;
           
        }
    }
}

public enum RotatingDirection
{
    Left,
    Right
}
