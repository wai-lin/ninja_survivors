using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObjectDestroy : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        Destroy(
            gameObject,
            animator.GetCurrentAnimatorStateInfo(0).length
        );
    }
}