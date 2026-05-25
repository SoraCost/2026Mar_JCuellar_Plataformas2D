using System;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    [SerializeField] float attackDistance = 2f;
    [SerializeField] Transform target;
    CharacterController2D characterController2D;

    CharacterController2D targetController;

    internal void SetTarget(Transform target)
    {
        this.target = target;

        // En el momento que detectamos al target, "sacamos" su componente CharacterController2D
        if (this.target != null)
        {
            targetController = this.target.GetComponent<CharacterController2D>();
        }
    }

    private void Awake()
    {
        characterController2D = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        Vector2 rawMove = Vector2.zero;
        if (target)
        {
            if (transform.position.x > target.position.x)
            {
                rawMove = Vector2.left;
            }
            else
            {
                rawMove = Vector2.right;
            }
            if (Mathf.Abs(target.transform.position.x - transform.position.x) < attackDistance)
            {
                rawMove = Vector2.zero;
                characterController2D.Punch(); //Golpear
            }
        }
        characterController2D.SetRawMove(rawMove);

    }

}
