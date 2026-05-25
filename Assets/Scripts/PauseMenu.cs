using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private InputActionReference pauseAction;

    private bool isPaused = false;

    private void Awake()
    {
        // Conectamos el botón para que ejecute la función TogglePause cuando se presione
        pauseAction.action.performed += TogglePause;
    }

    private void OnEnable()
    {
        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        pauseAction.action.Disable();
    }

    // Esta función decide si debe pausar o despausar
    private void TogglePause(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    // Función pública para que también puedas llamarla desde un botón de "Continuar" en la pantalla
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Oculta el menú
        Time.timeScale = 1f;          // El tiempo vuelve a la normalidad (1 = 100% de velocidad)
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Muestra el menú
        Time.timeScale = 0f;          // Congela el tiempo del juego (0 = 0% de velocidad)
        isPaused = true;
    }
    public void Restart()
    {
        // ¡OJO! Es vital devolver el tiempo a 1 antes de cargar el nivel.
        // Si no lo hacemos, el nivel cargará, pero todo estará congelado.
        Time.timeScale = 1f;

        // Le pedimos a Unity que busque el nombre de la escena en la que estamos y la vuelva a cargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Exit()
    {
        //Prueba
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
