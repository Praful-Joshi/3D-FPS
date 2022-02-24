using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    //declaring components
    private Rigidbody rb;
    [SerializeField] private GameObject playerCam;
    private PhotonView pv;

    //declaring variables
    [SerializeField] private float mouseSensitivity, sprintSpeed, walkSpeed, smoothTime, jumpVelocity = 6f, fallMultiplier = 2.5f, lowJumpMultiplier = 2f;
    private float verticalLookRotation;
    private bool grounded;
    private Vector3 moveDir, smoothMoveVelocity, moveAmount;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        pv = this.GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(!pv.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }

    private void Update()
    {
        if (!pv.IsMine) return;
        lookAround();
        getInput();
        jump();
    }

    private void FixedUpdate()
    {
        if (!pv.IsMine) return;
        move();
    }

    private void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = Vector3.up * jumpVelocity;
        }

        if (rb.velocity.y < 0) //falling
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void move()
    {
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    private void getInput()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    private void lookAround()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        playerCam.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = collision.gameObject.CompareTag("Floor");
    }

    private void OnCollisionStay(Collision collision)
    {
        grounded = collision.gameObject.CompareTag("Floor");
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = !collision.gameObject.CompareTag("Floor");
    }
}
