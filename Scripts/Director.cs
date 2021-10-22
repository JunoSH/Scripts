using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;

public class Director : MonoBehaviour
{
    public List<Questions> questions;

    public CopiedQuestion copiedquestion; //�@�eScriptableObject�R�s�[��


    public GameObject seikai;�@//�@�Z�C���X�g
    public GameObject fuseikai; //�@�~�C���X�g
    public GameObject tsugi;   //�����E�s������A���̖��ɔ�ԃ{�^��
    public GameObject CheckAns;�@//�s�����̎��A�����͂ǂꂾ������������
    public GameObject GO; //�Q�[���I�[�o�[��ʂɔ�΂��{�^��
    public GameObject C; //�N���A��ʂɔ�΂��{�^��
    public Text timetext;
    public float countdown; //��������
    public float countdownReset; //�������Ԃ����Z�b�g
    bool TimeStop;
    public Image TimeBar;


    int Count = 0; //���ؑ�
    int Qcount = 1;//������ڂ�
    int Qn; //���ԍ�
    int judge = 0; //����p
    public static int Score = 0; //�ŏI�X�R�A
    int gameover = 0; //�Q�[���I�[�o�[�܂ł��Ɖ��_��
    public int clearCount = 0;

    //public static int lastScore = 0; //�O��̃X�R�A


    public GameObject PausePanel;  //�|�[�Y�{�^���ŏo�Ă���UI

    AudioSource audioSource;
    float NowVolume;

    List<int> bn = new List<int>();�@//���ԍ�Qn�ƃ����_���ɑI�΂ꂽ�K���Ȑ�����3�����
    List<int> num = new List<int>(); //�o��ϖ��ԍ�

    [System.Serializable]
    public class SavedataALL 
    {
        public List<Savedata> savedataList = new List<Savedata>();
    }
    private SavedataALL savedataall = new SavedataALL();
    
    [System.Serializable]
    public class Savedata
    {
        public int changequizID;  //�a�����Ă����j�E������  
        public int QuestionID;    //���ԍ�    
        public int totalcount;    //��薈�̑���������
        public int SeikaiSu;    �@//����
    }
    Savedata savedata = new Savedata();
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        countdown = SettingScript.GetTimeLimitSetting();
        countdownReset = SettingScript.GetTimeLimitSetting();
        if (countdown == 0) 
        {
            countdown = 15; //defaultTime
            countdownReset = 15;
        }
        PausePanel.SetActive(false);

        
        int a = SettingScript.GetChangeQuiz();
        MondaiSentaku(a);
        Qn = Random.Range(0, copiedquestion.CopyList.Count);
        Shutsudai();
        TimeStop = true;


        //Debug.Log($"Count = {Count} Qcount = {Qcount}");
        audioSource = GetComponent<AudioSource>();
        NowVolume = SettingScript.VolumeValue;
        audioSource.volume = NowVolume;


        clearCount = PlayerPrefs.GetInt(Define.KeyClearCount, 0);
        Debug.Log("clearCount��"+clearCount);
        string filePath = Application.dataPath + "/../savedataall" + ".json";
        if (File.Exists(filePath) == true)
        {
            StreamReader reader;
            reader = new StreamReader(Application.dataPath + "/../savedataall" + ".json");
            savedataall = JsonUtility.FromJson<SavedataALL>(reader.ReadToEnd());
            reader.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        Text Qnum = GameObject.Find("Number").GetComponent<Text>(); //  ������ڂ�
        Qnum.text = Qcount.ToString() + "/10���";
        Text Qtext = GameObject.Find("Sentense").GetComponent<Text>();  //��蕶
        Qtext.text = copiedquestion.CopyList[Qn].Sentense;

        Text answer1 = GameObject.Find("AT1").GetComponent<Text>(); //����{�^��
        answer1.text = copiedquestion.CopyList[bn[0]].Answer;
        Text answer2 = GameObject.Find("AT2").GetComponent<Text>(); //�E��{�^��
        answer2.text = copiedquestion.CopyList[bn[1]].Answer;
        Text answer3 = GameObject.Find("AT3").GetComponent<Text>(); //�����{�^��
        answer3.text = copiedquestion.CopyList[bn[2]].Answer;
        Text answer4 = GameObject.Find("AT4").GetComponent<Text>(); //�E���{�^��
        answer4.text = copiedquestion.CopyList[bn[3]].Answer;
   

        if (Count == Qcount)
        {
            Qcount++;
        }
        Timer();
        colorchange(TimeBar.fillAmount);

    }
    public void AnswerButton1(Button sender)  //0
    {
        judge = int.Parse(sender.name);
        Hantei(false);
        TimeStop = false;

    }
    public void AnswerButton2(Button sender) //1
    {
        judge = int.Parse(sender.name);
        Hantei(false);
        TimeStop = false;
 
    }
    public void AnswerButton3(Button sender)//2
    {
        judge = int.Parse(sender.name);
        Hantei(false);
        TimeStop = false;

    }
    public void AnswerButton4(Button sender) //3
    {
        judge = int.Parse(sender.name);
        Hantei(false);
        TimeStop = false;

    }

