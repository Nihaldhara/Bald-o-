using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI tutoText;
    [SerializeField] private Image ShakaImage;
    [SerializeField] private Image ThumbsUpImage;

    private int currentStep = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShakaImage.enabled = false;
        ThumbsUpImage.enabled = false;
        StartCoroutine(LearnToValidate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Learn to Validate (thumbs up) => 0

    IEnumerator LearnToValidate()
    {
        tutoText.text = "To validate and move to next step, please do a thumb up.";
        ThumbsUpImage.enabled = true;
        yield return null;
    }

    // Learn to Move Level => 1
    IEnumerator LearnToMoveLevel()
    {
        tutoText.text = "Well done!!!";
        yield return new WaitForSeconds(3);
        tutoText.text = "To place the level, please grab the level handle. \n Once moved and validated, you won't be able to move it again.";
        yield return null;
    }

    // Learn to Grab People => 2
    IEnumerator LearnToGrabPeople()
    {
        tutoText.text = "Well done!!!";
        yield return new WaitForSeconds(3);
        tutoText.text = "To grab a person, please reach out and close your hand around them.";
        yield return new WaitForSeconds(2);
    }

    // Learn to Teleport in the Level => 3
    IEnumerator LearnToTeleportInLevel()
    {
        tutoText.text = "Well done!!!";
        yield return new WaitForSeconds(3);
        tutoText.text = "To teleport, pinch your fingers to the localization wanted";
        yield return new WaitForSeconds(2);
    }

    // Learn to Leave the Level => 4
    IEnumerator LearnToLeaveLevel()
    {
        tutoText.text = "Well done!!!";
        yield return new WaitForSeconds(3);
        tutoText.text = "To leave the level, please do the shaka.";
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
            tutoText.text = "Congratulations! You have completed the tutorial.";
        }
    }

    // Leave scene
    public void LeaveLevel()
    {
        Debug.Log("Shaka");
    }
}
