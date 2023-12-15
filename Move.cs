using UnityEngine;

public class DangerCubeMovement : MonoBehaviour
{
    public float moveDistance = 1.0f; // Set the maximum distance the cube should move
    public float moveSpeed = 2.0f;    // Set the speed of the movement

    private Vector3 initialPosition;
    private bool movingRight = true;

    void Start()
    {
        // Store the initial position of the cube
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position based on the oscillating motion
        float movement = moveSpeed * Time.deltaTime;

        if (movingRight)
        {
            transform.Translate(Vector3.right * movement);
        }
        else
        {
            transform.Translate(Vector3.left * movement);
        }

        // Check if the cube has moved the maximum distance to the right
        if (transform.position.x >= initialPosition.x + moveDistance)
        {
            movingRight = false;
        }
        // Check if the cube has moved the maximum distance to the left
        else if (transform.position.x <= initialPosition.x - moveDistance)
        {
            movingRight = true;
        }
    }
}