using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class PrefabGe : MonoBehaviour
{
    public List<Questions> prefabButtonList;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject prefabSource;
    public int selectingChangeQuizID;
    [SerializeField] Stonedatas stonedatas;

    Director.SavedataALL saveDataAll = new Director.SavedataALL();
    // Start is called before the first frame update
    public void Start()
    {
        
        Debug.Log(stonedatas);
    }

    
    public void ChooseDetail(int n)
    {       
        //copiedButton.CopyList.Clear();
        /*
        foreach (QS qs in prefabButtonList[n].Qsentenses)
        {
            Copy list = new Copy
            {
                ID = qs.QuestionID,
                Answer = qs.QuestionAnswer,
                Sentense = qs.QuestionSentenses,
                
                correctCount = qs.correctcount,
                Copiedpercentage = qs.percentage,

                nameInTips = qs.Name,
                wameiInTips = qs.Wamei,
                colorInTips = qs.color,
                productionInTips = qs.production,
                MHinTips = qs.MH
            };
            copiedButton.CopyList.Add(list);
        }
        UnityEditor.EditorUtility.SetDirty(copiedButton);
        */
    }
    
    public void HistoryCharacteristicDetail(int a)
    {
        selectingChangeQuizID = a;
        int n = prefabButtonList[selectingChangeQuizID].Qsentenses.Count;
        for (int i = 0; i < n; i++)
        {
            GameObject PF = Instantiate(prefabSource);
            PF.transform.SetParent(GameObject.Find("JewelContent").transform, false);
            PF.name = i.ToString();
            Text PFname = PF.transform.GetChild(0).GetComponent<Text>();
            PFname.text = prefabButtonList[selectingChangeQuizID].Qsentenses[i].QuestionAnswer;
            
        }
        
    }
    public void loaddata()
    {
        string filePath = Application.dataPath + "/../savedataall" + ".json";
        if (File.Exists(filePath) == true)
        {
            StreamReader reader;
            reader = new StreamReader(Application.dataPath + "/../savedataall" + ".json");
            saveDataAll = JsonUtility.FromJson<Director.SavedataALL>(reader.ReadToEnd());
            reader.Close();
        }

    }
    public void push(Button sender)
    {
        loaddata();
        int ID = int.Parse(sender.name);  //�{�^���̖��O��0����̐����ɂ��Ă���
        int _stoneiD = prefabButtonList[selectingChangeQuizID].Qsentenses[ID].stoneid;  //prefabbutton���X�g�ɂ�����scriptableobject�ɓ��͂���stoneID�����B�΂̋�ʂ�t����
        Debug.Log(stonedatas);
        Debug.Log(stonedatas.stonedataList);
        Stonedata stonedata = stonedatas.stonedataList.Find(p => p.stoneID == _stoneiD);�@�@//stonedataList�ɓ�����stoneID��sender��������ꂽID�őI�ʂ��ꂽstoneID����v����stonedata�����
                                                                                           //ID��v��stonedata�ɓ������e�f�[�^��\������̂��ȉ��̃R�[�h

        Text name = GameObject.Find("Name").GetComponent<Text>();
        name.text = "��" + stonedata.Name;

        Text japName = GameObject.Find("JapName").GetComponent<Text>();
        japName.text = "�E�a��: " + stonedata.Wamei;

        Text color = GameObject.Find("color").GetComponent<Text>();
        color.text = "�E�F: " + stonedata.color;
        
        Text productionArea = GameObject.Find("PA").GetComponent<Text>();
        productionArea.text = "�E�Y�n: " + stonedata.production;
        
        Text mohshardness = GameObject.Find("MohsHardness").GetComponent<Text>();
        mohshardness.text = "�E���[�X�d�x: " + stonedata.MH;


        int _quizID = selectingChangeQuizID;
        int _questionID = prefabButtonList[selectingChangeQuizID].Qsentenses[ID].QuestionID;
        Director.Savedata d = saveDataAll.savedataList.Find(p => p.changequizID == _quizID && p.QuestionID == _questionID);
        Text seitousu = GameObject.Find("Seitouritsu").GetComponent<Text>();
        if (d == null)
        {
            seitousu.text = "����: " + 0 + "��";
        }
        else
        {
            seitousu.text = "����: " + d.totalcount.ToString() + "��";
        }
    }
}
