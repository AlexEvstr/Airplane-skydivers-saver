using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Sprite[] backgrounds;
    public Image backgroundImage;

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

        int backgroundIndex = (currentLevel - 1) % backgrounds.Length;

        if (backgrounds != null && backgrounds.Length > 0 && backgroundImage != null)
        {
            backgroundImage.sprite = backgrounds[backgroundIndex];
        }
        else
        {
            Debug.LogWarning("Backgrounds array or Image is not properly set up!");
        }
    }
}