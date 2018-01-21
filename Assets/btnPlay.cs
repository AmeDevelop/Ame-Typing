using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnPlay : MonoBehaviour {

    public Text stringTextMesh;
    public Text alphabetTextMesh;
    private TypingSystem ts;
    private string[] keys = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", };
    public Lirycs lirycs;

    private Toggle btn_play;

    // Use this for initialization
    void Start () {
        ts = new TypingSystem();
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
                    if (ts.InputKey(key) == 1)
                    {
                        UpdateText();
                    }
                    break;
                }
            }
            if (ts.isEnded())
            {
                ts.SetInputString(lirycs.GetRandomWord());
                UpdateText();
            }
        }
    }


    // トグル時のイベント
    public void onValueChanged()
    {
        if (btn_play.isOn)
        {
            Debug.Log("Play started:");
            ts.SetInputString(lirycs.GetRandomWord());
            UpdateText();
        }
        else
        {
            Debug.Log("Play canceled:");
            stringTextMesh.text = "PRESS START !!";
            alphabetTextMesh.text = "PRESS START !!";
        }
    }


    // テキストの更新
    void UpdateText()
    {
        stringTextMesh.text = "<color=red>" + ts.GetInputedString() + "</color>" + ts.GetRestString();
        alphabetTextMesh.text = "<color=red>" + ts.GetInputedKey() + "</color>" + ts.GetRestKey();
    }

}
