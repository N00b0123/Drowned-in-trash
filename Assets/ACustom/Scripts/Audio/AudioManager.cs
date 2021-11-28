using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public static class AudioManager
{
    public enum Sound
    {
        RifleShot,
        RifleReload,
        ShotgunShot,
        ShotgunReload,
        SmgShot,
        SmgReload,
        PistolShot,
        PistolReload,
        EmptyShot,
        WoodBoxBreaking,
        Grenade,
        OpenDoor,
        CloseDoor,
        EnemyBreath,
        EnemyPain,
        EnemyDie,
        PlayerPain,
        PlayerJump,
        PlayerDie,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotSource;

    public static void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            //Rebuild in future to object polling
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 40f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.volume = 0.7f;
            audioSource.Play();

            GameObject.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("Sound");
                oneShotSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotSource.PlayOneShot(GetAudioClip(sound));
            
        }
    }

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.RifleShot] = 0f;
        soundTimerDictionary[Sound.ShotgunShot] = 0f;
        soundTimerDictionary[Sound.EnemyBreath] = 0f;
        soundTimerDictionary[Sound.EnemyPain] = 0f;
        soundTimerDictionary[Sound.PlayerDie] = 0f;
        soundTimerDictionary[Sound.EnemyDie] = 0f;
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.RifleShot:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float delay = .05f;
                    if (lastTimePlayed + delay < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    } 
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            case Sound.ShotgunShot:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float delay = .05f;
                    if (lastTimePlayed + delay < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            case Sound.EnemyBreath:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float delay = 3f;
                    if (lastTimePlayed + delay < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            case Sound.EnemyPain:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float delay = 0.5f;
                    if (lastTimePlayed + delay < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            case Sound.PlayerDie:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float delay = 5f;
                    if (lastTimePlayed + delay < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            case Sound.EnemyDie:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float delay = 0.5f;
                    if (lastTimePlayed + delay < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.instance.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }

        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

}
