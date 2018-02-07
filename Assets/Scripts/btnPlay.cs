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

    public static bool play_started;

    // Use this for initialization
    void Start () {
        btn_play = GetComponent<Toggle>();
        play_started = false;
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
            gaugeObj.Init();
            scoreObj.Init();
            musicObj.Prepare(id.text);
            musicObj.StartMusic(id.text);
            //timeObj.StartTimer();
            play_started = true;
        }
        else
        {
            Debug.Log("Play canceled:");
            play_started = false;
            //timeObj.CancelTimer();
            musicObj.CancelMusic();
            gaugeObj.Clear();
            scoreObj.Clear();
        }
    }
}
