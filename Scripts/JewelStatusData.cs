using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JewelDetails", menuName = "ScriptableObject/JewelDetails")] //���O���ꏏ�ɂȂ邩��ԈႦ�Ȃ�
public class JewelStatusData : ScriptableObject
{
    public List<JewelStatus> JewelStatusList = new List<JewelStatus>();
}


[System.Serializable]

public class JewelStatus 
{
    public GameObject PrefabBtn;
    public int JewelID;
    public string Name; //���O   
    public string Wamei;�@//�a��    
    public string chemical;//���w��
    public string color;//�F
    public string production;//�Y�n
    public string MH; //���[�X�d�xMohs hardness
}