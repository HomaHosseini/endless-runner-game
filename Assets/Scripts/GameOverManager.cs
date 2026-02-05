using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// show player score and the best score and function for restart
/// </summary>
public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {

        float lastScore = PlayerPrefs.GetFloat("LastScore", 0);
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);

        lastScoreText.text = "Your Score: " + Mathf.FloorToInt(lastScore).ToString();
        highScoreText.text = "Best Score: " + Mathf.FloorToInt(highScore).ToString();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
}