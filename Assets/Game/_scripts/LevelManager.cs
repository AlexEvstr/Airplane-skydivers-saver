using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public int totalLevels = 8;                 // Общее количество уровней
    public Button[] levelButtons;              // Кнопки для уровней
    public string gameSceneName = "GameScene"; // Название сцены с игрой
    public Image fadeImage;                    // Для затемнения
    public float fadeDuration = 1f;            // Длительность затухания

    private int currentLevel;                  // Текущий уровень

    private void Start()
    {
        // Получаем текущий уровень из PlayerPrefs или устанавливаем 1 уровень по умолчанию
        currentLevel = PlayerPrefs.GetInt("BestLevel", 1);

        // Настраиваем кнопки уровней
        SetupLevelButtons();
    }

    private void SetupLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            Button button = levelButtons[i];

            if (i + 1 <= currentLevel)
            {
                // Уровень доступен: замок отключен, кнопка активна
                Transform lockIcon = button.transform.GetChild(0); // Замок - нулевой дочерний объект
                if (lockIcon != null)
                {
                    lockIcon.gameObject.SetActive(false);
                }
                button.interactable = true;

                // Привязываем событие к кнопке
                int levelToLoad = i + 1; // Захватываем локальную переменную для лямбда-функции
                button.onClick.AddListener(() => LoadLevel(levelToLoad));
            }
            else
            {
                // Уровень недоступен: замок включен, кнопка неактивна
                Transform lockIcon = button.transform.GetChild(0); // Замок - нулевой дочерний объект
                if (lockIcon != null)
                {
                    lockIcon.gameObject.SetActive(true);
                }
                button.interactable = false;
            }
        }
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(FadeOutAndLoadLevel(level));
    }

    private IEnumerator FadeOutAndLoadLevel(int level)
    {
        // Сохраняем текущий уровень
        PlayerPrefs.SetInt("CurrentLevel", level);

        // Затухание
        Color color = fadeImage.color;
        color.a = 0; // Начальная альфа прозрачна
        fadeImage.color = color;

        while (fadeImage.color.a < 1)
        {
            color.a += Time.deltaTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        // Загружаем сцену с игрой
        SceneManager.LoadScene(gameSceneName);
    }
}
