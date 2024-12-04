using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUiButtons : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    private float fadeDuration = 0.5f;

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
}
