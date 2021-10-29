using System;
using UnityEngine;

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
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
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
