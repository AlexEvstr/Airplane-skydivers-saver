using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public AudioSource musicSource;           // AudioSource для музыки
    public AudioSource soundEffectsSource;    // AudioSource для звуков

    // Объекты для переключения музыки
    public GameObject musicOnObject;          // Включенная музыка
    public GameObject musicOffObject;         // Выключенная музыка

    // Объекты для переключения звуков
    public GameObject soundOnObject;          // Включенные звуки
    public GameObject soundOffObject;         // Выключенные звуки

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

    /// <summary>
    /// Переключение состояния музыки
    /// </summary>
    public void ToggleMusic()
    {
        _save.gameObject.SetActive(true);
        _back.SetActive(false);
        bool isMusicOn = musicSource.volume > 0;

        // Переключаем громкость музыки
        musicSource.volume = isMusicOn ? 0 : 1;

        // Обновляем UI
        UpdateUI(!isMusicOn, true);
    }

    /// <summary>
    /// Переключение состояния звуков
    /// </summary>
    public void ToggleSoundEffects()
    {
        _save.gameObject.SetActive(true);
        _back.SetActive(false);
        bool isSoundOn = soundEffectsSource.volume > 0;

        // Переключаем громкость звуков
        soundEffectsSource.volume = isSoundOn ? 0 : 1;

        // Обновляем UI
        UpdateUI(!isSoundOn, false);
    }

    /// <summary>
    /// Инициализация состояния звука и UI при старте
    /// </summary>
    private void InitializeAudioState()
    {
        // Музыка
        bool isMusicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1; // По умолчанию включено
        musicSource.volume = isMusicOn ? 1 : 0;
        UpdateUI(isMusicOn, true);

        // Звуки
        bool isSoundOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1; // По умолчанию включено
        soundEffectsSource.volume = isSoundOn ? 1 : 0;
        UpdateUI(isSoundOn, false);
    }

    /// <summary>
    /// Обновление UI объектов для музыки и звуков
    /// </summary>
    /// <param name="state">Состояние (включено/выключено)</param>
    /// <param name="isMusic">Если true — обновляем музыку, иначе — звуки</param>
    private void UpdateUI(bool state, bool isMusic)
    {
        if (isMusic)
        {
            musicOnObject.SetActive(state);
            musicOffObject.SetActive(!state);
            PlayerPrefs.SetInt("MusicEnabled", state ? 1 : 0); // Сохраняем состояние музыки
        }
        else
        {
            soundOnObject.SetActive(state);
            soundOffObject.SetActive(!state);
            PlayerPrefs.SetInt("SoundEnabled", state ? 1 : 0); // Сохраняем состояние звуков
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