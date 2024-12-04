using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    public float animationDuration = 0.5f; // Длительность анимации в секундах

    private Vector3 targetScale; // Конечный размер
    private float elapsedTime;   // Прошедшее время

    private void OnEnable()
    {
        // Сбрасываем начальные параметры
        transform.localScale = Vector3.zero;
        targetScale = Vector3.one;
        elapsedTime = 0f;
    }

    private void Update()
    {
        // Если объект уже достиг целевого размера, прекращаем обновление
        if (transform.localScale == targetScale)
        {
            enabled = false;
            return;
        }

        // Увеличиваем прошедшее время
        elapsedTime += Time.deltaTime;

        // Линейная интерполяция масштаба
        transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsedTime / animationDuration);

        // Ограничиваем масштаб, чтобы избежать превышения
        if (elapsedTime >= animationDuration)
        {
            transform.localScale = targetScale;
            enabled = false;
        }
    }
}
