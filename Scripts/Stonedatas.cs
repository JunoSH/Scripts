using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "stonedata", menuName = "ScriptableObject/stonedata")]

public class Stonedatas: ScriptableObject
{
    public List<Stonedata> stonedataList;
}
[System.Serializable]

public class Stonedata 
{
    public int stoneID;
    public string Name; //名前   
    public string Wamei;　//和名    
    public string color;//色
    public string production;//産地
    public string MH; //モース硬度Mohs hardness
}
