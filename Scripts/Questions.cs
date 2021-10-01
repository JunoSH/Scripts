using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Questions", menuName = "ScriptableObject/Questions")]
public class Questions : ScriptableObject
{
    public List<QS> Qsentenses = new List<QS>();
}
[System.Serializable]

public class QS
{
    public int QuestionID;
    public string QuestionAnswer;   
    public string QuestionSentenses;   
}
