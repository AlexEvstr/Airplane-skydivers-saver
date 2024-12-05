using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 1;
    public int rescueTarget = 5;
    public int rescuedCount = 0;
    public GameObject explosionPrefab;
    public GameObject parachutistPrefab;
    public GameObject rareParachutistPrefab;
    public GameObject lightningPrefab;
    public GameObject hurricanePrefab;
    public GameObject obstaclePrefab;
    public Text coinsText;
    public Text rescuedText;
    public GameObject gameOverScreen;
    public GameObject campaignCompleteScreen;
    public GameObject levelCompleteScreen;
    private GameAudio gameAudio;

    public GameObject playerPlane;

    private int coins;

    public float parachutistSpawnInterval = 3f;
    public float obstacleSpawnInterval = 5f;
    public float normalObstacleSpawnInterval = 7f;

    private void Awake()
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

    private void Start()
    {
        gameAudio = GetComponent<GameAudio>();
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        coins = PlayerPrefs.GetInt("Coins", 0);

        UpdateUI();

        StartCoroutine(SpawnParachutists());
        StartCoroutine(SpawnObstacles());
        StartCoroutine(SpawnNormalObstacles());
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
        gameAudio.CoinSound();

        UpdateUI();
    }

    public void IncreaseRescueCount()
    {
        rescuedCount++;
        UpdateUI();

        if (rescuedCount >= rescueTarget)
        {
            DeactivatePlane();

            if (currentLevel >= 8)
            {
                campaignCompleteScreen.SetActive(true);
            }
            else
            {
                LevelComplete();
            }
        }
    }

    private void LevelComplete()
    {
        currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        int bestLevel = PlayerPrefs.GetInt("BestLevel", currentLevel);
        if (bestLevel <= currentLevel)
        {
            bestLevel = currentLevel;
            PlayerPrefs.SetInt("BestLevel", bestLevel);
        }
        PlayerPrefs.Save();
        levelCompleteScreen.SetActive(true);
        gameAudio.WinSound();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        DeactivatePlane();

    }

    private void UpdateUI()
    {
        coinsText.text = $"{coins}";
        rescuedText.text = $"{rescuedCount}/{rescueTarget}";
    }

    private IEnumerator SpawnParachutists()
    {
        while (true)
        {
            yield return new WaitForSeconds(parachutistSpawnInterval);

            GameObject parachutistPrefabToSpawn = Random.value > 0.8f ? rareParachutistPrefab : parachutistPrefab;

            Vector3 spawnPosition = new Vector3(
                Random.Range(-2f, 2f),
                Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 1,
                0
            );

            Instantiate(parachutistPrefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(obstacleSpawnInterval);

            GameObject obstaclePrefab = Random.value > 0.5f ? lightningPrefab : hurricanePrefab;

            Vector3 spawnPosition = new Vector3(
                Random.Range(-2f, 2f),
                Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 1,
                0
            );

            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator SpawnNormalObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(normalObstacleSpawnInterval);

            Vector3 spawnPosition = new Vector3(
                Random.Range(-2f, 2f),
                Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 1,
                0
            );

            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void DeactivatePlane()
    {
        if (playerPlane != null)
        {
            playerPlane.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}