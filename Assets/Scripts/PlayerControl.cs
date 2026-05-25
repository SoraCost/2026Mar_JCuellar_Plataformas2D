using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    CharacterController2D characterController2D;
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference punch;

    Life life;

    
    private void Awake()
    {
        characterController2D = GetComponent<CharacterController2D>();

        move.action.performed += Onmove;
        move.action.started += Onmove;
        move.action.canceled += Onmove;

        jump.action.performed += OnJump;

        punch.action.performed += OnPunch;

        //life = GetComponent<Life>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        punch.action.Enable();

    }


    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        punch.action.Disable();
    }

    //private void Update()
    //{

    //}

    Vector2 rawMove;
    private void Onmove(InputAction.CallbackContext ctx)
    {
        rawMove = ctx.ReadValue<Vector2>();
        characterController2D.SetRawMove(rawMove);
    }
    private void OnJump(InputAction.CallbackContext ctx)
    {
        characterController2D.Jump();
    }
    private void OnPunch(InputAction.CallbackContext cxt)
    {
        characterController2D.Punch(); //Golpear
    }
}
