using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLoad : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitAndLoadGame());
    }

    private IEnumerator WaitAndLoadGame()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("menu");
    }
}