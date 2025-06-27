using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerLives : MonoBehaviour
{
    public int lives = 1;
    public TextMeshProUGUI livesText;
    [SerializeField] private GameStatus gameStatus;

    private string playerName;
    private Renderer playerRenderer;
    private Rigidbody rb;
    private bool isInvincible = false;
    private bool imPlayer1;

    private GameObject otherPlayer;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();

        if (CompareTag("Player1"))
        {
            playerName = gameStatus.player1Name;
            otherPlayer = GameObject.FindGameObjectWithTag("Player2");
        }
        else if (CompareTag("Player2"))
        {
            playerName = gameStatus.player2Name;
            otherPlayer = GameObject.FindGameObjectWithTag("Player1");
        }

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

        SetIgnoreCollisionWithOtherPlayer(true);

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

        SetIgnoreCollisionWithOtherPlayer(false);

        isInvincible = false;
    }

    private void SetIgnoreCollisionWithOtherPlayer(bool ignore)
    {
        if (otherPlayer == null) return;

        Collider[] myColliders = GetComponentsInChildren<Collider>();
        Collider[] theirColliders = otherPlayer.GetComponentsInChildren<Collider>();

        foreach (var myCol in myColliders)
        {
            foreach (var theirCol in theirColliders)
            {
                Physics.IgnoreCollision(myCol, theirCol, ignore);
            }
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = playerName;
        }
    }
}
