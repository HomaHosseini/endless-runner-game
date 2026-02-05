using UnityEngine;
using TMPro;
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

    void Start()
    {
        PlayerPosition = transform.position;
        startingZ = transform.position.z;
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
        Debug.Log("Collision");
        // Check if we hit an object tagged as "Obstacle"
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
        }
    }
    void Die()
    {
        isDead = true;
        forwardSpeed = 0;
        Debug.Log("Game Over!");
        // You can trigger a UI panel here

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Disable kinematic so gravity can pull the player down
            rb.useGravity = true;

            // Optional: Add a small "kick" backward so the fall looks more natural
            rb.AddForce(new Vector3(0, 0f, -1f), ForceMode.Impulse);

        }
    }

}
