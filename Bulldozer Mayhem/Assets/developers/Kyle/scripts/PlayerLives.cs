using UnityEngine;
using TMPro;

public class PlayerLives : MonoBehaviour
{
    public int lives = 3; // Number of lives
    public TextMeshProUGUI livesText; // UI text to display lives
    private string playerName; // Store player name
    private Vector3 originalPosition; // Store original spawn position
    private Quaternion originalRotation; // Store original rotation

    void Start()
    {
        // Store the player's original position and rotation for respawning
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // Assign the correct name from PlayerPrefs based on the GameObject's tag
        if (gameObject.CompareTag("Player1"))
        {
            playerName = PlayerPrefs.GetString("Player1Name", "Player 1");
        }
        else if (gameObject.CompareTag("Player2"))
        {
            playerName = PlayerPrefs.GetString("Player2Name", "Player 2");
        }

        UpdateLivesUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathBoundary")) // Check if the player collides with a death boundary
        {
            LoseLife();
        }
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            UpdateLivesUI();

            if (lives <= 0)
            {
                // Player is out of lives, inform the GameManager
                FindObjectOfType<GameManager>().PlayerLost(gameObject);
            }
            else
            {
                // Respawn the player at the original position
                Respawn();
            }
        }
    }

    void Respawn()
    {
        transform.position = originalPosition; // Reset position
        transform.rotation = originalRotation; // Reset rotation

        // Reset physics to prevent weird behavior after respawn
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Stop movement
            rb.angularVelocity = Vector3.zero; // Stop rotation
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = playerName + ": Lives: " + lives;
        }
    }
}
