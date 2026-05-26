using System;
using UnityEngine;
using UnityEngine.UI;

public class LifeCanvas : MonoBehaviour
{
    [SerializeField] Life life;
    [SerializeField] Image mask;

    private void Awake()
    {
        life.onLifeChanged.AddListener(onLifeChanged);
    }

    //private void OnEnable()
    //{
    //    life.onLifeChanged.AddListener(onLifeChanged);
    //    //life.onLifeDepleted.AddListener(onLifeDepleted);
    //}

    //private void OnDisable()
    //{
    //    life.onLifeChanged.RemoveListener(onLifeChanged);
    //    //life.onLifeDepleted.AddListener(onLifeDepleted);
    //}
        private void OnDestroy()
    {
        // Nos desuscribimos solo cuando el objeto es destruido por completo (con Destroy),
        // no cuando simplemente se oculta temporalmente (SetActive(false)).
        if (life != null)
        {
            life.onLifeChanged.RemoveListener(onLifeChanged);
        }
    }

    private void onLifeChanged(float currentLife, float startLife)
    {
        mask.fillAmount = currentLife / startLife;
    }

    //private void onLifeDepleted(float startLife)
    //{
    //    throw new NotImplementedException();
    //}
}
