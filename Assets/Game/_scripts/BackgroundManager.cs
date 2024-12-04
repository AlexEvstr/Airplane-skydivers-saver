using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Sprite[] backgrounds;  // Массив спрайтов для фона
    public Image backgroundImage; // UI-Image, отображающий фон

    private void Start()
    {
        // Получаем текущий уровень из PlayerPrefs
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

        // Вычисляем индекс фона (по модулю количества фоновых изображений)
        int backgroundIndex = (currentLevel - 1) % backgrounds.Length;

        // Устанавливаем спрайт для Image
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
