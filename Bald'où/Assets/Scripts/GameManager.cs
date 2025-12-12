using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState {SUCCESS, FAILURE, PLAYING}

public class GameManager : MonoBehaviour
{
    private GameState gameState = GameState.PLAYING;
    public Transform xrOrigin;
    
    [SerializeField] private int maxHealth;
    [SerializeField] private Image[] hearts = new Image[5];
    private int currentHealth;
    
    [SerializeField] private GameObject[] levels;
    private int currentLevel = 0;

    public bool isZoomedIn = false;

    [SerializeField] private GameObject environment;

    [SerializeField] private GameObject successUI;
    [SerializeField] private GameObject applause;
    [SerializeField] private GameObject failureUI;
    
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
        successUI.SetActive(false);
        failureUI.SetActive(false);
        
        applause.SetActive(false);
        
        currentHealth = maxHealth;
        environment.transform.position = new Vector3(xrOrigin.position.x, xrOrigin.position.y + 8, xrOrigin.position.z + 15);

        hearts = new Image[maxHealth];
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            gameState = GameState.FAILURE;
            failureUI.SetActive(true);
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
            successUI.SetActive(true);
            applause.SetActive(true);
        }
    }

    public void BaldieGrabbed()
    {
        MoveToNextLevel();
    }

    public void LureGrabbed()
    {
        currentHealth--;
        hearts[currentHealth].enabled = false;
        Debug.Log("Lure Grabbed");
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
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
}
