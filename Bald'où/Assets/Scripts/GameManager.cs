using System;
using UnityEngine;

public enum GameState {SUCCESS, FAILURE, PLAYING}

public class GameManager : MonoBehaviour
{
    private GameState gameState = GameState.PLAYING;
    public Transform xrOrigin;
    
    [SerializeField] private int maxHealth;
    private int currentHealth;
    
    [SerializeField] private GameObject[] levels;
    private int currentLevel = 0;

    public bool isZoomedIn = false;

    [SerializeField] private GameObject environment;
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
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        environment.transform.position = new Vector3(xrOrigin.position.x, xrOrigin.position.y + 8, xrOrigin.position.z + 15);
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

    public void ZoomingOut()
    {
        if (isZoomedIn)
        {
            xrOrigin.localScale = new Vector3(10.0f, 10.0f, 10.0f);
            xrOrigin.position = new Vector3(0f, 0f, -15f);
            isZoomedIn = false;
        }
    }

    /*public void AnchorLevel()
    {
        levelHandle.SetActive(false);
        targets.SetActive(true);
        characters.SetActive(true);
    }*/
}