    void Hantei(bool TimeOver)�@//�𓚂̐��ۂ𔻒肷��
    {
        int changequizID = SettingScript.GetChangeQuiz();

        //Debug.Log(judge);
        if (TimeOver || Qn != bn[judge])//�o��ԍ��ƁAbn���X�g�̒��g���Ⴄ�̂ł���� = �s�����̏ꍇ
        {
            gameover++;
            Zanki();
            fuseikai.transform.position = new Vector3(0, 0, 0);
            tsugi.transform.position = new Vector3(0, 0, 0);
            audioSource.PlayOneShot(SEscripts.GetIncorrect());
            int indexNum = bn.IndexOf(Qn); //�����̓��X�g�̂ǂ���
            //CheckAns.transform.position = GameObject.Find(indexNum.ToString()).transform.position;
            Vector3 Pos = GameObject.Find(indexNum.ToString()).transform.position;
            CheckAns.transform.SetParent(GameObject.Find(indexNum.ToString()).transform);
            CheckAns.transform.localPosition = new Vector3(Pos.x-115, Pos.y ,  Pos.z);
            Saveseikai(changequizID, Qn, false);

        }
        else //�o��ԍ��ƁAbn���X�g�̒��g�������ł���΁@���@�����̏ꍇ
        {
            Score++;
            int indexNum = bn.IndexOf(Qn);
            GameObject.Find(indexNum.ToString()).GetComponent<Image>().color = new Color(0.5f,0.8f,1);
            seikai.transform.position = new Vector3(0, 0, 0);
            tsugi.transform.position = new Vector3(0, 0, 0);
            audioSource.PlayOneShot(SEscripts.GetCorrect());

            if (Qcount == 10) //�𓚂��I�����^�C�~���O���A���̖��֔�ԑO
            {
                tsugi.transform.position = new Vector3(0, 10000, 0);
                C.transform.position = new Vector3(0, 0, 0); //����ɃN���A��ʂւƔ��

            }
           
            Saveseikai(changequizID, Qn , true);
        }

    }

    public void MondaiSentaku(int n)
    {
        copiedquestion.CopyList.Clear();
        foreach (QS j in questions[n].Qsentenses)
        {
            Copy q = new Copy
            {
                ID = j.QuestionID,
                Answer = j.QuestionAnswer,
                Sentense = j.QuestionSentenses
            };
            copiedquestion.CopyList.Add(q);    
        }
        UnityEditor.EditorUtility.SetDirty(copiedquestion);

        GameObject QText = GameObject.Find("Sentense");
        Text Alignment = QText.GetComponent<Text>();
        Alignment.alignment = questions[n].textAnchor;
        Alignment.fontSize = questions[n].fontsize;


        /*
        switch(n)
        {
            case 1:
                
                Alignment.fontSize = questions[n].fontsize;
                break;
        }
        */

    }

    public void Shutsudai()�@//���O�̖�蕶�ƑI������I�o
    {
        if (Score >= 1)
        {
            int indexNum = bn.IndexOf(Qn);
            GameObject.Find(indexNum.ToString()).GetComponent<Image>().color = new Color(1, 1, 1);
        }

        bn.Clear(); //�I�����S�č폜
        while (num.Contains(Qn))  //�o��ς݃��X�g��Qn���܂܂�Ă���
        {
            Qn = Random.Range(0, copiedquestion.CopyList.Count);  //num���X�g�ɓ����Ă���Qn�ȊO�̐������擾�B���ꂪ���̖��ƂȂ�
        }

        num.Add(Qn); //�o�肵�����̔ԍ����o��ς݃��X�g��
        bn.Add(Qn);�@//���������Ēu��
        for (int n = 0; n < 3; n++)
        {
            int tempBn = 0; //���̕ϐ������A���̕ϐ��Ŏ擾�ł����l��bn���X�g�ɂ��邩�ǂ������肵�AQn�Ƃ̏d���������
            do { tempBn = Random.Range(0, copiedquestion.CopyList.Count); }
            while (bn.Contains(tempBn));

            bn.Add(tempBn); //bn�̒��ɓ����̔ԍ��ƊԈႢ�̔ԍ��R������

        }
        bn = bn.OrderBy(b => System.Guid.NewGuid()).ToList(); //���X�g�̒��̏��Ԃ����ւ��č�蒼���Ă���

    }

