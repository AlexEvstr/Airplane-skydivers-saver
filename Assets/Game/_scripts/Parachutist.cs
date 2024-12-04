using UnityEngine;

public class Parachutist : MonoBehaviour
{
    public float fallSpeed = 2f;      // Скорость падения
    public bool isRare = false;      // Редкий парашютист или нет
    public GameObject sparkleEffect; // Эффект блесток при спасении

    private void Update()
    {
        // Падение вниз
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Удаление, если вышел за границы экрана
        if (transform.position.y < Camera.main.ViewportToWorldPoint(Vector3.zero).y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Добавляем монеты
            int coinsToAdd = isRare ? 5 : 1;
            GameManager.Instance.AddCoins(coinsToAdd);

            // Увеличиваем счетчик спасенных парашютистов
            GameManager.Instance.IncreaseRescueCount();

            // Показываем эффект блесток
            if (sparkleEffect)
            {
                Instantiate(sparkleEffect, transform.position, Quaternion.identity);
            }

            // Удаляем парашютиста
            Destroy(gameObject);
        }
    }
}
