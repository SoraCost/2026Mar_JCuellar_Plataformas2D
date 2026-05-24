using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 3.0f;
    [SerializeField] float jumpVelociy = 5f;

    [Header("Ground Check")]
    [SerializeField] float groundCheckDistance = 0.2f;
    [SerializeField] LayerMask groundLayerMask = Physics2D.DefaultRaycastLayers;

    [Header("Combat")]
    [SerializeField] Transform leftHit;
    [SerializeField] Transform rightHit;


    Rigidbody2D rb2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        leftHit.gameObject.SetActive(false);
        rightHit.gameObject.SetActive(false);

    }


    void Start()
    {


    }

    const float moveThreshold = 0.1f;

    void Update()
    {
        rb2D.linearVelocityX = rawMove.x * movementSpeed;
        bool isMoving = Mathf.Abs(rawMove.x) > moveThreshold;
        //animator.SetBool("isRunning", Mathf.Abs(rawMove.x) > moveThreshold);
        animator.SetBool("isRunning", isMoving);

        if (isMoving)
        {
            spriteRenderer.flipX = rawMove.x < 0f;
        }

        animator.SetBool("isGrounded", IsGrounded());
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayerMask);

        return hit.collider != null;
    }

    Vector2 rawMove;

    public void SetRawMove(Vector2 rawMove)
    {
        this.rawMove = rawMove;
    }

    internal void Jump()
    {
        if (IsGrounded())
        {
            rb2D.linearVelocityY = jumpVelociy;
        }
    }

    internal void Punch()
    {
        animator.SetTrigger("Punch");
        //animator.ResetTrigger("Punch");
    }

    const float deactivateHitDelay = 0.25f;
    public void OnAnimationPunch()
    {
        if (spriteRenderer.flipX)
        {
            leftHit.gameObject.SetActive(true);
            Invoke(nameof(DeactivateHit), deactivateHitDelay);
            Debug.Log("OnAnimationPuch Left");
        }
        else
        {
            rightHit.gameObject.SetActive(true);
            Invoke(nameof(DeactivateHit), deactivateHitDelay);
            Debug.Log("OnAnimationPuch Right");
        }
    }

    void DeactivateHit()
    {
        leftHit.gameObject.SetActive(false);
        rightHit.gameObject.SetActive(false);
    }

}
