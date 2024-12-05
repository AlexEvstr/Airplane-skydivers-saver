using UnityEngine;
using UnityEngine.SceneManagement;

public class Parachutist : MonoBehaviour
{
    public float fallSpeed = 2f;
    public bool isRare = false;
    public GameObject sparkleEffect;

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < Camera.main.ViewportToWorldPoint(Vector3.zero).y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "company")
            {
                int coinsToAdd = isRare ? 5 : 1;
                GameManager.Instance.AddCoins(coinsToAdd);
                PlaneController planeController = collision.GetComponent<PlaneController>();
                planeController.ShowParachuteEffect(isRare);
                GameManager.Instance.IncreaseRescueCount();
            }
            else
            {
                int coinsToAdd = isRare ? 5 : 1;
                EndlessGameManager.Instance.AddCoins(coinsToAdd);
                PlaneController planeController = collision.GetComponent<PlaneController>();
                planeController.ShowParachuteEffect(isRare);
                EndlessGameManager.Instance.IncreaseRescueCount();
            }
            

            if (sparkleEffect)
            {
                Instantiate(sparkleEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}