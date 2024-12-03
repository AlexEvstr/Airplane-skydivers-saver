using UnityEngine;
using UnityEngine.UI;

public class FirstEnter : MonoBehaviour
{
    [SerializeField] private GameObject _firstEnterPanel;
    [SerializeField] private Button _accelerometer;
    [SerializeField] private Button _swipes;
    [SerializeField] private GameObject _save;
    private int _controlType;

    private void Start()
    {
        string firstEnter = PlayerPrefs.GetString("isChoosenControl", "");
        if (firstEnter == "")
        {
            _firstEnterPanel.SetActive(true);
        }
        else return;

        _accelerometer.onClick.AddListener(ChooseAccelerometer);
        _swipes.onClick.AddListener(ChooseSwipes);
    }

    private void ChooseAccelerometer()
    {
        _swipes.transform.GetChild(0).gameObject.SetActive(false);
        _accelerometer.transform.GetChild(0).gameObject.SetActive(true);
        _save.SetActive(true);
        _controlType = 0;
        PlayerPrefs.SetInt("ControlType", _controlType);
    }

    private void ChooseSwipes()
    {
        _accelerometer.transform.GetChild(0).gameObject.SetActive(false);
        _swipes.transform.GetChild(0).gameObject.SetActive(true);
        _save.SetActive(true);
        _controlType = 1;
        PlayerPrefs.SetInt("ControlType", _controlType);
    }
}