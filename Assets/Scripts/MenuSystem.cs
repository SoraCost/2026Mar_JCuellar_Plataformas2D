using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class MenuSystem : MonoBehaviour
{
    [SerializeField] public string SceneName = "SampleScene";
    public void Play()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void Exit()
    {
        //Prueba
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
