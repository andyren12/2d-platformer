using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX;
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float jumpForce = 14;

    private enum animationState { isIdle, isRunning, isJumping, isFalling };

    [SerializeField] private AudioSource jumpSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveSpeed * dirX, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSound.Play();
        }

        handleAnimation();
    }

    private void handleAnimation()
    {
        animationState state;
        if (dirX > 0f)
        {
            state = animationState.isRunning;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = animationState.isRunning;
            sprite.flipX = true;
        }
        else
        {
            state = animationState.isIdle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = animationState.isJumping;
        } else if (rb.velocity.y < -0.1f)
        {
            state = animationState.isFalling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
