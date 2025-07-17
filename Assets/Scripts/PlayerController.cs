using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int Moving = Animator.StringToHash("Moving");

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed;
    public Vector3 playerMoveDirection;
    public float playerMaxHealth;
    public float playerCurrentHealth;

    private bool _isImmune;
    [SerializeField] private float immunityDuration;
    [SerializeField] private float immunityTimer;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        UIController.Instance.UpdateHealthSlider();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        playerMoveDirection = new Vector3(inputX, inputY).normalized;

        animator.SetFloat(MoveX, inputX);
        animator.SetFloat(MoveY, inputY);

        bool moving = playerMoveDirection != Vector3.zero;
        animator.SetBool(Moving, moving);

        // set immunity on game starts
        if (immunityTimer > 0)
            immunityTimer -= Time.deltaTime;
        else
            _isImmune = false;
    }

    void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(
            playerMoveDirection.x * movementSpeed,
            playerMoveDirection.y * movementSpeed
        );
    }

    public void TakeDamage(float damage)
    {
        if (_isImmune) return;

        _isImmune = true;
        immunityTimer = immunityDuration;

        playerCurrentHealth -= damage;
        UIController.Instance.UpdateHealthSlider();
        if (playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
    }
}