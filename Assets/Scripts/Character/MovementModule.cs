using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementModule : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform rotationTransform;
    [SerializeField] private float movementSpeed;
    private Vector3 moveDirection;

    public void MoveCharacter(Vector3 direction)
    {
        moveDirection.x = direction.x;
        moveDirection.z = direction.z;

        float tempMultiplier = 1;

        controller.Move(((transform.right * moveDirection.x) + (transform.forward * moveDirection.z)) * Time.deltaTime * movementSpeed * tempMultiplier);
        RotateCharacter(direction);
        
        
    }

    public void RotateCharacter(Vector3 direction)
    {
        float yAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        rotationTransform.rotation = Quaternion.Euler(0, yAngle , 0);
    }
}
