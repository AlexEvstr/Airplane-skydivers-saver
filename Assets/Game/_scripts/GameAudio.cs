using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioSource musicSource;         
    public AudioSource soundEffectsSource;

    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _explosion;
    [SerializeField] private AudioClip _bonus;
    [SerializeField] private AudioClip _coin;
    [SerializeField] private AudioClip _lose;
    [SerializeField] private AudioClip _win;
    [SerializeField] private AudioClip _upgrade;

    private void Start()
    {
        bool isMusicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1; // По умолчанию включено
        musicSource.volume = isMusicOn ? 1 : 0;

        // Звуки
        bool isSoundOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1; // По умолчанию включено
        soundEffectsSource.volume = isSoundOn ? 1 : 0;
    }

    public void ClickSound()
    {
        soundEffectsSource.PlayOneShot(_click);
    }

    public void ExplosionSound()
    {
        soundEffectsSource.PlayOneShot(_explosion);
    }

    public void BonusSound()
    {
        soundEffectsSource.PlayOneShot(_bonus);
    }

    public void CoinSound()
    {
        soundEffectsSource.PlayOneShot(_coin);
    }

    public void LoseSound()
    {
        soundEffectsSource.PlayOneShot(_lose);
    }

    public void WinSound()
    {
        soundEffectsSource.PlayOneShot(_win);
    }

    public void UpgradeSound()
    {
        soundEffectsSource.PlayOneShot(_upgrade);
    }
}
