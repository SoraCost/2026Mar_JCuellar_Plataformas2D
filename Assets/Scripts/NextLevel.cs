using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [Header("Configuración del Nivel")]
    [Tooltip("Escribe exactamente el nombre de la escena a la que quieres ir (respetando mayúsculas)")]
    [SerializeField] string nombreDelSiguienteNivel = "Nivel2";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos si quien tocó la meta es el jugador
        if (collision.CompareTag("Player"))
        {
            Debug.Log("¡Nivel completado! Cargando: " + nombreDelSiguienteNivel);

            // Cargamos la nueva escena por su nombre
            SceneManager.LoadScene(nombreDelSiguienteNivel);
        }
    }
}
