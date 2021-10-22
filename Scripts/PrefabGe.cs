using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PrefabGe : MonoBehaviour
{
    public List<Questions> prefabButtonList;
    public CopiedQuestion copiedButton;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject prefabSource;
    private int selectingChangeQuizID;
    [SerializeField] Stonedatas stonedatas;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(stonedatas);
    }


    public void ChooseDetail(int n)
    {
        selectingChangeQuizID = n;
        copiedButton.CopyList.Clear();

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
    }


    public void HistoryCharacteristicDetail(int a)
    {
        ChooseDetail(a);
        int n = copiedButton.CopyList.Count;
        Debug.Log("copiedList�̒��g��" + n +"�ł�");

        for (int i = 0; i < n; i++)
        {
            GameObject PF = Instantiate(prefabSource);
            PF.transform.SetParent(GameObject.Find("JewelContent").transform, false);
            PF.name = i.ToString();
            Text PFname = PF.transform.GetChild(0).GetComponent<Text>();
            PFname.text = copiedButton.CopyList[i].Answer;
            
        }
        
    }
    /*
    public void PlayRekishiTokusei()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject PF = Instantiate(PrefabButton.PrefabBtn);
            PF.transform.SetParent(GameObject.Find("JewelContent").transform, false);
            PF.name = i.ToString();
            Text PFname = PF.transform.GetChild(0).GetComponent<Text>();
            PFname.text = dd.JewelStatusList[i].Name;
        }
    }
    */
    public void push(Button sender)
    {
        int ID = int.Parse(sender.name);
        int stoneiD = prefabButtonList[selectingChangeQuizID].Qsentenses[ID].stoneid;
        Debug.Log(stonedatas);
        Debug.Log(stonedatas.stonedataList);
        Stonedata stonedata = stonedatas.stonedataList.Find(p => p.stoneID == stoneiD);
        


        Text name = GameObject.Find("Name").GetComponent<Text>();
        name.text = "��" + stonedata.Name;

        Text japName = GameObject.Find("JapName").GetComponent<Text>();
        japName.text = "�E�a��: " + copiedButton.CopyList[ID].wameiInTips;

        Text correctfrequency = GameObject.Find("CorrectFrequency").GetComponent <Text>();
        correctfrequency.text = "�E����: "+ copiedButton.CopyList[ID].correctCount.ToString() + "��";
        
        Text color = GameObject.Find("color").GetComponent<Text>();
        color.text = "�E�F: " + copiedButton.CopyList[ID].colorInTips;
        
        Text productionArea = GameObject.Find("PA").GetComponent<Text>();
        productionArea.text = "�E�Y�n: " + copiedButton.CopyList[ID].productionInTips;
        
        Text mohshardness = GameObject.Find("MohsHardness").GetComponent<Text>();
        mohshardness.text = "�E���[�X�d�x: " + copiedButton.CopyList[ID].MHinTips.ToString();

        Text percentage = GameObject.Find("Seitouritsu").GetComponent<Text>();
        percentage.text = "�E������: " + copiedButton.CopyList[ID].Copiedpercentage + "%";
    }
}
