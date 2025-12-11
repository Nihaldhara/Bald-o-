using UnityEngine;

public enum GameState {SUCCESS, FAILURE, PLAYING}

public class GameManager : MonoBehaviour
{
    private GameState gameState = GameState.PLAYING;
    
    [SerializeField] private int maxHealth;
    private int currentHealth = 5;
    
    [SerializeField] private GameObject[] levels;
    private int currentLevel = 0;
    
    private bool baldieGrabbed = false;

    [SerializeField] private GameObject LevelHandle;
    [SerializeField] private GameObject Targets;
    [SerializeField] private GameObject Characters;
    
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
    
    

    //A changer, il faut que ce soit faisable à chaque changement de niveau
    //Donc en gros il faudra passer le game object parent et tout manipuler à l'intérieur
    public void AnchorLevel()
    {
        LevelHandle.SetActive(false);
        Targets.SetActive(true);
        Characters.SetActive(true);
    }
}
