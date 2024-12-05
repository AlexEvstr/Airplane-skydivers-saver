using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _gameType;
    [SerializeField] private GameObject _levels;
    [SerializeField] private GameObject _options;
    [SerializeField] private GameObject _saveOptions;
    [SerializeField] private GameObject _backOptions;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _firstEnter;
    [SerializeField] private GameObject _rulesWindow;

    [SerializeField] private Image fadeImage;
    private float fadeDuration = 0.25f;

    private void Start()
    {
        Color color = fadeImage.color;
        color.a = 1;
        fadeImage.color = color;
        StartCoroutine(FadeIn());
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.25f);
        Color color = fadeImage.color;
        color.a = 1;
        fadeImage.color = color;

        while (fadeImage.color.a > 0)
        {
            color.a -= Time.deltaTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0;
        fadeImage.color = color;
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        Color color = fadeImage.color;
        color.a = 0;
        fadeImage.color = color;

        while (fadeImage.color.a < 1)
        {
            color.a += Time.deltaTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void OpenGameType()
    {
        FadeSwitchObjectss(_menu, _gameType);
    }

    public void BackToMenuFromGameType()
    {
        FadeSwitchObjectss(_gameType, _menu);
    }

    public void OpenLevels()
    {
        FadeSwitchObjectss(_gameType, _levels);
    }

    public void BackToGameTypeFromLevel()
    {
        FadeSwitchObjectss(_levels, _gameType);
    }

    public void OpenOptions()
    {
        FadeSwitchObjectss(_menu, _options);
    }

    public void CloseOptions()
    {
        FadeSwitchObjectss(_options, _menu);
    }

    public void OpenShop()
    {
        FadeSwitchObjectss(_menu, _shop); ;
    }

    public void CloseShop()
    {
        FadeSwitchObjectss(_shop, _menu);
    }

    public void CloseFirstEnter()
    {
        FadeSwitchObjectss(_firstEnter, _rulesWindow);
        PlayerPrefs.SetString("isChoosenControl", "yes");
    }

    public void OpenMenuFromRules()
    {
        FadeSwitchObjectss(_rulesWindow, _menu);
    }

    public void OpenRulesFromMenu()
    {
        FadeSwitchObjectss(_menu, _rulesWindow);
    }

    private void FadeSwitchObjectss(GameObject objectToDisable, GameObject objectToEnable)
    {
        StartCoroutine(FadeOutSwitchFadeIn(objectToDisable, objectToEnable));
    }

    private IEnumerator FadeOutSwitchFadeIn(GameObject objectToDisable, GameObject objectToEnable)
    {
        Color color = fadeImage.color;
        color.a = 0; // Начальная альфа прозрачна
        fadeImage.color = color;

        while (fadeImage.color.a < 1)
        {
            color.a += Time.deltaTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        // Отключаем и включаем объекты
        if (objectToDisable != null)
            objectToDisable.SetActive(false);
        if (objectToEnable != null)
            objectToEnable.SetActive(true);

        // Возвращение к прозрачности
        while (fadeImage.color.a > 0)
        {
            color.a -= Time.deltaTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        // Убедимся, что альфа полностью прозрачная
        color.a = 0;
        fadeImage.color = color;
        _saveOptions.SetActive(false);
        _backOptions.SetActive(true);
    }
}