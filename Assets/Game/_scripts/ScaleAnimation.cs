using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    public float animationDuration = 0.5f;

    private Vector3 targetScale;
    private float elapsedTime;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        targetScale = Vector3.one;
        elapsedTime = 0f;
    }

    private void Update()
    {
        if (transform.localScale == targetScale)
        {
            enabled = false;
            return;
        }

        elapsedTime += Time.deltaTime;

        transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsedTime / animationDuration);

        if (elapsedTime >= animationDuration)
        {
            transform.localScale = targetScale;
            enabled = false;
        }
    }
}