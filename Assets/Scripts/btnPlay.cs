using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnPlay : MonoBehaviour {

    public Text stringTextMesh1;
    public Text stringTextMesh2;
    public Text stringTextMesh3;
    public Text stringTextMesh4;
    public Text alphabetTextMesh;
    private TypingSystem ts1;
    private TypingSystem ts2;
    private TypingSystem ts3;
    private TypingSystem ts4;

    private string[] keys = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", };
    public Lyrics lyrics;
    private int pageCnt;

    private Toggle btn_play;

    // Use this for initialization
    void Start () {
        ts1 = new TypingSystem();
        ts2 = new TypingSystem();
        ts3 = new TypingSystem();
        ts4 = new TypingSystem();
        pageCnt = 0;
        btn_play = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update () {
        if (btn_play.isOn)
        {
            foreach (string key in keys)
            {
                if (Input.GetKeyDown(key))
                {
                    if (ts1.InputKey(key) == 1 || ts2.InputKey(key) == 1 || ts3.InputKey(key) == 1 || ts4.InputKey(key) == 1)
                    {
                        UpdateText();
                    }
                    break;
                }
            }
            if (ts1.isEnded() && ts2.isEnded() && ts3.isEnded() && ts4.isEnded())
            {
                pageCnt++;
                ts1.SetInputString(lyrics.GetLines(pageCnt, 0));
                ts2.SetInputString(lyrics.GetLines(pageCnt, 1));
                ts3.SetInputString(lyrics.GetLines(pageCnt, 2));
                ts4.SetInputString(lyrics.GetLines(pageCnt, 3));
                stringTextMesh1.text = ts1.GetRestString();
                stringTextMesh2.text = ts2.GetRestString();
                stringTextMesh3.text = ts3.GetRestString();
                stringTextMesh4.text = ts4.GetRestString();
                alphabetTextMesh.text = ts1.GetRestKey();
            }
        }
    }


    // トグル時のイベント
    public void onValueChanged()
    {
        if (btn_play.isOn)
        {
            Debug.Log("Play started:");
            lyrics.LoadLyrics();
            ts1.SetInputString(lyrics.GetLines(pageCnt, 0));
            ts2.SetInputString(lyrics.GetLines(pageCnt, 1));
            ts3.SetInputString(lyrics.GetLines(pageCnt, 2));
            ts4.SetInputString(lyrics.GetLines(pageCnt, 3));
            stringTextMesh1.text = ts1.GetRestString();
            stringTextMesh2.text = ts2.GetRestString();
            stringTextMesh3.text = ts3.GetRestString();
            stringTextMesh4.text = ts4.GetRestString();
            alphabetTextMesh.text = ts1.GetRestKey();
        }
        else
        {
            Debug.Log("Play canceled:");
            stringTextMesh1.text = "PRESS START !!";
            stringTextMesh2.text = "PRESS START !!";
            stringTextMesh3.text = "PRESS START !!";
            stringTextMesh4.text = "PRESS START !!";
            alphabetTextMesh.text = "PRESS START !!";
            pageCnt = 0;
        }
    }


    // テキストの更新
    void UpdateText()
    {
        stringTextMesh1.text = "<color=red>" + ts1.GetInputedString() + "</color>" + ts1.GetRestString();
        if (ts1.isEnded())
        {
            stringTextMesh2.text = "<color=red>" + ts2.GetInputedString() + "</color>" + ts2.GetRestString();
            if (ts2.isEnded())
            {
                stringTextMesh3.text = "<color=red>" + ts3.GetInputedString() + "</color>" + ts3.GetRestString();
                if (ts3.isEnded())
                {
                    stringTextMesh4.text = "<color=red>" + ts4.GetInputedString() + "</color>" + ts4.GetRestString();
                    alphabetTextMesh.text = "<color=red>" + ts4.GetInputedKey() + "</color>" + ts4.GetRestKey();
                }
                else
                {
                    alphabetTextMesh.text = "<color=red>" + ts3.GetInputedKey() + "</color>" + ts3.GetRestKey();
                }
            }
            else
            {
                alphabetTextMesh.text = "<color=red>" + ts2.GetInputedKey() + "</color>" + ts2.GetRestKey();
            }
        } else
        {
            alphabetTextMesh.text = "<color=red>" + ts1.GetInputedKey() + "</color>" + ts1.GetRestKey();
        }
    }

}
