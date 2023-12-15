using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text ScoreText;
    public Text winText;
    public GameObject Wall;
    public GameObject Wall2;
    public Transform teleportDestination;
    public Transform teleportDestination2;
    private Rigidbody rb;
    public int Score;

    private bool hasTeleportedThroughWall = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Score = 0;
        SetScoreText();
        winText.text = "";

        // Set walls to be inactive initially
        Wall.gameObject.SetActive(false);
        Wall2.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        // Restart level
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        // Quit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            Score = Score + 1;
            SetScoreText();

            // Check if the player has collected 3 coins
            if (Score == 3)
            {
                // Set the Wall to be active when the player collects 3 coins
                Wall.gameObject.SetActive(true);
            }
            // Check if the player has collected 6 coins
            else if (Score == 6)
            {
                // Set the Wall2 to be active when the player collects 6 coins
                Wall2.gameObject.SetActive(true);
            }
        }

        // Check if the player touches the wall and has collected 3 coins
        if (other.gameObject.CompareTag("wall") && Score >= 3)
        {
            // Teleport the player to the destination
            transform.position = teleportDestination.position;
            hasTeleportedThroughWall = true;
        }

        // Check if the player touches the wall2 and has collected 6 coins
        if (other.gameObject.CompareTag("wall2") && Score >= 6)
        {
            // Teleport the player to the destination
            transform.position = teleportDestination2.position;
        }

        if (other.gameObject.tag == "danger")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player has teleported through the wall and has left its trigger
        if (other.gameObject.CompareTag("wall") && hasTeleportedThroughWall)
        {
            // Deactivate the initial wall
            Wall.gameObject.SetActive(false);
            hasTeleportedThroughWall = false;
        }
    }

    void SetScoreText()
    {
        ScoreText.text = "Score: " + Score.ToString();

        if (Score >= 9)
        {
            winText.text = "You Win! Press R to restart or ESC to exit";
        }
    }
}
