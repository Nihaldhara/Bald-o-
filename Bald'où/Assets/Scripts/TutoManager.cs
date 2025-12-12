using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI tutoText;
    [SerializeField] private Image ShakaImage;
    [SerializeField] private Image ThumbsUpImage;
    [SerializeField] private Image StopImage;
    
    private int currentStep = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShakaImage.enabled = false;
        ThumbsUpImage.enabled = false;
        StopImage.enabled = false;

        StartCoroutine(TheGame());
    }

    //Quick Game Intro => 0
    IEnumerator TheGame()
    {
        tutoText.text = "Welcome to Bald'où! A game where the baldies are trying to invade our world, and only YOU can stop them";
        yield return new WaitForSeconds(3);
    }
    
    // Learn to Grab People => 1
    IEnumerator LearnToGrabPeople()
    {
        tutoText.text = "To grab a person, reach out and grab or pinch them with your right hand.";
        yield return new WaitForSeconds(2);
    }

    // Learn to Teleport in the Level => 2
    IEnumerator LearnToTeleportInLevel()
    {
        tutoText.text = "Well done!!!";
        yield return new WaitForSeconds(3);
        tutoText.text = "To zoom in and teleport onto the table, use your left hand: pinch to aim and release to teleport";
        yield return new WaitForSeconds(2);
    }

    // Learn to Leave the Level => 3
    IEnumerator LearnToLeaveLevel()
    {
        tutoText.text = "Well done!!!";
        yield return new WaitForSeconds(3);
        tutoText.text = "To go back to your original size, do the shaka with your right hand!";
        ShakaImage.enabled = true;
        yield return new WaitForSeconds(2);
    }

    // Next Step
    public void NextStep()
    {
        if (currentStep == 0)
        {
            currentStep++;
            StartCoroutine(LearnToGrabPeople());
        }
        else if (currentStep == 1)
        {
            currentStep++;
            StartCoroutine(LearnToTeleportInLevel());
        }
        else if (currentStep == 2)
        {
            currentStep++;
            StartCoroutine(LearnToLeaveLevel());
        }
        else if (currentStep == 3)
        {
            ShakaImage.enabled = false;
            StopImage.enabled = true;
            tutoText.text = "Congratulations! You have completed the tutorial.\n Place your palm up to go back to the main menu.";
        }
    }
}
