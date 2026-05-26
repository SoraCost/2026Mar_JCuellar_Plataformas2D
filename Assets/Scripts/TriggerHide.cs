using System.Collections;
using UnityEngine;

public class TriggerHide : MonoBehaviour
{
    [Header("Objetos que van a aparecer")]
    [SerializeField] GameObject[] objetosAMostrar;

    [Header("Objetos que van a desaparecer")]
    [SerializeField] GameObject[] objetosAOcultar;

    [Header("Objetos del nivel")]
    [SerializeField] float tiempoDeRetraso = 2f;
    [SerializeField] GameObject[] objetoNivelMostar;
    [SerializeField] GameObject[] objetoNivelOcultar;

    [Header("Configuración")]
    [SerializeField] bool onOnce = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos si el objeto que entró al trigger tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            // 1. Recorremos y ACTIVAMOS los objetos de la primera lista
            foreach (GameObject obj in objetosAMostrar)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }

            // 2. Recorremos y DESACTIVAMOS los objetos de la segunda lista
            foreach (GameObject obj in objetosAOcultar)
            {
                if (obj != null)
                {
                    obj.SetActive(false); // Aquí está la clave para apagarlos
                }
            }

            StartCoroutine(TemporizadorNivel());

            // 3. Si queremos que esto solo ocurra una vez, desactivamos este trigger
            if (onOnce)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator TemporizadorNivel()
    {
        // 1. Esperamos el tiempo definido
        yield return new WaitForSeconds(tiempoDeRetraso);

        // 2. Pasado el tiempo, ACTIVAMOS los objetos
        foreach (GameObject obj in objetoNivelMostar)
        {
            if (obj != null) obj.SetActive(true);
        }

        // 3. Pasado el tiempo, OCULTAMOS los objetos
        foreach (GameObject obj in objetoNivelOcultar)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

}