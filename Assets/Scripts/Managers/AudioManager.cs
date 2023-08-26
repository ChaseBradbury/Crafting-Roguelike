using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private Sound[] sounds;
    public static Dictionary<string, Sound> soundDictionary;

    void Awake()
    {
        soundDictionary = new Dictionary<string, Sound>();
        foreach (Sound sound in sounds)
        {
            sound.AudioSource = gameObject.AddComponent<AudioSource>();
            sound.AudioSource.clip = sound.audioClip;
            soundDictionary[sound.name] = sound;
        }
    }

    public static void Play(string name)
    {
        if (soundDictionary.ContainsKey(name))
        {
            Sound sound = soundDictionary[name];
            sound.AudioSource.Play();
        }
        else
        {
            Debug.Log("No sound with name " + name);
        }
    }

    public static void PlayRandom(string name, int numOptions)
    {
        int number = UnityEngine.Random.Range(0, numOptions);
        Play(name + number);
    }

    public static void PlayCraftSound()
    {
        PlayRandom("craft", 4);
    }

    public static void PlayPlaceSound()
    {
        PlayRandom("place", 3);
    }

    public static void PlayLiftSound()
    {
        PlayRandom("lift", 4);
    }
}
