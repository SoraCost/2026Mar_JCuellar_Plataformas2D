using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenEnemies;

    [Header("Límites")]
    [Tooltip("Cantidad máxima a generar. Pon 0 si quieres que sea infinito.")]
    [SerializeField] int maxEnemies = 3;

    // Lista para rastrear los enemigos creados por este generador en particular
    private List<GameObject> enemigosCreados = new List<GameObject>();

    private void OnEnable()
    {
        StartCoroutine(GenerateEnemies());
    }

    IEnumerator GenerateEnemies()
    {
        int spawnCount = 0; // Iniciamos nuestro contador en cero cada vez que se enciende el generador

        // El ciclo continuará SI el límite es 0 (infinito) O SI el contador es menor al límite
        while (maxEnemies <= 0 || spawnCount < maxEnemies)
        {
            yield return new WaitForSeconds(timeBetweenEnemies);
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.GetComponent<AIControl>().SetTarget(target);

            // Agregamos el nuevo enemigo recién instanciado a nuestra lista
            enemigosCreados.Add(newEnemy);

            // Sumamos 1 al contador
            spawnCount++;
        }
    }

    private void OnDisable()
    {
        foreach (GameObject enemigo in enemigosCreados)
        {
            // Verificamos que el enemigo siga existiendo. 
            // Es vital porque el jugador pudo haberlo destruido/derrotado antes de que el generador se apagara.
            if (enemigo != null)
            {
                Destroy(enemigo);
            }
        }

        // Vaciamos la lista para que arranque en limpio por si alguna vez vuelves a encender el generador
        enemigosCreados.Clear();
    }
}
