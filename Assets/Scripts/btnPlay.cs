using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnPlay : MonoBehaviour {

    public TimerObject timeObj;
    public MusicObject musicObj;
    private Toggle btn_play;
    public InputField iptNum;

    // Use this for initialization
    void Start () {
        btn_play = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update () {

    }


    // トグル時のイベント
    public void onValueChanged()
    {
        if (btn_play.isOn)
        {
            Debug.Log("Play started:");
            timeObj.Prepare();
            timeObj.StartTimer();
            musicObj.StartMusic(iptNum.text);
        }
        else
        {
            Debug.Log("Play canceled:");
            timeObj.CancelTimer();
            musicObj.CancelMusic();
        }
    }
}
