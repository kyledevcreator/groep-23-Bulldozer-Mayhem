using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerLives : MonoBehaviour
{
    public int lives = 3;
    public TextMeshProUGUI livesText;

    private string playerName;
    private Renderer playerRenderer;
    private Collider playerCollider;
    private Rigidbody rb;
    private bool isInvincible = false;
    private bool imPlayer1;

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
        if (GetComponent<MovementPlayerOne>().player == MovementPlayerOne.PlayerEnum.Player1)
        {
            Respawn(true);
            imPlayer1 = true;
        }
        else
        {
            Respawn(false);
            imPlayer1 = false;
        }
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
                Respawn(imPlayer1);
            }
        }
    }

    void Respawn(bool isPlayer1)
    {
        if (isPlayer1)
        {
            transform.position = new Vector3(0.87f, 74.57f, -7.11f);
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else
        {
            transform.position = new Vector3(11, 75, 5);
            transform.rotation = Quaternion.Euler(0, 225, 0);
        }

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
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
