using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float startTime;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI timeText; // Référence à un composant Text dans l'interface utilisateur

    void Start()
    {
        startTime = Time.time; // Enregistre le temps de départ
    }

    void Update()
    {
        float timeElapsed = Time.time - startTime;
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Affiche le temps au format MM:SS
    }

    public void CalculateScore()
    {
        float timeElapsed = Time.time - startTime;
        score = Mathf.Max(0, 1000 - Mathf.FloorToInt(timeElapsed * 10)); // Exemple de calcul de score basé sur le temps
        Debug.Log("Score: " + score);

        PlayerPrefs.SetInt("FinalScore", score); // Sauvegarde le score final
    }
}
