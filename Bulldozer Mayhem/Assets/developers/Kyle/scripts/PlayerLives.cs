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

        if (CompareTag("Player1"))
            playerName = PlayerPrefs.GetString("Player1Name", "Player 1");
        else if (CompareTag("Player2"))
            playerName = PlayerPrefs.GetString("Player2Name", "Player 2");
        UpdateLivesUI();
    }
    public void SpawnPlayerAtStart()
    {
        SpawnOnRandomPlatform(); 
    }

    private void Update()
    {
        if (transform.position.y < 50)
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
        Transform platform = GameManager.Instance.GetUniqueRandomPlatform();

        if (platform != null)
        {
            Vector3 spawnPosition = platform.transform.position + new Vector3(0, 0, 0);
            transform.position = spawnPosition;
            Debug.Log($"Spawning player on: {platform.name} at {spawnPosition}");
        }
        else
        {
            Debug.LogWarning("No platform available for spawn. Using Vector3.zero.");
            transform.position = Vector3.zero;
        }

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.rotation = Quaternion.identity;
        }

        gameObject.SetActive(true);
        StartCoroutine(InvincibilityPhase());
    }

    private IEnumerator InvincibilityPhase()
    {
        isInvincible = true;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("ParentLayer"), LayerMask.NameToLayer("ParentLayer"), true);

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

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("ParentLayer"), LayerMask.NameToLayer("ParentLayer"), false);
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
