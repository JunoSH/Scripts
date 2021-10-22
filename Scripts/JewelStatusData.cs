using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JewelDetails", menuName = "ScriptableObject/JewelDetails")] //名前が一緒になるから間違えない
public class JewelStatusData : ScriptableObject
{
    public List<JewelStatus> JewelStatusList = new List<JewelStatus>();
}


[System.Serializable]

public class JewelStatus 
{
    public GameObject PrefabBtn;
    public int JewelID;
    public string Name; //名前   
    public string Wamei;　//和名    
    public string chemical;//化学名
    public string color;//色
    public string production;//産地
    public string MH; //モース硬度Mohs hardness
}