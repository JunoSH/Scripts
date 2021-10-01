using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Director : MonoBehaviour
{
    public JewelStatusData DD;
    public Questions Q;  //ScriptableObject�u����

    public GameObject seikai;�@//�@�Z�C���X�g
    public GameObject fuseikai; //�@�~�C���X�g
    public GameObject tsugi;   //�����E�s������A���̖��ɔ�ԃ{�^��
    public GameObject CheckAns;�@//�s�����̎��A�����͂ǂꂾ������������
    public GameObject GO; //�Q�[���I�[�o�[��ʂɔ�΂��{�^��
    public GameObject C; //�N���A��ʂɔ�΂��{�^��
    public Text timetext;  
    public float countdown; //��������
    public float countdownReset; //�������Ԃ����Z�b�g
    bool TimeStop;�@�@
    public Image TimeBar;
    
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
        countdown = SettingScript.GetTimeLimitSetting();
        countdownReset = SettingScript.GetTimeLimitSetting();
        if(countdown == 0) //setting�����ɃX�^�[�g����A�܂���setting���J���ĉ������͂���Ă��Ȃ��ꍇ15�b
        {
            countdown = 15; //defaultTime
            countdownReset = 15;
            print("0start");
        }
        PausePanel.SetActive(false);

        Qn = Random.Range(0,Q.Qsentenses.Count);  //�����_���Ȗ��ԍ�  09/28�@��̃V�[���Ŗ���ScriptableObject�������ւ���悤�ɂ���ύX��������B���ʂȃV�[�����Ԉ���
        Shutsudai();
        TimeStop = true;

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
        Qtext.text = Q.Qsentenses[Qn].QuestionSentenses;

        Text answer1 = GameObject.Find("AT1").GetComponent<Text>(); //����{�^��
        answer1.text = Q.Qsentenses[bn[0]].QuestionAnswer;
        Text answer2 = GameObject.Find("AT2").GetComponent<Text>(); //�E��{�^��
        answer2.text = Q.Qsentenses[bn[1]].QuestionAnswer; 
        Text answer3 = GameObject.Find("AT3").GetComponent<Text>(); //�����{�^��
        answer3.text = Q.Qsentenses[bn[2]].QuestionAnswer; 
        Text answer4 = GameObject.Find("AT4").GetComponent<Text>(); //�E���{�^��
        answer4.text = Q.Qsentenses[bn[3]].QuestionAnswer;

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
        Debug.Log(judge);
        if (TimeOver || Qn != bn[judge])//�o��ԍ��ƁAbn���X�g�̒��g���Ⴄ�̂ł����
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
            GameObject.Find(indexNum.ToString()).GetComponent<Image>().color = new Color(0.5f,0.8f,1);
            seikai.transform.position = new Vector3(0, 0, 0);
            tsugi.transform.position = new Vector3(0, 0, 0);
            audioSource.PlayOneShot(SEscripts.GetCorrect());
            if (Qcount == 10) //�𓚂��I�����^�C�~���O���A���̖��֔�ԑO
            {
                tsugi.transform.position = new Vector3(0, 10000, 0);
                C.transform.position = new Vector3(0, 0, 0);�@//����ɃN���A��ʂւƔ��
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
            Qn = Random.Range(0, Q.Qsentenses.Count);  //num���X�g�ɓ����Ă���Qn�ȊO�̐������擾�B���ꂪ���̖��ƂȂ�
        }
        num.Add(Qn); //�o�肵�����̔ԍ����o��ς݃��X�g��
        bn.Add(Qn);�@//���������Ēu��
        for (int n = 0; n < 3; n++)
        {
            int tempBn = 0; //���̕ϐ������A���̕ϐ��Ŏ擾�ł����l��bn���X�g�ɂ��邩�ǂ������肵�AQn�Ƃ̏d���������
            do { tempBn = Random.Range(0, Q.Qsentenses.Count); }
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
}
