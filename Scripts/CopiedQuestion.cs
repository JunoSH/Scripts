using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CopiedQuestion", menuName = "ScriptableObject/CopiedQuestion")]
public class CopiedQuestion : ScriptableObject
{
    public List<Copy> CopyList = new List<Copy>();
}
[System.Serializable]

public class Copy
{
    public int ID;
    public string Answer;
    public string Sentense;

    public int stoneIDforTips;
}
