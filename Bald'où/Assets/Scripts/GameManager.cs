using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    SUCCESS,
    FAILURE,
    PLAYING
}

public class GameManager : MonoBehaviour
{
    private GameState gameState = GameState.PLAYING;
    public Transform xrOrigin;

    [SerializeField] private int maxHealth;
    [SerializeField] private Image[] hearts;
    private int currentHealth;

    public bool LeftThumbsUpActive;
    public bool RightThumbsUpActive;

    [SerializeField] private GameObject[] levels;
    private int currentLevel = 0;

    public bool isZoomedIn = false;

    [SerializeField] private GameObject environment;

    [SerializeField] private GameObject successUI;
    [SerializeField] private GameObject failureUI;

    [SerializeField] private GameObject applause;
    [SerializeField] private GameObject cross;
    [SerializeField] private GameObject thumbsUp;

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
        environment.transform.position =
            new Vector3(xrOrigin.position.x, xrOrigin.position.y + 8, xrOrigin.position.z + 15);

        LeftThumbsUpActive = false;
        RightThumbsUpActive = false;
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

    //Add brief "thumbs up" animation
    public void BaldieGrabbed()
    {
        if (currentLevel + 1 < levels.Length)
            StartCoroutine(ThumbsUpAnimation());
        MoveToNextLevel();
    }

    IEnumerator ThumbsUpAnimation()
    {
        thumbsUp.SetActive(true);
        yield return new WaitForSeconds(5);
        thumbsUp.SetActive(false);
    }

    //Add brief "cross" animation
    public void LureGrabbed()
    {
        hearts[currentHealth - 1].enabled = false;
        currentHealth--;
        StartCoroutine(CrossAnimation());
    }

    IEnumerator CrossAnimation()
    {
        cross.SetActive(true);
        yield return new WaitForSeconds(5);
        cross.SetActive(false);
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

    public void ThumbsUp()
    {
        if (LeftThumbsUpActive && RightThumbsUpActive)
        {
            Debug.Log("Both Thumbs Up - Returning to Menu");
            GoToMenu();
        }
    }

    public void LeftThumbUp(bool isUp)
    {
        if (isUp)
        {
            LeftThumbsUpActive = true;
            Debug.Log("Left Thumb Up");
        }
        else
        {
            LeftThumbsUpActive = false;
            Debug.Log("Left Thumb Down");
        }
    }

    public void RightThumbUp(bool isUp)
    {
        if (isUp)
        {
            RightThumbsUpActive = true;
            Debug.Log("Right Thumb Up");
        }
        else
        {
            RightThumbsUpActive = false;
            Debug.Log("Right Thumb Down");
        }
    }
}