using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.IO;

public class SettingScript : MonoBehaviour
{
    public Sounds sound;

    public GameObject settingPanel;

    public InputField inputfield;
    public Text text;
    public GameObject Dialog;
    public GameObject ErrorBlowing;
    public static float TimeLimit;
    public Slider VolumeSlider;

    AudioSource audiosource;
    public static float VolumeValue = 0.5f; //デフォルト
    float VolumeMute;

    public static int ChangeQuiz;


    public int m_int;




    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        inputfield.GetComponent<InputField>(); //inputfield = inputfield = GetComponent<InputField>();
        text.GetComponent<Text>();
        settingPanel.SetActive(false);
        audiosource = GetComponent<AudioSource>();
        VolumeSlider.GetComponent<Slider>();
        VolumeSlider.value = VolumeValue;


        StreamReader reader;

        reader = new StreamReader(Application.dataPath + "/../savedataall"+".json");
        //loaddata = reader.ReadToEnd();
        Director.SavedataALL savedataALL = JsonUtility.FromJson<Director.SavedataALL>(reader.ReadToEnd());
        //Debug.Log(reader.ReadToEnd());
        reader.Close();
        //Debug.Log(reader.ToString());
        foreach(Director.Savedata s in savedataALL.savedataList)
        {
            Debug.Log($"changequizID = {s.changequizID} questionID = {s.QuestionID} SeikaiSu = {s.SeikaiSu} totalcount = {s.totalcount}");
        }
        //savedata = JsonUtility.FromJson<Savedata>(loaddata);


    }
    public void TimeSetting() //OKボタン
    {
        audiosource.PlayOneShot(sound.PushButton);
        if (float.TryParse(text.text, out float result))
        {
            TimeLimit = result;
            TimeSettingDialog();          
        }
        else
        {
            StartCoroutine("ErrorDialog");
            Debug.Log("変換できませんでした");
        }

        Debug.Log(TimeLimit);
    }
    void TimeSettingDialog()
    {
        
        Text dialogtext = GameObject.Find("TimeDialog").GetComponent<Text>();
        if(TimeLimit>30)
        {
            TimeLimit = 30;
            dialogtext.text = "The TimeLimit was set at " + TimeLimit + "sec";
        }
        else if(TimeLimit<0)
        {
            TimeLimit = 15;
            dialogtext.text = "The TimeLimit was set at " + TimeLimit + "sec";
        }
        else
        {
            dialogtext.text = "The TimeLimit was set at " + TimeLimit + "sec";
        }
        StartCoroutine("ShowDialog");
    }

    IEnumerator ShowDialog()
    {
        //Dialog.transform.localPosition = new Vector3(0,200,0);
        Dialog.transform.DOLocalMove(new Vector3(0, -85, 0), 1f).SetRelative(true);
        //Dialog.GetComponent<RectTransform>().DOMoveY(-200, 1f).SetRelative(true);
        yield return new WaitForSeconds(2);
        Dialog.transform.DOLocalMove(new Vector3(0, 85, 0), 1f).SetRelative(true);

    }
    IEnumerator ErrorDialog()
    {
        ErrorBlowing.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        yield return new WaitForSeconds(2);
        ErrorBlowing.transform.DOScale(new Vector3(0, 0, 0), 0.1f);

    }

    public static float GetTimeLimitSetting()
    {
        return TimeLimit;
    }
    void AlphaON()
    {
        settingPanel.GetComponent<CanvasGroup>().alpha = 1;
    }
    void AlphaOFF()
    {
        settingPanel.GetComponent<CanvasGroup>().alpha = 0;
    }
    public void OpenSetting()
    {
        audiosource.PlayOneShot(SEscripts.GetPushButton());
        settingPanel.SetActive(true);
        AlphaON();
    }
    public void CloseSetting()
    {
        audiosource.PlayOneShot(SEscripts.GetPushButton());
        VolumeValue = VolumeSlider.value;
        settingPanel.SetActive(false);
    }
    public void SliderValueChange()
    {
        Debug.Log("現在値：" + VolumeSlider.value);
    }
    public void MuteButton()  //09/29 作成中
    {
        audiosource.PlayOneShot(sound.PushButton);
        VolumeValue = 0f;
        VolumeSlider.value = 0f;
    }
    public static float GetVolumeValue()
    {
        return VolumeValue;
    }
    public void GoGame()
    {
        ChangeQuiz = 0;
        GetChangeQuiz();
        SceneManager.LoadScene("QuizHontai");
    }
    public void GoGame2()
    {
        ChangeQuiz = 1;
        GetChangeQuiz();
        SceneManager.LoadScene("QuizHontai");
    }
    public static int GetChangeQuiz()
    {
        return ChangeQuiz;
    }

}