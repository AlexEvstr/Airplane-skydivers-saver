using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalObstacle : MonoBehaviour
{
    public float speed = 2f; // Скорость движения
    private GameAudio gameAudio;

    private void Start()
    {
        gameAudio = FindObjectOfType<GameAudio>();
    }

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
            //StartCoroutine(OpenGameOver());

            gameAudio.LoseSound();
            if (SceneManager.GetActiveScene().name == "company")
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                EndlessGameManager.Instance.GameOver();
            }

            // Показываем эффект взрыва
            if (SceneManager.GetActiveScene().name == "company")
            {
                GameObject explosion = Instantiate(GameManager.Instance.explosionPrefab, collision.transform.position, Quaternion.identity);
            }
            else
            {
                GameObject explosion = Instantiate(EndlessGameManager.Instance.explosionPrefab, collision.transform.position, Quaternion.identity);
            }
            
            //Destroy(collision.gameObject); // Удаляем самолет
            gameAudio.ExplosionSound();
        }
    }
}