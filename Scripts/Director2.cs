using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Director2 : MonoBehaviour
{
    public JewelStatusData DD;
    public Sounds sound;
    public Question2 Q2;  //ScriptableObject置き場

    public GameObject seikai2;　//　〇イラスト
    public GameObject fuseikai2; //　×イラスト
    public GameObject tsugi2;   //正解・不正解後、次の問題に飛ぶボタン
    public GameObject CheckAns2;　//不正解の時、正解はどれだったかを示す
    public GameObject GO2; //ゲームオーバー画面に飛ばすボタン
    public GameObject C2; //クリア画面に飛ばすボタン

    public Text timetext2;
    public float countdown2; //制限時間
    public float countdownReset2; //制限時間をリセットするため
    bool TimeStop2;
    public Image TimeBar2;

    int Count = 0; //問題切替
    int Qcount = 1;//今何問目か
    int Qn = 0; //問題番号
    int judge = 0; //判定用
    public static int Score = 0; //最終スコア
    int gameover = 0; //ゲームオーバーまであと何点か

    public GameObject PausePanel;  //ポーズボタンで出てくるUI

    AudioSource audioSource;
    float NowVolume;

    List<int> bn = new List<int>();　//問題番号Qnとランダムに選ばれた適当な数字を3つ入れる
    List<int> num = new List<int>(); //出題済問題番号

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        countdown2 = SettingScript.GetTimeLimitSetting();
        countdownReset2 = SettingScript.GetTimeLimitSetting();
        if (countdown2 == 0) //settingせずにスタートする、またはsettingを開いて何も入力されていない場合15秒
        {
            countdown2 = 15; //defaultTime
            countdownReset2 = 15;
            print("0start");
        }
        PausePanel.SetActive(false);
        Qn = Random.Range(0, 15);  //ランダムな問題番号
        Shutsudai();
        TimeStop2 = true;

        //Debug.Log($"Count = {Count} Qcount = {Qcount}");
        audioSource = GetComponent<AudioSource>();
        NowVolume = SettingScript.VolumeValue;
        audioSource.volume = NowVolume;
        Debug.Log("音量レベルは "+NowVolume);

    }

    // Update is called once per frame
    void Update()
    {
        Text Qnum = GameObject.Find("Number").GetComponent<Text>(); //  今何問目か
        Qnum.text = Qcount.ToString() + "/10問目";
        Text Qtext = GameObject.Find("Sentense").GetComponent<Text>();  //問題文
        Qtext.text = Q2.NameQuizList[Qn].NameQuestion;

        Text answer1 = GameObject.Find("AT1").GetComponent<Text>(); //左上ボタン
        answer1.text = Q2.NameQuizList[bn[0]].JewelNameAnswer;
        Text answer2 = GameObject.Find("AT2").GetComponent<Text>(); //右上ボタン
        answer2.text = Q2.NameQuizList[bn[1]].JewelNameAnswer;
        Text answer3 = GameObject.Find("AT3").GetComponent<Text>(); //左下ボタン
        answer3.text = Q2.NameQuizList[bn[2]].JewelNameAnswer;
        Text answer4 = GameObject.Find("AT4").GetComponent<Text>(); //右下ボタン
        answer4.text = Q2.NameQuizList[bn[3]].JewelNameAnswer;

        if (Count == Qcount)
        {
            Qcount++;
        }
        Timer();
        colorchange(TimeBar2.fillAmount);

    }

    public void AnswerButton1(Button sender)  //0
    {
        judge = int.Parse(sender.name);
        Hantei(false);
        TimeStop2 = false;

    }
    public void AnswerButton2(Button sender) //1
    {
        judge = int.Parse(sender.name);
        Hantei(false);
        TimeStop2 = false;

    }
    public void AnswerButton3(Button sender)//2
    {
        judge = int.Parse(sender.name);
        Hantei(false);
        TimeStop2 = false;

    }
    public void AnswerButton4(Button sender) //3
    {
        judge = int.Parse(sender.name);
        Hantei(false);
        TimeStop2 = false;

    }

    void Hantei(bool TimeOver)　//解答の正否を判定する
    {
        Debug.Log(judge);
        if (TimeOver || Qn != bn[judge])//出題番号と、bnリストの中身が違うのであれば
        {
            gameover++;
            Zanki();
            fuseikai2.transform.position = new Vector3(0, 0, 0);
            tsugi2.transform.position = new Vector3(0, 0, 0);
            audioSource.PlayOneShot(sound.Incorrect);
            int indexNum = bn.IndexOf(Qn); //正解はリストのどこか
            Vector3 Pos = GameObject.Find(indexNum.ToString()).transform.position;
            CheckAns2.transform.SetParent(GameObject.Find(indexNum.ToString()).transform);
            CheckAns2.transform.localPosition = new Vector3(Pos.x - 115, Pos.y, Pos.z);
            /*
            switch (indexNum)
            {
                case 0:
                    CheckAns.transform.position = GameObject.Find("0").transform.position;
                    break;
                case 1:
                    CheckAns.transform.position = GameObject.Find("1").transform.position;
                    break;
                case 2:
                    CheckAns.transform.position = GameObject.Find("2").transform.position;
                    break;
                case 3:
                    CheckAns.transform.position = GameObject.Find("3").transform.position;
                    break;
            }
            */
        }
        else //出題番号と、bnリストの中身が同じであれば
        {
            Score++;
            int indexNum = bn.IndexOf(Qn);
            GameObject.Find(indexNum.ToString()).GetComponent<Image>().color = new Color(0.5f, 0.8f, 1);
            seikai2.transform.position = new Vector3(0, 0, 0);
            tsugi2.transform.position = new Vector3(0, 0, 0);
            audioSource.PlayOneShot(sound.Correct);
            if (Qcount == 10) //解答を終えたタイミング且つ、次の問題へ飛ぶ前
            {
                tsugi2.transform.position = new Vector3(0, 10000, 0);
                C2.transform.position = new Vector3(0, 0, 0);　//代わりにクリア画面へと飛ぶ
            }

        }

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
            Qn = Random.Range(0, 15);  //numリストに入っているQn以外の数字を取得。これが次の問題となる
        }
        num.Add(Qn); //出題した問題の番号を出題済みリストに
        bn.Add(Qn);　//正解を入れて置く
        for (int n = 0; n < 3; n++)
        {
            int tempBn = 0; //仮の変数を作り、その変数で取得できた値がbnリストにあるかどうか判定し、Qnとの重複を避ける
            do { tempBn = Random.Range(0, 15); }
            while (bn.Contains(tempBn));

            bn.Add(tempBn); //bnの中に答えの番号と間違いの番号３つが入る

        }
        bn = bn.OrderBy(b => System.Guid.NewGuid()).ToList(); //リストの中の順番を入れ替えて作り直している

    }

    public void NextQuestion() //次の問題へ移る直前で、解答前の状況に戻す
    {
        Count++;
        TimeBar2.fillAmount = 1;
        Shutsudai();
        countdown2 = countdownReset2;
        TimeStop2 = true;
        seikai2.transform.position = new Vector3(0, 10000, 0);
        fuseikai2.transform.position = new Vector3(0, 10000, 0);
        tsugi2.transform.position = new Vector3(0, 10000, 0);
        CheckAns2.transform.position = new Vector3(0, 10000, 0);
        //Debug.Log($"Count = {Count} Qcount = {Qcount}");

    }

    void Zanki() //間違えた回数を示すハートを消す
    {
        if (gameover == 1)
        {
            GameObject.Find("lifeF").transform.position = new Vector3(0, 10000, 0);
        }
        else if (gameover == 2)
        {
            GameObject.Find("lifeS").transform.position = new Vector3(0, 10000, 0);
        }
        else
        {
            GameObject.Find("lifeT").transform.position = new Vector3(0, 10000, 0);
            GO2.transform.position = new Vector3(0, 0, 0);
        }
    }

    public static int GetScore2()//クリア画面でのスコア表示
    {
        return Score;
    }

    public void Clear()
    {
        SceneManager.LoadScene("Clear");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    void Timer()
    {

        if (TimeStop2 == true)
        {
            TimeBar2.fillAmount -= (TimeBar2.fillAmount / countdown2) * Time.deltaTime;
            countdown2 -= Time.deltaTime;
            timetext2.text = countdown2.ToString("f1");
            if (countdown2 < 0)
            {
                TimeStop2 = false;
                Hantei(true);
            }
        }

    }

    void colorchange(float value)
    {
        if (value > 0.664f)
        {
            TimeBar2.color = new Color(0.19f, 0.54f, 1.0f, 1);
            //timetext.color = new Color(0.19f, 0.54f, 1.0f, 1);
        }
        else if (value > 0.33f)
        {
            TimeBar2.color = new Color(1.0f, 1.0f, 0.32f, 1);
            //timetext.color = new Color(1.0f, 1.0f, 0.32f, 1);
        }
        else
        {
            TimeBar2.color = new Color(1.0f, 0.16f, 0.2f, 1);
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
}
