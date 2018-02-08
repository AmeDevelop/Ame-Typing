using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnPlay : MonoBehaviour {

    public MusicObject musicObj;
    private Toggle btn_play;
    public Text id;
    public GaugeObject gaugeObj;
    public ScoreObject scoreObj;
    public ScreenObject screenObj;
    public SETypeObject seObj;

    public static bool play_started;
    public static bool isClear;

    // Use this for initialization
    void Start () {
        btn_play = GetComponent<Toggle>();
        play_started = false;
        isClear = false;
    }

    // Update is called once per frame
    void Update () {

    }


    // トグル時のイベント
    public void onValueChanged()
    {
        if (btn_play.isOn)
        {
            if (id.text == "000") return;
            Debug.Log("Play started:");
            StartCoroutine("PlayStart");
            //gaugeObj.Init();
            //scoreObj.Init();
            //musicObj.Prepare(id.text);
            //musicObj.StartMusic(id.text);
            ////timeObj.StartTimer();
            //play_started = true;
        }
        else
        {
            Debug.Log("Play canceled:");
            StopCoroutine("PlayStart");
            StartCoroutine("PlayEnd");
            //play_started = false;
            //isClear = false;
            ////timeObj.CancelTimer();
            //musicObj.CancelMusic();
            //gaugeObj.Clear();
            //scoreObj.Clear();
        }
    }


    /// <summary>
    /// ゲーム開始処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayStart()
    {
        seObj.GameStartEnd(SETypeObject.AUDIO_START);
        gaugeObj.Init();
        scoreObj.Init();
        musicObj.Prepare(id.text);

        screenObj.showStartInfo();
        yield return new WaitForSeconds(3.0f);
        screenObj.hideStartInfo();

        screenObj.showGameInfo("Game Start");
        yield return new WaitForSeconds(2.0f);
        screenObj.hideGameInfo();

        musicObj.StartMusic(id.text);
        play_started = true;
        isClear = false;

        yield break;
    }

    /// <summary>
    /// ゲーム終了処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayEnd()
    {
        play_started = false;
        musicObj.CancelMusic();

        string message = "";
        int setype = 0;
        if (isClear)
        {
            message = "Game Clear";
            setype = SETypeObject.AUDIO_CLEAR;

        } else
        {
            message = "Game Over";
            setype = SETypeObject.AUDIO_OVER;
        }
        seObj.GameStartEnd(setype);
        screenObj.showGameInfo(message);
        yield return new WaitForSeconds(5.0f);
        screenObj.hideGameInfo();

        isClear = false;
        gaugeObj.Clear();
        scoreObj.Clear();
        musicObj.PostProcess();
    }

}
