using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerMovement : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    //public static PlayerController Instance;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask obstacleLayer;

    private PlayerControl playerControl;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator anim;
    public SpriteRenderer sprite;
    private bool facingLeft = false;


    private void Awake()
    {
        playerControl = new PlayerControl();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControl.Movement.Move.ReadValue<Vector2>();

        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.y);
    }

    private void Move()
    {

        Vector2 targetPos = rb.position + movement * (moveSpeed * Time.fixedDeltaTime);

        RaycastHit2D hit = Physics2D.BoxCast(rb.position, boxCollider.size, 0f, movement.normalized, (moveSpeed * Time.fixedDeltaTime), obstacleLayer);

        if (hit.collider == null)
        {
            rb.MovePosition(targetPos);
        }
    }

    private void AdjustPlayerFacingDirection()
    {
        if (movement.x != 0f)
        {
            sprite.flipX = movement.x < 0f;
            FacingLeft = true;
        }
        else if (movement.y != 0f)
        {
            sprite.flipX = false;
            FacingLeft = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Nabrak: " + col.gameObject.name);
    }

}
