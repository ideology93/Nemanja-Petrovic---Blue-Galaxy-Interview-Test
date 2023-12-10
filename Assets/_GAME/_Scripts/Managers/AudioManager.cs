/*
Simple Sound Manager (c) 2016 Digital Ruby, LLC
http://www.digitalruby.com

Source code may no longer be redistributed in source format. Using this in apps and games is fine.
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Be sure to add this using statement to your scripts
// using DigitalRuby.SoundManagerNamespace



public class AudioManager : MonoBehaviour
{
    float startMusicVolume;
    float startSFXVolume;
    public SoundManager SoundManager;
    public InputField SoundCountTextBox;
    public Toggle PersistToggle;
    private static AudioManager _instance;
    public bool isMuted;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is null");
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        GetVolume();
    }

    public AudioSource[] SoundAudioSources;
    public AudioSource[] MusicAudioSources;

    public void GetVolume()
    {
        startSFXVolume = SoundAudioSources[0].volume;
        startMusicVolume = MusicAudioSources[0].volume;
    }
    public void MuteVolume()
    {
        if (!isMuted)
        {
            isMuted = true;
            SoundAudioSources[0].volume = 0;
            MusicAudioSources[0].volume = 0;
        }
        else
        {
            isMuted = false;
            SoundAudioSources[0].volume = startSFXVolume;
            MusicAudioSources[0].volume = startMusicVolume;
        }


    }
    public void PlaySound(int index)
    {
        if (!SoundAudioSources[index].isPlaying)
        {
            SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
        }
        else return;
    }
    public void PlaySoundObstacleDestroy(ObstacleType obstacleType)
    {
        if (obstacleType == ObstacleType.Wood)
        {
            if (!SoundAudioSources[0].isPlaying)
            {
                SoundAudioSources[0].PlayOneShotSoundManaged(SoundAudioSources[0].clip);
            }
            else return;
        }
        else
        {
            if (!SoundAudioSources[1].isPlaying)
            {
                SoundAudioSources[1].PlayOneShotSoundManaged(SoundAudioSources[1].clip);
            }
            else return;
        }
    }
    public void PlaySoundObstacleMove(ObstacleType obstacleType)
    {
        if (obstacleType == ObstacleType.Wood)
        {
            if (!SoundAudioSources[3].isPlaying)
            {
                SoundAudioSources[3].PlayOneShotSoundManaged(SoundAudioSources[3].clip);
            }
            else return;
        }
        else
        {
            if (!SoundAudioSources[2].isPlaying)
            {
                SoundAudioSources[2].PlayOneShotSoundManaged(SoundAudioSources[2].clip);
            }
            else return;
        }
    }
    public void PlaySoundWithClipping(int index)
    {
        SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
    }
    public void StopSound(int index)
    {
        SoundManager.StopLoopingSound(SoundAudioSources[index]);
    }

    private void PlayMusic(int index)
    {
        MusicAudioSources[index].PlayLoopingMusicManaged(1.0f, 1.0f, true);
    }

    public void PersistToggleChanged(bool isOn)
    {
        SoundManager.StopSoundsOnLevelLoad = !isOn;
    }
}
public enum ObstacleType
{
    Wood, Brick, Rock, Concrete, Galaxy, Cardboard
}
public enum ObstacleModifier
{
    Regular, JackInTheBox
}

