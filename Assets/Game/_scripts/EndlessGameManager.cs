using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class EndlessGameManager : MonoBehaviour
{
    public static EndlessGameManager Instance; // Синглтон

    public int currentLevel = 1;          // Текущий уровень
    public int rescueTarget = 5;         // Цель спасения парашютистов
    public int rescuedCount = 0;         // Счетчик спасенных
    public GameObject explosionPrefab;   // Префаб взрыва
    public GameObject parachutistPrefab; // Префаб парашютиста
    public GameObject rareParachutistPrefab; // Префаб редкого парашютиста
    public GameObject lightningPrefab;   // Префаб молнии
    public GameObject hurricanePrefab;   // Префаб урагана
    public GameObject obstaclePrefab;    // Префаб обычного препятствия
    public Text coinsText;               // Текст монет
    public Text rescuedText;             // Текст спасенных парашютистов
    public GameObject gameOverScreen;    // Окно Game Over
    public GameObject campaignCompleteScreen; // Окно завершения кампании
    public GameObject levelCompleteScreen;
    private GameAudio gameAudio;

    public GameObject playerPlane;

    private int coins;                   // Количество монет

    public float parachutistSpawnInterval = 3f; // Интервал спавна парашютистов
    public float obstacleSpawnInterval = 5f;   // Интервал спавна препятствий
    public float normalObstacleSpawnInterval = 7f; // Интервал спавна обычных препятствий

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
        // Загружаем текущий уровень и монеты из PlayerPrefs
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        coins = PlayerPrefs.GetInt("Coins", 0);

        UpdateUI();

        // Начинаем спавн парашютистов и препятствий
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

        // Проверяем, выполнена ли цель
        
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
        PlayerPrefs.Save(); // Убедимся, что данные сохраняются
        levelCompleteScreen.SetActive(true); // Показываем экран завершения уровня
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
        rescuedText.text = $"{rescuedCount}";
    }

    private IEnumerator SpawnParachutists()
    {
        while (true)
        {
            yield return new WaitForSeconds(parachutistSpawnInterval);

            // Рандомное определение обычного или редкого парашютиста
            GameObject parachutistPrefabToSpawn = Random.value > 0.8f ? rareParachutistPrefab : parachutistPrefab;

            // Спавн парашютиста
            Vector3 spawnPosition = new Vector3(
                Random.Range(-2.5f, 2.5f), // Рандомное X в пределах экрана
                Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 1, // Над экраном
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

            // Случайный выбор препятствия
            GameObject obstaclePrefab = Random.value > 0.5f ? lightningPrefab : hurricanePrefab;

            // Спавн препятствия
            Vector3 spawnPosition = new Vector3(
                Random.Range(-2.5f, 2.5f), // Рандомное X в пределах экрана
                Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 1, // Над экраном
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

            // Спавн обычного препятствия
            Vector3 spawnPosition = new Vector3(
                Random.Range(-2.5f, 2.5f), // Рандомное X в пределах экрана
                Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 1, // Над экраном
                0
            );

            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void DeactivatePlane()
    {
        if (playerPlane != null)
        {
            playerPlane.SetActive(false);
        }
    }
}
