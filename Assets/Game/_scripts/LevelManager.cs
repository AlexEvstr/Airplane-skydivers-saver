using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public int totalLevels = 8;
    public Button[] levelButtons;
    public string gameSceneName = "GameScene";
    public Image fadeImage;
    public float fadeDuration = 1f;

    private int currentLevel;

    private void Start()
    {
        Time.timeScale = 1;
        currentLevel = PlayerPrefs.GetInt("BestLevel", 1);

        SetupLevelButtons();
    }

    private void SetupLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            Button button = levelButtons[i];

            if (i + 1 <= currentLevel)
            {
                Transform lockIcon = button.transform.GetChild(0);
                if (lockIcon != null)
                {
                    lockIcon.gameObject.SetActive(false);
                }
                button.interactable = true;

                int levelToLoad = i + 1;
                button.onClick.AddListener(() => LoadLevel(levelToLoad));
            }
            else
            {
                Transform lockIcon = button.transform.GetChild(0);
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
        PlayerPrefs.SetInt("CurrentLevel", level);

        Color color = fadeImage.color;
        color.a = 0;
        fadeImage.color = color;

        while (fadeImage.color.a < 1)
        {
            color.a += Time.deltaTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(gameSceneName);
    }
}