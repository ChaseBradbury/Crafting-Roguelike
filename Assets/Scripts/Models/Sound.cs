using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;
    public float volume;
    private AudioSource audioSource;

    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }
}
