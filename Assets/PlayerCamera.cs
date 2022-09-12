﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Look Sensitivity")]
    public float sensX;
    public float sensY;

    //an empty game object that is a child of the player. we use it to turn the player when moving the camera
    [Header("Orientation Point on Player")]
    public Transform orientation;

    [Header("Player RigidBody for Speed Detection")]
    [SerializeField] Rigidbody Player;
    public float baseFOV;
    private float playerSpeed;

    //Empty variables that are filled when the mouse moves. These values are added to the camera to make it rotate.
    float xRotation;
    float yRotation;

    private void Start()
    {
        //Lock and Hide the cursor on start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Camera.main.fieldOfView = baseFOV;
    }

    private void LateUpdate()
    {
        playerSpeed = Player.velocity.magnitude;
        SpeedView();
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //apply mouse movements to the rotation variables
        //this is confusing, but it is just the way Unity does it
        yRotation += mouseX;
        xRotation -= mouseY;

        //this line clamps the mouseX movement so that the camera does not flip backwards
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //THESE LINES ARE WHAT ACTUALLY MOVES THE CAMERA
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    private void SpeedView()
    {
        if (playerSpeed <= 2)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, baseFOV, Time.deltaTime*1.4f);
        }
        if (playerSpeed > 8)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, baseFOV+10, Time.deltaTime * 1.4f);
        }
    }
}
