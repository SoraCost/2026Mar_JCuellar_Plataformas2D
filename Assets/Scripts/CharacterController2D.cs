using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 3.0f;
    [SerializeField] float jumpVelociy = 3f;

    [Header("Ground Check")]
    [SerializeField] float groundCheckDistance = 1f;
    [SerializeField] LayerMask groundLayerMask = Physics2D.DefaultRaycastLayers;

    [Header("Combat")]
    [SerializeField] Transform leftHit;
    [SerializeField] Transform rightHit;

    Rigidbody2D rb2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [Header("Respawn")]
    [SerializeField] bool Enemy;

    Life life;
    public bool isDead = false;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        leftHit.gameObject.SetActive(false);
        rightHit.gameObject.SetActive(false);

        life = GetComponent<Life>();

    }

    private void OnEnable()
    {
        life.onLifeDepleted.AddListener(OnLifeDepleted);

        isDead = false;
    }

    private void OnLifeDepleted(float arg0)
    {
        //gameObject.SetActive(false);
        //if (Enemy != true)
        //{
        //    animator.SetTrigger("isDeath");
        //    Invoke(nameof(Resurrect), 3f);
        //}

        if (Enemy != true)
        {
            StartCoroutine(DeathSequence());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private System.Collections.IEnumerator DeathSequence()
    {
        isDead = true; // Bloqueamos el movimiento
        Death(); // Activamos el trigger "isDeath" en el Animator
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        Invoke(nameof(Resurrect), 3f);
    }

    void Resurrect()
    {
        isDead = false;
        gameObject.SetActive(true);
        life.Restart();
    }


    void Start()
    {


    }

    const float moveThreshold = 0.1f;

    void Update()
    {
        if (isDead) return;

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
        Vector2 direccionSuelo;

        // 1. Si la gravedad es negativa (-1), disparamos el rayo hacia ARRIBA
        if (rb2D.gravityScale < 0)
        {
            direccionSuelo = Vector2.up;
        }
        // 2. Si la gravedad es normal (1), disparamos el rayo hacia ABAJO
        else
        {
            direccionSuelo = Vector2.down;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direccionSuelo, groundCheckDistance, groundLayerMask);

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
            // Si la gravedad está invertida, el salto debe tener fuerza NEGATIVA (para saltar hacia abajo)
            if (rb2D.gravityScale < 0)
            {
                rb2D.linearVelocityY = -jumpVelociy;
            }
            // Si la gravedad es normal, el salto tiene fuerza POSITIVA (salto normal hacia arriba)
            else
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

    internal void Death()
    {
        animator.SetTrigger("isDeath");
    }

    void DeactivateHit()
    {
        leftHit.gameObject.SetActive(false);
        rightHit.gameObject.SetActive(false);
    }

}
