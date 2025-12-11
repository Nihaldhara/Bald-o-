using UnityEngine;

public enum GameState {SUCCESS, FAILURE, PLAYING}

public class GameManager : MonoBehaviour
{
    private GameState gameState = GameState.PLAYING;
    
    [Header("Player Health")]
    [SerializeField] private int maxHealth;
    private int currentHealth = 5;

    [SerializeField] private GameObject[] levels;
    private int currentLevel = 0;
    
    public bool baldieGrabbed = false;
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Failure...");
            gameState = GameState.FAILURE;
        }

        if (baldieGrabbed && currentLevel < levels.Length)
        {
            Debug.Log("Next Level");
            MoveToNextLevel();
        }

        if (baldieGrabbed && currentLevel >= levels.Length)
        {
            Debug.Log("Success!");
            gameState = GameState.SUCCESS;
        }
    }

    private void MoveToNextLevel()
    {
        baldieGrabbed = false;
        
        levels[currentLevel].gameObject.SetActive(false);
        currentLevel++;
        levels[currentLevel].gameObject.SetActive(true);
    }

    public void GrabRight()
    {
        baldieGrabbed = true;
    }

    public void GrabWrong()
    {
        currentHealth--;
    }
}
