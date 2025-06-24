using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PlayerLives : MonoBehaviour
{
    public int lives = 3;
    public TextMeshProUGUI livesText;

    private string playerName;
    private Renderer playerRenderer;
    private Collider playerCollider;
    private Rigidbody rb;
    private bool isInvincible = false;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        playerCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        // Get the player name from PlayerPrefs
        if (CompareTag("Player1"))
            playerName = PlayerPrefs.GetString("Player1Name", "Player 1");
        else if (CompareTag("Player2"))
            playerName = PlayerPrefs.GetString("Player2Name", "Player 2");

        UpdateLivesUI();
        SpawnOnRandomPlatform(); // Start on a random platform
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathBoundary") && !isInvincible)
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
                FindObjectOfType<GameManager>().PlayerLost(gameObject);
            }
            else
            {
                Respawn();
            }
        }
    }

    void Respawn()
    {
        SpawnOnRandomPlatform();
    }

    private void SpawnOnRandomPlatform()
    {
        List<GameObject> platforms = GameManager.Instance.GetActivePlatforms();

        if (platforms.Count > 0)
        {
            GameObject platform = platforms[Random.Range(0, platforms.Count)];
            transform.position = platform.transform.position + new Vector3(0, 3, 0);
        }
        else
        {
            transform.position = Vector3.zero;
        }

        transform.rotation = Quaternion.identity;

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.rotation = Quaternion.identity;
            rb.Sleep();
            rb.WakeUp();
        }

        gameObject.SetActive(true);
        StartCoroutine(InvincibilityPhase());
    }

    private IEnumerator InvincibilityPhase()
    {
        isInvincible = true;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"), true);

        float blinkDuration = 5f;
        float blinkInterval = 0.25f;
        float timer = 0f;

        while (timer < blinkDuration)
        {
            if (playerRenderer != null)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
            }

            timer += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        if (playerRenderer != null)
        {
            playerRenderer.enabled = true;
        }

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"), false);
        isInvincible = false;
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = $"{playerName}: Lives: {lives}";
        }
    }
}
