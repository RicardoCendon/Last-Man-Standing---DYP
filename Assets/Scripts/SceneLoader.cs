using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Juego"); 
    }

    public void QuitGame()
    {
        Debug.Log("Salir del juego");
        Application.Quit(); 
    }
}