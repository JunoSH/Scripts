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
    public string Name; //���O   
    public string Wamei;�@//�a��    
    public string color;//�F
    public string production;//�Y�n
    public string MH; //���[�X�d�xMohs hardness
}
