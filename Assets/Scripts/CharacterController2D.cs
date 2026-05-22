using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3.0f;

    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference punch;

    Rigidbody2D rb2D;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        move.action.performed += Onmove;
        move.action.started += Onmove;
        move.action.canceled += Onmove;

        jump.action.performed += OnJump;

        punch.action.performed += OnPunch;
    }
    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        punch.action.Enable();
    }

    void Start()
    {
        
    }
    void Update()
    {
        rb2D.linearVelocityX = rawMove.x * movementSpeed;
    }
    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        punch.action.Disable();
    }

    Vector2 rawMove;
    private void Onmove(InputAction.CallbackContext ctx)
    {
        rawMove = ctx.ReadValue<Vector2>();
    }
    private void OnJump(InputAction.CallbackContext ctx)
    {
        throw new NotImplementedException();
    }
    private void OnPunch(InputAction.CallbackContext cxt)
    {
        throw new NotImplementedException();
    }
}
