using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sounds", menuName = "ScriptableObject/Sounds")]

public class Sounds : ScriptableObject
{
    public AudioClip Correct;
    public AudioClip Incorrect;
    public AudioClip PushButton;
}
[System.Serializable] 
public class SoundData
{
    public AudioClip Correct;
    public AudioClip Incorrect;
    
}


