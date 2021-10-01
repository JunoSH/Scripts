using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEscripts : MonoBehaviour
{
    [SerializeField]
    private Sounds sounds;
    private static Sounds sound;
    AudioSource audiosource;
    void Start()
    {
        if(sounds == null)
        {
            Debug.LogError("soundÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
        }
        sound = sounds;
        audiosource = GetComponent<AudioSource>();
    }
    public static AudioClip GetPushButton()
    {
        return sound.PushButton;
    }

    public static AudioClip GetIncorrect()
    {
        return sound.Incorrect;
    }
    public static AudioClip GetCorrect()
    {
        return sound.Correct;
    }
}
