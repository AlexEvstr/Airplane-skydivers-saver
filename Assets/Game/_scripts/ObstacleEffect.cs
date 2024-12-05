using UnityEngine;

public class ObstacleEffect : MonoBehaviour
{
    public enum EffectType { Lightning, Hurricane }
    public EffectType effectType;

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
            PlaneController planeController = collision.GetComponent<PlaneController>();

            if (planeController != null)
            {
                gameAudio.BonusSound();
                float duration = effectType == EffectType.Lightning
                    ? PlayerPrefs.GetFloat("Immunity", 3.0f)
                    : PlayerPrefs.GetFloat("Controllability", 3.0f);

                if (effectType == EffectType.Lightning)
                {
                    planeController.DisableControls(duration);
                }
                else if (effectType == EffectType.Hurricane)
                {
                    planeController.InvertControls(duration);
                }
            }

            Destroy(gameObject);
        }
    }
}