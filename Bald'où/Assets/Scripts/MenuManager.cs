using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start the game by loading the main scene
    // CHANGE THE NAME OF THE SCENE
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void StartTuto()
    {
        SceneManager.LoadScene("Tuto");
    }
}
