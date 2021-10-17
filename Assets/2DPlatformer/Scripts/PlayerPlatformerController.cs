using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : MonoBehaviour
{
    [Tooltip("player speed")]
    [SerializeField] private float RunSpeed = 40f;

    private bool isJumping = false;
    private float horizontalMovement = 0f;

    private CharacterController2D controller;

    private void OnEnable()
    {
        controller = GetComponent<CharacterController2D>();

    }

    private void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        horizontalMovement = Input.GetAxisRaw("Horizontal") * RunSpeed * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMovement);
        if (isJumping)
        {
            controller.Jump();
        }

        isJumping = false;
    }
}
