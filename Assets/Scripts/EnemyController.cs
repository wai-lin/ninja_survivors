using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float movementSpeed;
    private Vector3 _direction;

    void FixedUpdate()
    {
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
}