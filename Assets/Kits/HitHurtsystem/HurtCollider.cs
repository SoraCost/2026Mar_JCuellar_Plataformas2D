using UnityEngine;
using UnityEngine.Events;

public class HurtCollider : MonoBehaviour
{
    public UnityEvent onHitReceived;
    internal void NotifyHit(HitCollider hitCollider)
    {
        onHitReceived.Invoke();
    }
}
