using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float health;

    private Vector3 _direction;
    [SerializeField] private GameObject destroyEffect;

    void Start()
    {
        // to account for the FixedUpdate time frame size of 50
        health *= 50;
    }

    void FixedUpdate()
    {
        // stop enemies if player is dead
        if (!PlayerController.Instance.gameObject.activeSelf)
        {
            rigidBody.velocity = Vector2.zero;
            return;
        }

        ;

        // enemy faces towards player
        bool isPlayerOnRightSide = PlayerController.Instance.transform.position.x
                                   > transform.position.x;
        spriteRenderer.flipX = isPlayerOnRightSide;

        // enemy moves towards player
        _direction = (
            PlayerController.Instance.transform.position - transform.position
        ).normalized;
        rigidBody.velocity = new Vector2(
            _direction.x * movementSpeed,
            _direction.y * movementSpeed
        );
    }

    void OnCollisionStay2D(Collision2D other)
    {
        bool isCollidedWithPlayer = other.gameObject.CompareTag("Player");
        if (isCollidedWithPlayer) PlayerController.Instance.TakeDamage(damage);
    }

    public void TakeDamage(float incomingDamage)
    {
        health -= incomingDamage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(
                destroyEffect,
                transform.position,
                transform.rotation
            );
        }
    }
}