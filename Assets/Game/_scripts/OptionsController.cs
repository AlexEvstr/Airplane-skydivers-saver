using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource soundEffectsSource;

    public GameObject musicOnObject;
    public GameObject musicOffObject;

    public GameObject soundOnObject;
    public GameObject soundOffObject;

    [SerializeField] private Button _accelerometer;
    [SerializeField] private Button _swipes;
    [SerializeField] private Button _save;
    [SerializeField] private GameObject _back;

    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _upgrade;
    private int _controlType;

    private void Start()
    {
        _controlType = PlayerPrefs.GetInt("ControlType", 0);
        if (_controlType == 0)
        {
            _swipes.transform.GetChild(0).gameObject.SetActive(false);
            _accelerometer.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            _accelerometer.transform.GetChild(0).gameObject.SetActive(false);
            _swipes.transform.GetChild(0).gameObject.SetActive(true);
        }

        InitializeAudioState();
    }

    public void ToggleMusic()
    {
        _save.gameObject.SetActive(true);
        _back.SetActive(false);
        bool isMusicOn = musicSource.volume > 0;

        musicSource.volume = isMusicOn ? 0 : 1;

        UpdateUI(!isMusicOn, true);
    }

    public void ToggleSoundEffects()
    {
        _save.gameObject.SetActive(true);
        _back.SetActive(false);
        bool isSoundOn = soundEffectsSource.volume > 0;

        soundEffectsSource.volume = isSoundOn ? 0 : 1;

        UpdateUI(!isSoundOn, false);
    }

    private void InitializeAudioState()
    {
        bool isMusicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        musicSource.volume = isMusicOn ? 1 : 0;
        UpdateUI(isMusicOn, true);

        bool isSoundOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        soundEffectsSource.volume = isSoundOn ? 1 : 0;
        UpdateUI(isSoundOn, false);
    }

    private void UpdateUI(bool state, bool isMusic)
    {
        if (isMusic)
        {
            musicOnObject.SetActive(state);
            musicOffObject.SetActive(!state);
            PlayerPrefs.SetInt("MusicEnabled", state ? 1 : 0);
        }
        else
        {
            soundOnObject.SetActive(state);
            soundOffObject.SetActive(!state);
            PlayerPrefs.SetInt("SoundEnabled", state ? 1 : 0); 
        }
    }

    public void ChooseAccelerometer()
    {
        _swipes.transform.GetChild(0).gameObject.SetActive(false);
        _accelerometer.transform.GetChild(0).gameObject.SetActive(true);
        _save.gameObject.SetActive(true);
        _back.SetActive(false);
        _controlType = 0;
        PlayerPrefs.SetInt("ControlType", _controlType);
    }

    public void ChooseSwipes()
    {
        _accelerometer.transform.GetChild(0).gameObject.SetActive(false);
        _swipes.transform.GetChild(0).gameObject.SetActive(true);
        _save.gameObject.SetActive(true);
        _back.SetActive(false);
        _controlType = 1;
        PlayerPrefs.SetInt("ControlType", _controlType);
    }

    public void ClickSound()
    {
        soundEffectsSource.PlayOneShot(_click);
    }

    public void UpgradeSound()
    {
        soundEffectsSource.PlayOneShot(_upgrade);
    }
}