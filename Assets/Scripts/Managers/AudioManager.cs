using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private Sound[] sounds;
    [SerializeField] private Sound[] musics;
    private static Dictionary<string, Sound> soundDictionary;
    private static Dictionary<string, Sound> musicDictionary;
    private static AudioSource musicSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            soundDictionary = new Dictionary<string, Sound>();
            foreach (Sound sound in sounds)
            {
                sound.AudioSource = gameObject.AddComponent<AudioSource>();
                sound.AudioSource.clip = sound.audioClip;
                soundDictionary[sound.name] = sound;
            }
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicDictionary = new Dictionary<string, Sound>();
            foreach (Sound music in musics)
            {
                musicDictionary[music.name] = music;
            }
            PlayMusic("crafting", false);
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

    public static void PlayMusic(string name, bool queue)
    {
        if (musicDictionary.ContainsKey(name))
        {
            if (queue)
            {
                Instance.StartCoroutine(QueueMusic(name));
            }
            else
            {
                musicSource.clip = musicDictionary[name].audioClip;
                musicSource.volume = musicDictionary[name].volume;
                musicSource.Play();
            }

        }
    }
    
    public static IEnumerator QueueMusic(string name)
    {
        if (musicDictionary.ContainsKey(name))
        {
            yield return new WaitForSeconds(musicSource.clip.length);
            musicSource.clip = musicDictionary[name].audioClip;
            musicSource.volume = musicDictionary[name].volume;
            musicSource.Play();
        }
    }
}
