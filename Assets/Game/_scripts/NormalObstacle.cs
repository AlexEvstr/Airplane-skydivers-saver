using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalObstacle : MonoBehaviour
{
    public float speed = 2f;
    private GameAudio gameAudio;

    private void Start()
    {
        gameAudio = FindObjectOfType<GameAudio>();
    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < Camera.main.ViewportToWorldPoint(Vector3.zero).y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameAudio.LoseSound();
            if (SceneManager.GetActiveScene().name == "company")
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                EndlessGameManager.Instance.GameOver();
            }

            if (SceneManager.GetActiveScene().name == "company")
            {
                GameObject explosion = Instantiate(GameManager.Instance.explosionPrefab, collision.transform.position, Quaternion.identity);
            }
            else
            {
                GameObject explosion = Instantiate(EndlessGameManager.Instance.explosionPrefab, collision.transform.position, Quaternion.identity);
            }
            
            gameAudio.ExplosionSound();
        }
    }
}