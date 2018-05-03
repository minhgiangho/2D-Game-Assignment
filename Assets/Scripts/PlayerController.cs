using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float MaxSpeed = 7f;

    // speed after jump
    public float JumpTakeOffSpeed = 7f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = JumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            // allow player cancel their jumping
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * .5f;
            }
        }

        // check whether need to flip the sprite
        bool flipSprite = spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f);
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        
        // jump anim
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Math.Abs(velocity.x)/MaxSpeed);
        
        
        targetVelocity = move * MaxSpeed;
    }
}