    public void NextQuestion() //���̖��ֈڂ钼�O�ŁA�𓚑O�̏󋵂ɖ߂�
    {
        Count++;
        TimeBar.fillAmount = 1;
        Shutsudai();
        countdown = countdownReset;
        TimeStop = true;
        seikai.transform.position = new Vector3(0, 10000, 0);
        fuseikai.transform.position = new Vector3(0, 10000, 0);
        tsugi.transform.position = new Vector3(0, 10000, 0);
        CheckAns.transform.position = new Vector3(0, 10000, 0);

    }

    void Zanki() //�ԈႦ���񐔂������n�[�g������
    {
        if(gameover == 1)
        {
            GameObject.Find("lifeF").transform.position = new Vector3(0, 10000, 0);
        }
        else if(gameover == 2)
        {
            GameObject.Find("lifeS").transform.position = new Vector3(0, 10000, 0);
        }
        else
        {
            GameObject.Find("lifeT").transform.position = new Vector3(0, 10000, 0);
            GO.transform.position = new Vector3(0, 0, 0);
        }
    }

    public static int GetScore()//�N���A��ʂł̃X�R�A�\��
    {
        return Score;
    }

    public void Clear() //�N���A��ʌĂяo��
    {
        clearCount ++;
        PlayerPrefs.SetInt(Define.KeyClearCount, clearCount);
        //Debug.Log("�N���A����clearCount��" + clearCount);
        SceneManager.LoadScene("Clear");
    }

    public void GameOver()�@//�Q�[���I�[�o�[��ʌĂяo��
    {
        SceneManager.LoadScene("GameOver");
    }

    void Timer()�@//�^�C���o�[�ݒ�
    {

        if (TimeStop == true)
        {
            TimeBar.fillAmount -= (TimeBar.fillAmount / countdown)*Time.deltaTime;
            countdown -= Time.deltaTime;
            timetext.text = countdown.ToString("f1");
            if(countdown < 0)�@
            {
                TimeStop = false;
                Hantei(true);
            }
        }

    }

    void colorchange(float value)
    {
        if (value > 0.664f)
        {
            TimeBar.color = new Color(0.19f, 0.54f, 1.0f, 1);
            //timetext.color = new Color(0.19f, 0.54f, 1.0f, 1);
        }
        else if (value > 0.33f)
        {
            TimeBar.color = new Color(1.0f, 1.0f, 0.32f, 1);
            //timetext.color = new Color(1.0f, 1.0f, 0.32f, 1);
        }
        else
        {
            TimeBar.color = new Color(1.0f, 0.16f, 0.2f, 1);
            //timetext.color = new Color(1.0f, 0.16f, 0.2f, 1);
        }
    }
    public void PauseButton()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
        PausePanel.GetComponent<CanvasGroup>().alpha = 1;
    }
    public void ResumeButton()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ReturnTitleButton()
    {
        SceneManager.LoadScene("TITLE");
        Time.timeScale = 1f;
    }




    public void Saveseikai(int _changequizID, int _QuestionID , bool isCorrected)
    {
        Savedata d = savedataall.savedataList.Find(p => p.changequizID == _changequizID && p.QuestionID == _QuestionID);
        if(d == null)
        {
            d = new Savedata { changequizID = _changequizID, QuestionID = _QuestionID };
            savedataall.savedataList.Add(d);
        }
        d.totalcount += 1;

        if (isCorrected == true)
        {
            d.SeikaiSu += 1;
            questions[_changequizID].Qsentenses[_QuestionID].correctcount = d.SeikaiSu;
            
            int c = PlayerPrefs.GetInt(Define.KeyClearCount, 0);

            if(clearCount > 0)
            {
                float seikaisu = questions[_changequizID].Qsentenses[_QuestionID].correctcount;
                float s = (c / (float)seikaisu) *100;
                string seitouritsu = s.ToString("f1");
                questions[_changequizID].Qsentenses[_QuestionID].percentage = seitouritsu;

                Debug.Log(seitouritsu);

            }
      
        }
        UnityEditor.EditorUtility.SetDirty(questions[_changequizID]);
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(savedataall);
        int SeikaiMondaiBangou = Qn;
        writer = new StreamWriter(Application.dataPath + "/../savedataall"+".json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    

    
}
