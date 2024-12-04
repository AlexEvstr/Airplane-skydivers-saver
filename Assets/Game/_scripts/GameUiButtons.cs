using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUiButtons : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject _pause;
    private float fadeDuration = 0.5f;

    private void Start()
    {
        Time.timeScale = 1;
        Color color = fadeImage.color;
        color.a = 1;
        fadeImage.color = color;
        StartCoroutine(FadeIn());
    }

    public void OpenPause()
    {
        _pause.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePause()
    {
        _pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadSceneWithFade(string sceneName)
    {
        Time.timeScale = 1;
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        Time.timeScale = 1;
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
}
