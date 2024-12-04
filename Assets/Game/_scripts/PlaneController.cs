using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speed = 5f;              // Скорость движения самолета
    public float swipeSensitivity = 1f;  // Чувствительность свайпов
    public float accelerometerSensitivity = 2f; // Чувствительность акселерометра

    private int controlType;              // Тип управления (0 = акселерометр, 1 = свайпы)
    private Vector3 startTouchPosition;   // Начальная позиция касания
    private Vector3 currentPosition;      // Текущая позиция касания

    private float screenLeftLimit;        // Левая граница экрана
    private float screenRightLimit;       // Правая граница экрана

    private void Start()
    {
        // Получаем тип управления из PlayerPrefs
        controlType = PlayerPrefs.GetInt("ControlType", 0);

        // Вычисляем границы экрана
        Vector3 screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        screenLeftLimit = screenLeft.x;
        screenRightLimit = screenRight.x;
    }

    private void Update()
    {
        if (controlType == 0)
        {
            // Управление акселерометром
            HandleAccelerometer();
        }
        else if (controlType == 1)
        {
            // Управление свайпами
            HandleSwipes();
        }

        // Ограничиваем самолет в пределах границ экрана
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, screenLeftLimit, screenRightLimit),
            transform.position.y,
            transform.position.z
        );
    }

    private void HandleAccelerometer()
    {
        float tilt = Input.acceleration.x * accelerometerSensitivity; // Наклон устройства
        transform.Translate(Vector3.right * tilt * speed * Time.deltaTime);
    }

    private void HandleSwipes()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Сохраняем начальную позицию касания
            startTouchPosition = Input.mousePosition;
            currentPosition = startTouchPosition;
        }

        if (Input.GetMouseButton(0))
        {
            // Обновляем текущую позицию касания
            currentPosition = Input.mousePosition;

            // Вычисляем разницу по X между началом касания и текущей позицией
            float deltaX = (currentPosition.x - startTouchPosition.x) * swipeSensitivity;

            // Двигаем самолет
            transform.Translate(Vector3.right * deltaX * Time.deltaTime);

            // Обновляем начальную позицию, чтобы движение было плавным
            startTouchPosition = currentPosition;
        }
    }
}
