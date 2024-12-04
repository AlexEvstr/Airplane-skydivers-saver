using UnityEngine;

public class ObstacleEffect : MonoBehaviour
{
    public enum EffectType { Lightning, Hurricane }
    public EffectType effectType;

    public float speed = 2f; // Скорость движения препятствия

    private void Update()
    {
        // Движение вниз
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Удаление, если вышли за пределы экрана
        if (transform.position.y < Camera.main.ViewportToWorldPoint(Vector3.zero).y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlaneController planeController = collision.GetComponent<PlaneController>();

            if (planeController != null)
            {
                // Длительность эффектов зависит от улучшений
                float duration = effectType == EffectType.Lightning
                    ? PlayerPrefs.GetFloat("Immunity", 3.0f)
                    : PlayerPrefs.GetFloat("Controllability", 3.0f);

                if (effectType == EffectType.Lightning)
                {
                    // Отключаем управление
                    planeController.DisableControls(duration);
                }
                else if (effectType == EffectType.Hurricane)
                {
                    // Инвертируем управление
                    planeController.InvertControls(duration);
                }
            }

            // Уничтожаем препятствие после эффекта
            Destroy(gameObject);
        }
    }
}
