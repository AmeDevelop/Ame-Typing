﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnPlay : MonoBehaviour {

    public TypingObject typeObj;
    public MusicObject musicObj;
    private Toggle btn_play;

    // Use this for initialization
    void Start () {
        btn_play = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update () {
        if (btn_play.isOn)
        {
            typeObj.Control();
         }
    }


    // トグル時のイベント
    public void onValueChanged()
    {
        if (btn_play.isOn)
        {
            Debug.Log("Play started:");
            typeObj.StartTyping();
            musicObj.StartMusic();
        }
        else
        {
            Debug.Log("Play canceled:");
            typeObj.CancelTyping();
            musicObj.CancelMusic();
        }
    }
}
