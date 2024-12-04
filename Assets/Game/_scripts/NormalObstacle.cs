using System.Collections;
using UnityEngine;

public class NormalObstacle : MonoBehaviour
{
    public float speed = 2f; // Скорость движения

    private void Update()
    {
        // Движение вниз
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Удаление, если вышло за границы экрана
        if (transform.position.y < Camera.main.ViewportToWorldPoint(Vector3.zero).y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Уничтожение самолета
            StartCoroutine(OpenGameOver());

            // Показываем эффект взрыва
            GameObject explosion = Instantiate(GameManager.Instance.explosionPrefab, collision.transform.position, Quaternion.identity);
            //Destroy(collision.gameObject); // Удаляем самолет
        }
    }

    private IEnumerator OpenGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.GameOver();
    }
}