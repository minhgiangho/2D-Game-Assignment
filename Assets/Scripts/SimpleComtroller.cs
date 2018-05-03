using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Animator))]
public class SimpleComtroller : MonoBehaviour
{
    public float Speed = 7f;
//    public float Accel = 3f;
    private Animator animator;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Vector2 input;

    public bool IsJumping;
    public float JumpSpeed = 8f;

    private float rayCastLengthCheck = 0.005f;

    // allow the raycast check start from outside of hero sprite
    private float width;
    private float height;

    public float JumpDurationThreshold = 0.25f;
    private float jumpDuration;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.2f;
    }


    void Update()
    {
//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            _animator.SetBool("isFlash", true);
//        }
//        else if (Input.GetKeyDown(KeyCode.T))
//        {
//            _animator.SetBool("isAttack", true);
//        }
//        else if (Input.GetKeyDown(KeyCode.F))
//        {
//            _animator.SetBool("isRun", true);
//        }

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Jump");

        if (input.x > 0)
        {
            sr.flipX = false;
        }
        else if (input.x < 0)
        {
            sr.flipX = true;
        }

        if (PlayerIsOnGround() && IsJumping == false)
        {
            if (input.y > 0f)
            {
                IsJumping = true;
            }
        }

        // control the threshold of jumping, thus player can only jump up to a certain height
        if (jumpDuration > JumpDurationThreshold)
        {
            input.y = 0f;
        }

        if (IsJumping && jumpDuration < JumpDurationThreshold)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
        }
    }

    private void FixedUpdate()
    {
        // for accel
//        float xVelocity = 0f;
//        if (input.x < 0.01f)
//        {
//            xVelocity = 0f;
//        }
//        else
//        {
//            xVelocity = input.x * Speed;
//        }
//
//        rb.AddForce(new Vector2(xVelocity, 0));
//        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
        float x = transform.position.x + input.x * Time.deltaTime * Speed;
        rb.MovePosition(new Vector2(x, transform.position.y));
    }

    public bool PlayerIsOnGround()
    {
        bool groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height),
            -Vector2.up, rayCastLengthCheck);
        bool groundCheck2 = Physics2D.Raycast(
            new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height),
            -Vector2.up, rayCastLengthCheck);
        bool groundCheck3 = Physics2D.Raycast(
            new Vector2(transform.position.x - (width - 0.2f), transform.position.y - height),
            -Vector2.up, rayCastLengthCheck);
        return groundCheck1 || groundCheck2 || groundCheck3;
    }
}