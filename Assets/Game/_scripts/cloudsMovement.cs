using UnityEngine;

public class cloudsMovement : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 1.0f);
    }
}