using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed;
    public Vector3 playerMoveDirection;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        playerMoveDirection = new Vector3(inputX, inputY).normalized;

        animator.SetFloat("MoveX", inputX);
        animator.SetFloat("MoveY", inputY);

        bool moving = playerMoveDirection != Vector3.zero;
        animator.SetBool("Moving", moving);
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(
            playerMoveDirection.x * movementSpeed,
            playerMoveDirection.y * movementSpeed
        );
    }
}