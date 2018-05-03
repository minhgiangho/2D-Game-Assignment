using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class of physics object
/// </summary>
public class PhysicsObject : MonoBehaviour
{
    public float MinGroundNormalY = .65f;
    public float GravityModlfier = 1f;

    /// <summary>
    /// the incomming input from outside ths class where the object is trying to move
    /// </summary>
    protected Vector2 targetVelocity;

    protected bool grounded;

    // provide a fake initial value for the sake of following calculation
    protected Vector2 groundNormal = new Vector2(0f, 0f);
    protected Vector2 velocity;
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float MIN_MOVE_DISTANCE = 0.001f;

    // for not get stuck in other collider
    protected const float SHELL_RADIUS = 0.01f;

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    /// <summary>
    /// will be called at every frame, check input and update animation
    /// </summary>
    protected virtual void ComputeVelocity()
    {
        
    }

    private void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    private void FixedUpdate()
    {
        velocity += GravityModlfier * Physics2D.gravity * Time.deltaTime;
        // handle horizontal movement
        velocity.x = targetVelocity.x;
        // handle the vertical movement
        grounded = false;

        // store direction that trying to move along the ground
        Vector2 deltaPosition = velocity * Time.deltaTime;
        // handle vertical and horizontal movement separately
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, false);
        move = Vector2.up * deltaPosition.y;
        Movement(move, true);
    }

    // if we use physic component to handle this stuff, we will lose detail game feel
    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;
        if (distance > MIN_MOVE_DISTANCE)
        {
            // In the next frame, are there sth gonna to overlap with collider?
            // check overlap before moving
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + SHELL_RADIUS);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                // the objects going to overlap with physical collider
                hitBufferList.Add(hitBuffer[i]);
            }

            // find the minimum distance to the hit objects which are on 'move' direction
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                // check the player is on ground or not(or on slope as well)
                if (currentNormal.y > MinGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - SHELL_RADIUS;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }
}