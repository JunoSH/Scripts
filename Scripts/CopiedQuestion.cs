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

    public float correctCount;
    public string Copiedpercentage;

    public string nameInTips; //名前   
    public string wameiInTips;　//和名    
    public string colorInTips;//色
    public string productionInTips;//産地
    public string MHinTips; //モース硬度Mohs hardness
}
