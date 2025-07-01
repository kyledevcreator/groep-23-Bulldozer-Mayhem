using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameStatus gameStatus;
    [SerializeField] private PlayerStatistic player1Stat, player2Stat;


    [SerializeField] private List<GameObject> platforms;
    [SerializeField] private float minDelay, maxDelay, spawnDelayPlatform;
    [SerializeField] private float minDelayObjectSpawn, maxDelayObjectSpawn, spawnDelay;

    [SerializeField] private List<GameObject> spawnObjectPrefabs = new();
    private List<GameObject> spawnedObjects = new();

    public float objectLifetime = 5f;
    public Material platformMaterial;

    public TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI roundCount;
    [SerializeField] private TextMeshProUGUI p1text, p2text;
    public GameObject player1;
    public GameObject player2;

    public GameObject controlSceneButton;

    private int playersAlive;
    private bool firstChoosing;

    private List<GameObject> availablePlatforms = new List<GameObject>();

    public GameObject shopPanel;
    [SerializeField] private List<GameObject> shopButtons = new();
    [SerializeField] private List<TextMeshProUGUI> shopTexts = new();
    [SerializeField] private List<string> shopItems = new();
    private PlayerStatistic currentStatistic;

    private float deltaGas;
    private float deltaReverse;
    private float deltaTorque;
    private float deltaFrontS;
    private float deltaBackS;
    private float deltaLeftS;
    private float deltaRightS;
    private float deltaFrontP;
    private float deltaBackP;
    private float deltaLeftP;
    private float deltaRightP;
    private float deltaDragRotation;

    void Awake()
    {
        firstChoosing = true;
        playersAlive = 2;
        gameStatus.currentRound++;
        roundCount.text = "Round " + gameStatus.currentRound.ToString();
        foreach (GameObject button in shopButtons)
        {
            button.SetActive(true);
        }

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
        shopPanel.SetActive(false);

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
        Debug.Log(playersAlive);
        if (playersAlive <= 1)
        {
            FindWinner();
        }
    }

    void FindWinner()
    {
        if (!player1.activeSelf)
        {
            winnerText.text = "Player 2 (" + player2.GetComponent<PlayerLives>().playerName + ") Wins! They choose their powerup first!";
            currentStatistic = player2Stat;
        }
        else if (!player2.activeSelf)
        {
            winnerText.text = "Player 1 (" + player1.GetComponent<PlayerLives>().playerName + ") Wins! They choose their powerup first!";
            currentStatistic = player1Stat;
        }
        BuildShop();
        shopPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReturnToControlScene()
    {
        SceneManager.LoadScene("ControlScene");
    }

    void RestartGame()
    {
        SceneManager.LoadScene("MainGame");
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


    private void BuildShop()
    {
        for (int i = 0; i < shopTexts.Count; i++)
        {
            shopTexts[i].text = shopItems[Random.Range(0, shopItems.Count)];
        }

    }


    private void ApplyButton(string powerup)
    {
        if (powerup == "Step on da gas!")
        {
            deltaGas = 50;
            deltaReverse = 25;
            deltaTorque = 0;
            deltaFrontS = 0;
            deltaBackS = 0;
            deltaLeftS = 0;
            deltaRightS = 0;
            deltaFrontP = 0;
            deltaBackP = 0;
            deltaLeftP = 0;
            deltaRightP = 0;
            deltaDragRotation = 0;
        }
        else if (powerup == "Put it in reverse!")
        {
            deltaGas = 15;
            deltaReverse = 75;
            deltaTorque = 0;
            deltaFrontS = 0;
            deltaBackS = 2;
            deltaLeftS = 0;
            deltaRightS = 0;
            deltaFrontP = 0;
            deltaBackP = 0;
            deltaLeftP = 0;
            deltaRightP = 0;
            deltaDragRotation = 0;
        }
        else if (powerup == "Sleeper build!")
        {
            deltaGas = -20;
            deltaReverse = -20;
            deltaTorque = 0;
            deltaFrontS = 2;
            deltaBackS = 2;
            deltaLeftS = 2;
            deltaRightS = 2;
            deltaFrontP = 5;
            deltaBackP = 5;
            deltaLeftP = 5;
            deltaRightP = 5;
            deltaDragRotation = 0;
        }
        else if (powerup == "Lefty!")
        {
            deltaGas = -20;
            deltaReverse = -20;
            deltaTorque = 0;
            deltaFrontS = 0;
            deltaBackS = 0;
            deltaLeftS = 10;
            deltaRightS = 0;
            deltaFrontP = 0;
            deltaBackP = 0;
            deltaLeftP = 10;
            deltaRightP = 0;
            deltaDragRotation = 0;
        }
        else if (powerup == "Righteous!")
        {
            deltaGas = -20;
            deltaReverse = -20;
            deltaTorque = 0;
            deltaFrontS = 0;
            deltaBackS = 0;
            deltaLeftS = 0;
            deltaRightS = 10;
            deltaFrontP = 0;
            deltaBackP = 0;
            deltaLeftP = 0;
            deltaRightP = 10;
            deltaDragRotation = 0;
        }
        ApplyDeltas();
    }

    private void ApplyDeltas()
    {
        currentStatistic.gasSpeedBonus += deltaGas;
        currentStatistic.reverseSpeedBonus += deltaReverse;
        currentStatistic.torqueSpeedBonus += deltaTorque;
        currentStatistic.frontStrength += deltaFrontS;
        currentStatistic.backStrength += deltaBackS;
        currentStatistic.leftStrength += deltaLeftS;
        currentStatistic.rightStrength += deltaRightS;
        currentStatistic.frontPower += deltaFrontP;
        currentStatistic.backPower += deltaBackP;
        currentStatistic.leftPower += deltaLeftP;
        currentStatistic.rightPower += deltaRightP;
        currentStatistic.rotationalDragBonus += deltaDragRotation;
    }

    public void Button(int button)
    {
        ApplyButton(shopTexts[button].text);
        shopButtons[button].SetActive(false);
        if (!firstChoosing)
        {
            Time.timeScale = 1f;
            RestartGame();
            shopPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
        }

        if (currentStatistic == player1Stat)
        {
            currentStatistic = player2Stat;
            winnerText.text = "Player 2 (" + player2.GetComponent<PlayerLives>().playerName + ") may now select their powerup!";
        }
        else
        {
            currentStatistic = player1Stat;
            winnerText.text = "Player 1 (" + player1.GetComponent<PlayerLives>().playerName + ") may now select their powerup!";
        }
        firstChoosing = false;
    }
}

