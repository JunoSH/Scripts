using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Questions", menuName = "ScriptableObject/Questions")]
public class Questions : ScriptableObject
{
    public int fontsize;
    public TextAnchor textAnchor;
    public List<QS> Qsentenses = new List<QS>();
}
[System.Serializable]

public class QS
{
    public int QuestionID;
    public string QuestionAnswer;   
    public string QuestionSentenses;

    public float correctcount;
    public string percentage;

    public int stoneid;

    public string Name; //���O   
    public string Wamei;�@//�a��    
    public string color;//�F
    public string production;//�Y�n
    public string MH; //���[�X�d�xMohs hardness


}


