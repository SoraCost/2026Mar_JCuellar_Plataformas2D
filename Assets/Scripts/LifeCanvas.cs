using System;
using UnityEngine;
using UnityEngine.UI;

public class LifeCanvas : MonoBehaviour
{
    [SerializeField] Life life;
    [SerializeField] Image mask;

    private void OnEnable()
    {
        life.onLifeChanged.AddListener(onLifeChanged);
        //life.onLifeDepleted.AddListener(onLifeDepleted);
    }

    private void OnDisable()
    {
        life.onLifeChanged.RemoveListener(onLifeChanged);
        //life.onLifeDepleted.AddListener(onLifeDepleted);
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
