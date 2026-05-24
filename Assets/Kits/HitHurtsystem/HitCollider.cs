using System;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HurtCollider hurtCollider = collision.GetComponent<HurtCollider>();
        hurtCollider?.NotifyHit(this);
    }
}
