using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public enum Clip { CLICK, PICKUP, GAME_OVER, WIN } 
    
    [SerializeField]
    AudioSource musicController;
    [SerializeField]
    AudioSource sfxController;

    public AudioClip music;
    public AudioClip click;
    public AudioClip pickup;
    public AudioClip gameOver;
    public AudioClip win;

    Dictionary<Clip, AudioClip> clips;

    private StorageController storage;

    public static AudioController Instance;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    private void Start()
    {
        clips = new Dictionary<Clip, AudioClip>();
        storage = StorageController.Instance;
        if (storage.GetInt(Constants.SFX_ON_OFF) == 1)
        {
            sfxController.mute = false;
            sfxController.volume = storage.GetFloat(Constants.SFX_VOLUME);
        }
        else
        {
            sfxController.mute = true;
        }

        if (storage.GetInt(Constants.MUSIC_ON_OFF) == 1)
        {
            musicController.mute = false;
            musicController.volume = storage.GetFloat(Constants.MUSIC_VOLUME);
            musicController.clip = music;
            musicController.Play();
        }
        else
        {
            musicController.mute = true;
        }

        clips.Add(Clip.CLICK, click);
        clips.Add(Clip.PICKUP, pickup);
        clips.Add(Clip.GAME_OVER, gameOver);
        clips.Add(Clip.WIN, win);
    }

    public void MusicOn()
    {
        musicController.mute = false;
        musicController.clip = music;
        storage.StoreInt(Constants.MUSIC_ON_OFF, 1);
        musicController.volume = storage.GetFloat(Constants.MUSIC_VOLUME);
        musicController.Play();
    }

    public void MusicOff()
    {
        musicController.mute = true;
        musicController.Stop();
        storage.StoreInt(Constants.MUSIC_ON_OFF, 0);
    }

    public void SetMusicVolume(float volume)
    {
        musicController.volume = volume;
        storage.StoreFloat(Constants.MUSIC_VOLUME, volume);
    }

    public void SFXOn()
    {
        sfxController.mute = false;
        storage.StoreInt(Constants.SFX_ON_OFF, 1);
        sfxController.volume = storage.GetFloat(Constants.SFX_VOLUME);
    }

    public void SFXOff()
    {
        sfxController.mute = true;
        sfxController.Stop();
        storage.StoreInt(Constants.SFX_ON_OFF, 0);
    }

    public void SetSFXVolume(float volume)
    {
        sfxController.volume = volume;
        storage.StoreFloat(Constants.SFX_VOLUME, volume);
    }

    public void PlayClick() {
        sfxController.clip = clips[Clip.CLICK];
        sfxController.Play();
    }

    public void PlaySFX(Clip c)
    {
        sfxController.clip = clips[c];
        sfxController.Play();
    }
}
