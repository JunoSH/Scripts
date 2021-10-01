using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question2", menuName = "ScriptableObject/Question2")]
public class Question2 : ScriptableObject
{
    public List<JewelNameQuiz> NameQuizList = new List<JewelNameQuiz>();
}
[System.Serializable]
public class JewelNameQuiz
{
    public string JewelNameAnswer;
    public string NameQuestion;
}
