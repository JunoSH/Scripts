using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Savedata
{
    public int CorrectedQuiz;
}

public class SaveData : MonoBehaviour
{

    Savedata savedata = new Savedata();
    [SerializeField] Text SeikaiSuText;
    void Start()
    {

        savedata.CorrectedQuiz = 0;

        string a = JsonUtility.ToJson(savedata);

        Debug.Log(a);
    }
    public void OnClickEvent()
    {
        savedata.CorrectedQuiz++;
        SeikaiSuText.text = savedata.CorrectedQuiz.ToString();
    }
    /*
    public void SaveSeikai()
    {
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(savedata);
        int seikai = Director.Qn;
        writer = new StreamWriter(Application.dataPath + "/save"  +"JSON"+ seikai + ".json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();

        //重要なのは名前決め。セーブロードのスクリプトをDirectorに持っていく。その上で、名前にscriptableobjectに設定したIDを使って、文字列と数字の差別化を図る


    }

    public void LoadSeikaisu()
    {
        string loaddata = "";
        int seikai = int.Parse(SeikaiSuText.text);
        StreamReader reader;

        reader = new StreamReader(Application.dataPath + "/save" + "JewelNameScore"+seikai + ".json");
        loaddata = reader.ReadToEnd();
        reader.Close();

        savedata = JsonUtility.FromJson<Savedata>(loaddata);
        SeikaiSuText.text = savedata.CorrectedQuiz.ToString();


    }
    */
}