using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleComtroller : MonoBehaviour
{
    public float speed = 14f;
    private Animator animator;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Vector2 input;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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
    }
}