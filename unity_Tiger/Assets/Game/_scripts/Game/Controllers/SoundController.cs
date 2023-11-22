using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoSingleton<SoundController>
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundsSource;
    [SerializeField] private List<AudioClip> backgroundSounds;

    private void Start()
    {
        SetTogglesSetting();
        GameEvents.Instance.OnGameStarted += PlayGameBackgroundMusic;
        GameEvents.Instance.OnGameLosed += StopBackgroundMusic;
        GameEvents.Instance.OnGameStoped += StopBackgroundMusic;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameStarted -= PlayGameBackgroundMusic;
        GameEvents.Instance.OnGameLosed -= StopBackgroundMusic;
        GameEvents.Instance.OnGameStoped -= StopBackgroundMusic;
    }

    public void PlayMainBackgroundMusic()
    {
        musicSource.clip = backgroundSounds[(int)BackgroundSounds.MainSound];
        musicSource.Play();
    }


    private void PlayGameBackgroundMusic()
    {
        musicSource.clip = backgroundSounds[(int)BackgroundSounds.GameSound];
        musicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }

    public void PlayEffectSound(AudioClip clip)
    {
        soundsSource.PlayOneShot(clip);
    }

    public void ToggleMusic()
    {
        SLS.Data.Settings.MusicMuted.Value = !SLS.Data.Settings.MusicMuted.Value;
        musicSource.mute = SLS.Data.Settings.MusicMuted.Value;
    }

    public void ToggleSounds()
    {
        SLS.Data.Settings.SoundsMuted.Value = !SLS.Data.Settings.SoundsMuted.Value;
        soundsSource.mute = SLS.Data.Settings.SoundsMuted.Value;
    }

    private void SetTogglesSetting()
    {
        musicSource.mute = SLS.Data.Settings.MusicMuted.Value;
        soundsSource.mute = SLS.Data.Settings.SoundsMuted.Value;
    }

    public enum BackgroundSounds
    {
        MainSound,
        GameSound
    }
}
