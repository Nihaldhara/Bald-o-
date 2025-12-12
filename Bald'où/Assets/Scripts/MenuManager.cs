using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start the game by loading the main scene
    public void StartGame()
    {
        SceneManager.LoadScene("Fuuusion");
    }

    public void StartTuto()
    {
        SceneManager.LoadScene("Tuto");
    }
}
