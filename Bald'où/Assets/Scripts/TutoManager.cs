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

    [SerializeField] private GameObject levelHandle;
    [SerializeField] private GameObject targets;
    [SerializeField] private GameObject characters;
    
    private int currentStep = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShakaImage.enabled = false;
        ThumbsUpImage.enabled = false;
        StopImage.enabled = false;

        StartCoroutine(LearnToValidate());
    }

    // Learn to Validate (thumbs up) => 0
    IEnumerator LearnToValidate()
    {
        tutoText.text = "To validate and move to the next step, do a thumbs up with your right hand.";
        ThumbsUpImage.enabled = true;
        yield return null;
    }

    // Learn to Move Level => 1
    IEnumerator LearnToMoveLevel()
    {
        tutoText.text = "Well done!!!";
        yield return new WaitForSeconds(3);
        tutoText.text = "To move the table to a comfortable position, grab the level handle with your right hand. \n After you validate this position, you won't be able to move it again.";
        yield return null;
    }

    // Learn to Grab People => 2
    IEnumerator LearnToGrabPeople()
    {
        AnchorLevel();
        tutoText.text = "Well done!!!";
        yield return new WaitForSeconds(3);
        tutoText.text = "To grab a person, reach out and grab or pinch them with your right hand.";
        yield return new WaitForSeconds(2);
    }

    // Learn to Teleport in the Level => 3
    IEnumerator LearnToTeleportInLevel()
    {
        tutoText.text = "Well done!!!";
        yield return new WaitForSeconds(3);
        tutoText.text = "To zoom in and teleport onto the table, use your left hand: pinch to aim and release to teleport";
        yield return new WaitForSeconds(2);
    }

    // Learn to Leave the Level => 4
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
            ThumbsUpImage.enabled = false;
            currentStep++;
            StartCoroutine(LearnToMoveLevel());
        }
        else if (currentStep == 1)
        {
            currentStep++;
            StartCoroutine(LearnToGrabPeople());
        }
        else if (currentStep == 2)
        {
            currentStep++;
            StartCoroutine(LearnToTeleportInLevel());
        }
        else if (currentStep == 3)
        {
            currentStep++;
            StartCoroutine(LearnToLeaveLevel());
        }
        else if (currentStep == 4)
        {
            ShakaImage.enabled = false;
            StopImage.enabled = true;
            tutoText.text = "Congratulations! You have completed the tutorial.\n Place your palm up to go back to the main menu.";
        }
    }

    public void GoToMenu()
    {
        StopImage.enabled = false;
        SceneManager.LoadScene("Menu");
    }
    
    private void AnchorLevel()
    {
        levelHandle.SetActive(false);
        targets.SetActive(true);
        characters.SetActive(true);
    }
    
    //TODO: Utiliser une main pour grab et l'autre main pour tp
}
