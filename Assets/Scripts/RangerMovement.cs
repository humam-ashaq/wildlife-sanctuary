using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement = new Vector2(
            Keyboard.current.leftArrowKey.isPressed ? -1 :
            Keyboard.current.rightArrowKey.isPressed ? 1 : 0,
            Keyboard.current.downArrowKey.isPressed ? -1 :
            Keyboard.current.upArrowKey.isPressed ? 1 : 0
        );

        animator.SetBool("isWalking", movement != Vector2.zero);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
