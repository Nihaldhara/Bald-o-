using System;
using UnityEngine;

public enum GameState {SUCCESS, FAILURE, PLAYING}

public class GameManager : MonoBehaviour
{
    private GameState gameState = GameState.PLAYING;
    
    [SerializeField] private int maxHealth;
    private int currentHealth;
    
    [SerializeField] private GameObject[] levels;
    private int currentLevel = 0;

    [SerializeField] private GameObject levelHandle;
    [SerializeField] private GameObject targets;
    [SerializeField] private GameObject characters;
    
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            gameState = GameState.FAILURE;
        }
    }

    private void MoveToNextLevel()
    {
        if (currentLevel + 1 < levels.Length)
        {
            levels[currentLevel].gameObject.SetActive(false);
            currentLevel++;
            levels[currentLevel].gameObject.SetActive(true);
        }
        else
        {
            levels[currentLevel].gameObject.SetActive(false);
            gameState = GameState.SUCCESS;
        }
    }

    public void BaldieGrabbed()
    {
        MoveToNextLevel();
    }

    public void LureGrabbed()
    {
        currentHealth--;
        Debug.Log("Lure Grabbed");
    }

    public void AnchorLevel()
    {
        levelHandle.SetActive(false);
        targets.SetActive(true);
        characters.SetActive(true);
    }
}
