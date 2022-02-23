using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //declaring components
    private Rigidbody rb;
    [SerializeField] private GameObject playerCam;

    //declaring variables
    [SerializeField] private float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    private float verticalLookRotation;
    private bool grounded;
    private Vector3 smoothMoveVelocity, moveAmount;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerCam.SetActive(true);
    }

    private void Update()
    {
        lookAround();
    }

    private void lookAround()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        playerCam.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
}
