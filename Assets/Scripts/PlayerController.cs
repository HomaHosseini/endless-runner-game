using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// automatic move forward and change line using A and D or the left and right arrows
/// </summary>
public class PlayerController : MonoBehaviour
{

    public float forwardSpeed = 8f;
    public float laneDistance = 2f;
    public float laneChangeSpeed = 10f;

    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right
    private Vector3 PlayerPosition;

    public TextMeshProUGUI scoreText;
    private bool isDead = false;
    private float startingZ;

    //high score

    public TextMeshProUGUI highScoreText;
    private float highScore = 0;

    void Start()
    {
        PlayerPosition = transform.position;
        startingZ = transform.position.z;

        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        UpdateHighScoreText();
    }

    void Update()
    {
        if (isDead) return;

        // 1. Calculate and Update Score
        UpdateScore();

        // 2. Move Forward
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);

        // 3. Handle Input
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLeft();
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            MoveRight();

        // 4. Smooth Lane Changing
        float targetX = (currentLane - 1) * laneDistance;
        PlayerPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        Vector3 smoothed = Vector3.Lerp(transform.position, PlayerPosition, Time.deltaTime * laneChangeSpeed);
        transform.position = new Vector3(smoothed.x, transform.position.y, transform.position.z);
    }

    void MoveLeft()
    {
        if (currentLane > 0) currentLane--;
    }

    void MoveRight()
    {
        if (currentLane < 2) currentLane++;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Obstacle") && !isDead)
        {
            Die();
        }
    }




    void UpdateScore()
    {
        if (scoreText != null)
        {
            // Score is the total distance traveled on the Z axis
            float distance = transform.position.z - startingZ;
            scoreText.text = "Score: " + Mathf.FloorToInt(distance / 10).ToString();

            if ((distance / 10) > highScore)
            {
                highScore = distance / 10;
                PlayerPrefs.SetFloat("HighScore", highScore);
                UpdateHighScoreText();
            }
        }
    }

    void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "Best: " + Mathf.FloorToInt(highScore).ToString();
        }
    }

    void Die()
    {
        isDead = true;
        forwardSpeed = 0;

        float finalScore = (transform.position.z - startingZ) / 10;
        PlayerPrefs.SetFloat("LastScore", finalScore);


        float highScore = PlayerPrefs.GetFloat("HighScore", 0);
        if (finalScore > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", finalScore);
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Disable kinematic so gravity can pull the player down
            rb.useGravity = true;

            // Optional: Add a small "kick" backward so the fall looks more natural
            rb.AddForce(new Vector3(0, 0f, -1f), ForceMode.Impulse);

        }

        StartCoroutine(LoadGameOverAfterDelay(1.5f));
    }
    IEnumerator LoadGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameOver");
    }

}
