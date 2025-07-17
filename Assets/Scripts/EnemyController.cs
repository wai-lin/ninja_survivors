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
    [SerializeField] private int experienceToGive;

    [SerializeField] private float pushBackTime;
    private float _pushBackCounter;

    private Vector3 _direction;
    [SerializeField] private GameObject destroyEffect;

    void FixedUpdate()
    {
        // stop enemies if player is dead
        if (!PlayerController.Instance.gameObject.activeSelf)
        {
            rigidBody.velocity = Vector2.zero;
            return;
        }

        // push back on damage taken
        if (_pushBackCounter > 0)
        {
            _pushBackCounter -= Time.fixedDeltaTime;
            // pushed back ~~vvv
            if (movementSpeed > 0) movementSpeed = -movementSpeed;
            // follow player ~~vvv
            if (_pushBackCounter <= 0) movementSpeed = Mathf.Abs(movementSpeed);
        }

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
        DamageNumberController.Instance.CreateNumber(incomingDamage, transform.position);

        // trigger push back when damage is taken
        _pushBackCounter = pushBackTime;

        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(
                destroyEffect,
                transform.position,
                transform.rotation
            );
            PlayerController.Instance.GetExperience(experienceToGive);
        }
    }
}