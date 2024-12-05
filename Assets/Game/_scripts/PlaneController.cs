using System.Collections;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speed = 5f;                 // Скорость движения самолета
    public float swipeSensitivity = 1f;     // Чувствительность свайпов
    public float accelerometerSensitivity = 2f; // Чувствительность акселерометра

    private int controlType;                // Тип управления (0 = акселерометр, 1 = свайпы)
    private Vector3 startTouchPosition;     // Начальная позиция касания
    private Vector3 currentPosition;        // Текущая позиция касания

    private float screenLeftLimit;          // Левая граница экрана
    private float screenRightLimit;         // Правая граница экрана

    private bool controlsDisabled = false;  // Флаг отключения управления
    private bool invertedControls = false;  // Флаг инверсии управления

    private Transform lightningEffect;      // Дочерний объект для молнии
    private Transform hurricaneEffect;

    private Transform normalParachuteEffect;
    private Transform rareParachuteEffect;

    [SerializeField] private GameObject _gameOver;

    private void Start()
    {
        normalParachuteEffect = transform.GetChild(2);
        rareParachuteEffect = transform.GetChild(3);

        // Выключаем эффекты при старте
        normalParachuteEffect.gameObject.SetActive(false);
        rareParachuteEffect.gameObject.SetActive(false);
        // Получаем тип управления из PlayerPrefs
        controlType = PlayerPrefs.GetInt("ControlType", 0);

        // Вычисляем границы экрана
        Vector3 screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        screenLeftLimit = screenLeft.x;
        screenRightLimit = screenRight.x;

        // Получаем ссылки на дочерние объекты эффектов
        lightningEffect = transform.GetChild(0);
        hurricaneEffect = transform.GetChild(1);

        // Убеждаемся, что эффекты отключены при старте
        lightningEffect.gameObject.SetActive(false);
        hurricaneEffect.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (controlsDisabled) return; // Если управление отключено, выходим

        // Выбираем тип управления
        if (controlType == 0)
        {
            HandleAccelerometer(); // Управление акселерометром
        }
        else if (controlType == 1)
        {
            HandleSwipes(); // Управление свайпами
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

        // Инвертируем управление, если флаг установлен
        if (invertedControls)
        {
            tilt *= -1;
        }

        transform.Translate(Vector3.right * tilt * speed * Time.deltaTime);
    }

    private void HandleSwipes()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Сохраняем начальную позицию касания
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            // Обновляем текущую позицию касания
            currentPosition = Input.mousePosition;

            // Вычисляем разницу по X между началом касания и текущей позицией
            float deltaX = (currentPosition.x - startTouchPosition.x) * swipeSensitivity;

            // Инвертируем управление, если флаг установлен
            if (invertedControls)
            {
                deltaX *= -1;
            }

            // Двигаем самолет
            transform.Translate(Vector3.right * deltaX * Time.deltaTime);

            // Обновляем начальную позицию, чтобы движение было плавным
            startTouchPosition = currentPosition;
        }
    }

    // Отключение управления на определенное время с визуальным эффектом
    public void DisableControls(float duration)
    {
        StartCoroutine(DisableControlsCoroutine(duration));
    }

    private IEnumerator DisableControlsCoroutine(float duration)
    {
        controlsDisabled = true;

        // Включаем эффект молнии
        lightningEffect.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        // Выключаем эффект молнии
        lightningEffect.gameObject.SetActive(false);

        controlsDisabled = false;
    }

    // Инверсия управления на определенное время с визуальным эффектом
    public void InvertControls(float duration)
    {
        StartCoroutine(InvertControlsCoroutine(duration));
    }

    private IEnumerator InvertControlsCoroutine(float duration)
    {
        invertedControls = true;

        // Включаем эффект урагана
        hurricaneEffect.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        // Выключаем эффект урагана
        hurricaneEffect.gameObject.SetActive(false);

        invertedControls = false;
    }

    public void ShowParachuteEffect(bool isRare)
    {
        StartCoroutine(ShowParachuteEffectCoroutine(isRare));
    }

    private IEnumerator ShowParachuteEffectCoroutine(bool isRare)
    {
        Transform effect = isRare ? rareParachuteEffect : normalParachuteEffect;

        // Включаем эффект
        effect.gameObject.SetActive(true);

        // Ждем одну секунду
        yield return new WaitForSeconds(1f);

        // Выключаем эффект
        effect.gameObject.SetActive(false);
    }

    //private void OnDisable()
    //{
    //    _gameOver.SetActive(true);
    //}

}
