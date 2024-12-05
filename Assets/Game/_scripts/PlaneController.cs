using System.Collections;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private float speed = 5f;
    private float swipeSensitivity = 0.5f;
    private float accelerometerSensitivity = 2f;

    private int controlType;
    private Vector3 startTouchPosition;
    private Vector3 currentPosition;

    private float screenLeftLimit;
    private float screenRightLimit;

    private bool controlsDisabled = false;
    private bool invertedControls = false;

    private Transform lightningEffect;
    private Transform hurricaneEffect;

    private Transform normalParachuteEffect;
    private Transform rareParachuteEffect;

    [SerializeField] private GameObject _gameOver;

    private void Start()
    {
        normalParachuteEffect = transform.GetChild(2);
        rareParachuteEffect = transform.GetChild(3);

        normalParachuteEffect.gameObject.SetActive(false);
        rareParachuteEffect.gameObject.SetActive(false);
        controlType = PlayerPrefs.GetInt("ControlType", 0);

        Vector3 screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        screenLeftLimit = screenLeft.x;
        screenRightLimit = screenRight.x;

        lightningEffect = transform.GetChild(0);
        hurricaneEffect = transform.GetChild(1);

        lightningEffect.gameObject.SetActive(false);
        hurricaneEffect.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (controlsDisabled) return;

        if (controlType == 0)
        {
            HandleAccelerometer();
        }
        else if (controlType == 1)
        {
            HandleSwipes();
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, screenLeftLimit, screenRightLimit),
            transform.position.y,
            transform.position.z
        );
    }

    private void HandleAccelerometer()
    {
        float tilt = Input.acceleration.x * accelerometerSensitivity;

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
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            currentPosition = Input.mousePosition;

            float deltaX = (currentPosition.x - startTouchPosition.x) * swipeSensitivity;

            if (invertedControls)
            {
                deltaX *= -1;
            }

            transform.Translate(Vector3.right * deltaX * Time.deltaTime);

            startTouchPosition = currentPosition;
        }
    }

    public void DisableControls(float duration)
    {
        StartCoroutine(DisableControlsCoroutine(duration));
    }

    private IEnumerator DisableControlsCoroutine(float duration)
    {
        controlsDisabled = true;

        lightningEffect.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        lightningEffect.gameObject.SetActive(false);

        controlsDisabled = false;
    }

    public void InvertControls(float duration)
    {
        StartCoroutine(InvertControlsCoroutine(duration));
    }

    private IEnumerator InvertControlsCoroutine(float duration)
    {
        invertedControls = true;

        hurricaneEffect.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

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

        effect.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        effect.gameObject.SetActive(false);
    }
}