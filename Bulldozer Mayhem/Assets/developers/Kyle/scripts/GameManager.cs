using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private List<GameObject> platforms;
    [SerializeField] private float minDelay, maxDelay, spawnDelayPlatform;
    [SerializeField] private float minDelayObjectSpawn, maxDelayObjectSpawn, spawnDelay;

    [SerializeField] private List<GameObject> spawnObjectPrefabs = new();
    private List<GameObject> spawnedObjects = new();

    public float objectLifetime = 5f;
    public Material platformMaterial;

    public GameObject winnerPanel;
    public TextMeshProUGUI winnerText;
    public GameObject player1;
    public GameObject player2;

    public GameObject controlSceneButton;

    private int playersAlive = 2;

    private List<GameObject> availablePlatforms = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(StartNewRound());
        StartCoroutine(GravityDelay());
        winnerPanel.SetActive(false);

        if (controlSceneButton != null)
        {
            controlSceneButton.SetActive(false);
        }

        ResetAvailablePlatforms(); 

        player1.GetComponent<PlayerLives>().SpawnPlayerAtStart();
        player2.GetComponent<PlayerLives>().SpawnPlayerAtStart();
    }

    private IEnumerator GravityDelay()
    {
        while (platforms.Count > 0)
        {
            float randomWaitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(randomWaitTime);
            PlatformFall();
        }
    }

    private void PlatformFall()
    {
        int i = Random.Range(0, platforms.Count);

        if (platforms[i] != null && platforms[i].activeInHierarchy)
        {
            StartCoroutine(ChangeColorAndFall(platforms[i]));
        }
        else
        {
            PlatformFall();
        }
    }

    private IEnumerator ChangeColorAndFall(GameObject platform)
    {
        yield return new WaitForSeconds(spawnDelayPlatform);

        Renderer renderer = platform.GetComponent<Renderer>();
        if (renderer != null && platformMaterial != null)
        {
            renderer.material.color = Color.red;
        }

        yield return new WaitForSeconds(3.0f);

        Rigidbody rigidBody = platform.GetComponent<Rigidbody>();
        Collider collider = platform.GetComponent<Collider>();
        if (rigidBody != null)
        {
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            collider.enabled = false;
            rigidBody.drag = 0.2f;
        }

        Destroy(platform, 5);
        platforms.Remove(platform);
    }

    private IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(spawnDelay);

        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        while (true)
        {
            float randomTime = Random.Range(minDelayObjectSpawn, maxDelayObjectSpawn);
            GameObject newObject = Instantiate(spawnObjectPrefabs[Random.Range(0, spawnObjectPrefabs.Count)], GetRandomPosition(), Quaternion.identity);
            StartCoroutine(DestroyAfterTime(newObject, objectLifetime));
            yield return new WaitForSeconds(randomTime);
        }
    }

    private IEnumerator DestroyAfterTime(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null)
        {
            Destroy(obj);
        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-9, 21), 105, Random.Range(-16f, 16f));
    }

    public void PlayerLost(GameObject player)
    {
        playersAlive--;
        player.SetActive(false);

        if (playersAlive == 1)
        {
            FindWinner();
        }
        else if (playersAlive == 0)
        {
            RestartGame();
        }
    }

    void FindWinner()
    {
        if (!player1.activeSelf)
        {
            winnerText.text = "Player 2 Wins!";
        }
        else if (!player2.activeSelf)
        {
            winnerText.text = "Player 1 Wins!";
        }

        winnerPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReturnToControlScene()
    {
        SceneManager.LoadScene("ControlScene");
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public List<GameObject> GetActivePlatforms()
    {
        return platforms.FindAll(p => p != null && p.activeInHierarchy);
    }

    public void ResetAvailablePlatforms()
    {
        availablePlatforms = GetActivePlatforms();
    }

    public Transform GetUniqueRandomSpawnPositions()
    {
        if (availablePlatforms.Count == 0)
            return null;

        int index = Random.Range(0, availablePlatforms.Count);
        Transform platformChild = availablePlatforms[index].GetComponent<Transform>();
        availablePlatforms.RemoveAt(index);
        return platformChild;
    }
}

