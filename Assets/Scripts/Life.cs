using System;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    [SerializeField] float startLife = 1f;
    [SerializeField] float damagePerHit = 0.3f;

    public UnityEvent<float, float> onLifeChanged;
    public UnityEvent<float> onLifeDepleted;

    HurtCollider hurtCollider;

    float currentLife;

    [SerializeField] bool debugReceiveDamage;
    private void OnValidate()
    {
        if (debugReceiveDamage)
        {
            debugReceiveDamage = false;
            OnHitReceived();
        }
    }

    private void Awake()
    {
        hurtCollider = GetComponent<HurtCollider>();

        currentLife = startLife;
    }

    private void OnEnable()
    {
        hurtCollider.onHitReceived.AddListener(OnHitReceived);
    }

    private void OnDisable()
    {
        hurtCollider.onHitReceived.RemoveListener(OnHitReceived);
    }

    private void OnHitReceived()
    {
        if(currentLife > 0)
        {
            currentLife -= damagePerHit;
            onLifeChanged.Invoke(currentLife, startLife);
            if (currentLife <= 0f)
            {
                currentLife = 0f;
                onLifeDepleted.Invoke(startLife);
            }

        }
    }

    internal void Restart()
    {
        currentLife = startLife;
        onLifeChanged.Invoke(currentLife, startLife);
    }
}
