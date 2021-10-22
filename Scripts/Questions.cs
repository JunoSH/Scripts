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

    public string Name; //名前   
    public string Wamei;　//和名    
    public string color;//色
    public string production;//産地
    public string MH; //モース硬度Mohs hardness


}


