using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed;
    public Vector3 playerMoveDirection;

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        playerMoveDirection = new Vector3(inputX, inputY).normalized;

        animator.SetFloat("MoveX", inputX);
        animator.SetFloat("MoveY", inputY);

        if (playerMoveDirection == Vector3.zero)
            animator.SetBool("Moving", false);
        else
            animator.SetBool("Moving", true);
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(
            playerMoveDirection.x * movementSpeed,
            playerMoveDirection.y * movementSpeed
        );
    }
}