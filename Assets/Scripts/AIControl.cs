using UnityEngine;

public class AIControl : MonoBehaviour
{
    [SerializeField] Transform target;
    CharacterController2D characterController2D;

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
        }
        characterController2D.SetRawMove(rawMove);
    }

}
