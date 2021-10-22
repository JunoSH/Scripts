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

    public CopiedQuestion copiedquestion; //　各ScriptableObjectコピー先


    public GameObject seikai;　//　〇イラスト
    public GameObject fuseikai; //　×イラスト
    public GameObject tsugi;   //正解・不正解後、次の問題に飛ぶボタン
    public GameObject CheckAns;　//不正解の時、正解はどれだったかを示す
    public GameObject GO; //ゲームオーバー画面に飛ばすボタン
    public GameObject C; //クリア画面に飛ばすボタン
    public Text timetext;
    public float countdown; //制限時間
    public float countdownReset; //制限時間をリセット
    bool TimeStop;
    public Image TimeBar;


    int Count = 0; //問題切替
    int Qcount = 1;//今何問目か
    int Qn; //問題番号
    int judge = 0; //判定用
    public static int Score = 0; //最終スコア
    int gameover = 0; //ゲームオーバーまであと何点か
    public int clearCount = 0;

    //public static int lastScore = 0; //前回のスコア


    public GameObject PausePanel;  //ポーズボタンで出てくるUI

    AudioSource audioSource;
    float NowVolume;

    List<int> bn = new List<int>();　//問題番号Qnとランダムに選ばれた適当な数字を3つ入れる
    List<int> num = new List<int>(); //出題済問題番号

    [System.Serializable]
    public class SavedataALL 
    {
        public List<Savedata> savedataList = new List<Savedata>();
    }
    private SavedataALL savedataall = new SavedataALL();
    
    [System.Serializable]
    public class Savedata
    {
        public int changequizID;  //和名当てか歴史・特性か  
        public int QuestionID;    //問題番号    
        public int totalcount;    //問題毎の遭遇した回数
        public int SeikaiSu;    　//正解数
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
        Debug.Log("clearCountは"+clearCount);
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
       
        Text Qnum = GameObject.Find("Number").GetComponent<Text>(); //  今何問目か
        Qnum.text = Qcount.ToString() + "/10問目";
        Text Qtext = GameObject.Find("Sentense").GetComponent<Text>();  //問題文
        Qtext.text = copiedquestion.CopyList[Qn].Sentense;

        Text answer1 = GameObject.Find("AT1").GetComponent<Text>(); //左上ボタン
        answer1.text = copiedquestion.CopyList[bn[0]].Answer;
        Text answer2 = GameObject.Find("AT2").GetComponent<Text>(); //右上ボタン
        answer2.text = copiedquestion.CopyList[bn[1]].Answer;
        Text answer3 = GameObject.Find("AT3").GetComponent<Text>(); //左下ボタン
        answer3.text = copiedquestion.CopyList[bn[2]].Answer;
        Text answer4 = GameObject.Find("AT4").GetComponent<Text>(); //右下ボタン
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

    void Hantei(bool TimeOver)　//解答の正否を判定する
    {
        int changequizID = SettingScript.GetChangeQuiz();

        //Debug.Log(judge);
        if (TimeOver || Qn != bn[judge])//出題番号と、bnリストの中身が違うのであれば = 不正解の場合
        {
            gameover++;
            Zanki();
            fuseikai.transform.position = new Vector3(0, 0, 0);
            tsugi.transform.position = new Vector3(0, 0, 0);
            audioSource.PlayOneShot(SEscripts.GetIncorrect());
            int indexNum = bn.IndexOf(Qn); //正解はリストのどこか
            //CheckAns.transform.position = GameObject.Find(indexNum.ToString()).transform.position;
            Vector3 Pos = GameObject.Find(indexNum.ToString()).transform.position;
            CheckAns.transform.SetParent(GameObject.Find(indexNum.ToString()).transform);
            CheckAns.transform.localPosition = new Vector3(Pos.x-115, Pos.y ,  Pos.z);
            Saveseikai(changequizID, Qn, false);

        }
        else //出題番号と、bnリストの中身が同じであれば　＝　正解の場合
        {
            Score++;
            int indexNum = bn.IndexOf(Qn);
            GameObject.Find(indexNum.ToString()).GetComponent<Image>().color = new Color(0.5f,0.8f,1);
            seikai.transform.position = new Vector3(0, 0, 0);
            tsugi.transform.position = new Vector3(0, 0, 0);
            audioSource.PlayOneShot(SEscripts.GetCorrect());

            if (Qcount == 10) //解答を終えたタイミング且つ、次の問題へ飛ぶ前
            {
                tsugi.transform.position = new Vector3(0, 10000, 0);
                C.transform.position = new Vector3(0, 0, 0); //代わりにクリア画面へと飛ぶ

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

    public void Shutsudai()　//直前の問題文と選択肢を選出
    {
        if (Score >= 1)
        {
            int indexNum = bn.IndexOf(Qn);
            GameObject.Find(indexNum.ToString()).GetComponent<Image>().color = new Color(1, 1, 1);
        }

        bn.Clear(); //選択肢全て削除
        while (num.Contains(Qn))  //出題済みリストにQnが含まれている
        {
            Qn = Random.Range(0, copiedquestion.CopyList.Count);  //numリストに入っているQn以外の数字を取得。これが次の問題となる
        }

        num.Add(Qn); //出題した問題の番号を出題済みリストに
        bn.Add(Qn);　//正解を入れて置く
        for (int n = 0; n < 3; n++)
        {
            int tempBn = 0; //仮の変数を作り、その変数で取得できた値がbnリストにあるかどうか判定し、Qnとの重複を避ける
            do { tempBn = Random.Range(0, copiedquestion.CopyList.Count); }
            while (bn.Contains(tempBn));

            bn.Add(tempBn); //bnの中に答えの番号と間違いの番号３つが入る

        }
        bn = bn.OrderBy(b => System.Guid.NewGuid()).ToList(); //リストの中の順番を入れ替えて作り直している

    }

    public void NextQuestion() //次の問題へ移る直前で、解答前の状況に戻す
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

    void Zanki() //間違えた回数を示すハートを消す
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

    public static int GetScore()//クリア画面でのスコア表示
    {
        return Score;
    }

    public void Clear() //クリア画面呼び出し
    {
        clearCount ++;
        PlayerPrefs.SetInt(Define.KeyClearCount, clearCount);
        //Debug.Log("クリア時のclearCountは" + clearCount);
        SceneManager.LoadScene("Clear");
    }

    public void GameOver()　//ゲームオーバー画面呼び出し
    {
        SceneManager.LoadScene("GameOver");
    }

    void Timer()　//タイムバー設定
    {

        if (TimeStop == true)
        {
            TimeBar.fillAmount -= (TimeBar.fillAmount / countdown)*Time.deltaTime;
            countdown -= Time.deltaTime;
            timetext.text = countdown.ToString("f1");
            if(countdown < 0)　
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
