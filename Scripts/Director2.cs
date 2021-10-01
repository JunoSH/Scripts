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
    public Question2 Q2;  //ScriptableObject�u����

    public GameObject seikai2;�@//�@�Z�C���X�g
    public GameObject fuseikai2; //�@�~�C���X�g
    public GameObject tsugi2;   //�����E�s������A���̖��ɔ�ԃ{�^��
    public GameObject CheckAns2;�@//�s�����̎��A�����͂ǂꂾ������������
    public GameObject GO2; //�Q�[���I�[�o�[��ʂɔ�΂��{�^��
    public GameObject C2; //�N���A��ʂɔ�΂��{�^��

    public Text timetext2;
    public float countdown2; //��������
    public float countdownReset2; //�������Ԃ����Z�b�g���邽��
    bool TimeStop2;
    public Image TimeBar2;

    int Count = 0; //���ؑ�
    int Qcount = 1;//������ڂ�
    int Qn = 0; //���ԍ�
    int judge = 0; //����p
    public static int Score = 0; //�ŏI�X�R�A
    int gameover = 0; //�Q�[���I�[�o�[�܂ł��Ɖ��_��

    public GameObject PausePanel;  //�|�[�Y�{�^���ŏo�Ă���UI

    AudioSource audioSource;
    float NowVolume;

    List<int> bn = new List<int>();�@//���ԍ�Qn�ƃ����_���ɑI�΂ꂽ�K���Ȑ�����3�����
    List<int> num = new List<int>(); //�o��ϖ��ԍ�

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        countdown2 = SettingScript.GetTimeLimitSetting();
        countdownReset2 = SettingScript.GetTimeLimitSetting();
        if (countdown2 == 0) //setting�����ɃX�^�[�g����A�܂���setting���J���ĉ������͂���Ă��Ȃ��ꍇ15�b
        {
            countdown2 = 15; //defaultTime
            countdownReset2 = 15;
            print("0start");
        }
        PausePanel.SetActive(false);
        Qn = Random.Range(0, 15);  //�����_���Ȗ��ԍ�
        Shutsudai();
        TimeStop2 = true;

        //Debug.Log($"Count = {Count} Qcount = {Qcount}");
        audioSource = GetComponent<AudioSource>();
        NowVolume = SettingScript.VolumeValue;
        audioSource.volume = NowVolume;
        Debug.Log("���ʃ��x���� "+NowVolume);

    }

    // Update is called once per frame
    void Update()
    {
        Text Qnum = GameObject.Find("Number").GetComponent<Text>(); //  ������ڂ�
        Qnum.text = Qcount.ToString() + "/10���";
        Text Qtext = GameObject.Find("Sentense").GetComponent<Text>();  //��蕶
        Qtext.text = Q2.NameQuizList[Qn].NameQuestion;

        Text answer1 = GameObject.Find("AT1").GetComponent<Text>(); //����{�^��
        answer1.text = Q2.NameQuizList[bn[0]].JewelNameAnswer;
        Text answer2 = GameObject.Find("AT2").GetComponent<Text>(); //�E��{�^��
        answer2.text = Q2.NameQuizList[bn[1]].JewelNameAnswer;
        Text answer3 = GameObject.Find("AT3").GetComponent<Text>(); //�����{�^��
        answer3.text = Q2.NameQuizList[bn[2]].JewelNameAnswer;
        Text answer4 = GameObject.Find("AT4").GetComponent<Text>(); //�E���{�^��
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

    void Hantei(bool TimeOver)�@//�𓚂̐��ۂ𔻒肷��
    {
        Debug.Log(judge);
        if (TimeOver || Qn != bn[judge])//�o��ԍ��ƁAbn���X�g�̒��g���Ⴄ�̂ł����
        {
            gameover++;
            Zanki();
            fuseikai2.transform.position = new Vector3(0, 0, 0);
            tsugi2.transform.position = new Vector3(0, 0, 0);
            audioSource.PlayOneShot(sound.Incorrect);
            int indexNum = bn.IndexOf(Qn); //�����̓��X�g�̂ǂ���
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
        else //�o��ԍ��ƁAbn���X�g�̒��g�������ł����
        {
            Score++;
            int indexNum = bn.IndexOf(Qn);
            GameObject.Find(indexNum.ToString()).GetComponent<Image>().color = new Color(0.5f, 0.8f, 1);
            seikai2.transform.position = new Vector3(0, 0, 0);
            tsugi2.transform.position = new Vector3(0, 0, 0);
            audioSource.PlayOneShot(sound.Correct);
            if (Qcount == 10) //�𓚂��I�����^�C�~���O���A���̖��֔�ԑO
            {
                tsugi2.transform.position = new Vector3(0, 10000, 0);
                C2.transform.position = new Vector3(0, 0, 0);�@//����ɃN���A��ʂւƔ��
            }

        }

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
            Qn = Random.Range(0, 15);  //num���X�g�ɓ����Ă���Qn�ȊO�̐������擾�B���ꂪ���̖��ƂȂ�
        }
        num.Add(Qn); //�o�肵�����̔ԍ����o��ς݃��X�g��
        bn.Add(Qn);�@//���������Ēu��
        for (int n = 0; n < 3; n++)
        {
            int tempBn = 0; //���̕ϐ������A���̕ϐ��Ŏ擾�ł����l��bn���X�g�ɂ��邩�ǂ������肵�AQn�Ƃ̏d���������
            do { tempBn = Random.Range(0, 15); }
            while (bn.Contains(tempBn));

            bn.Add(tempBn); //bn�̒��ɓ����̔ԍ��ƊԈႢ�̔ԍ��R������

        }
        bn = bn.OrderBy(b => System.Guid.NewGuid()).ToList(); //���X�g�̒��̏��Ԃ����ւ��č�蒼���Ă���

    }

    public void NextQuestion() //���̖��ֈڂ钼�O�ŁA�𓚑O�̏󋵂ɖ߂�
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

    void Zanki() //�ԈႦ���񐔂������n�[�g������
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

    public static int GetScore2()//�N���A��ʂł̃X�R�A�\��
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
