using UnityEngine;

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

    void Start()
    {
        PlayerPosition = transform.position;
    }

    void Update()
    {

        // move forward
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);

        // get left and right key from player input
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLeft();
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            MoveRight();

        // placement of player base on the lanes
        float x = (currentLane - 1) * laneDistance;
        PlayerPosition = new Vector3(x, transform.position.y, transform.position.z);

        //change lane smoothly
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

}
