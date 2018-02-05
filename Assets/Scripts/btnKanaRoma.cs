using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnKanaRoma : MonoBehaviour {

    public static bool isKana;

    private Toggle btn_kanaroma;
    private string[] kyes_qwe = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", };
    private string[] kyes_jis = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "!", "\"", "#", "$", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", ":", ";", "<", "=", ">", "?", "@", "[", "\\", "]", "^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", };

    // Use this for initialization
    void Start () {

        isKana = false;
        btn_kanaroma = GetComponent<Toggle>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    // トグル時のイベント
    public void onValueChanged()
    {
        if (btn_kanaroma.isOn)
        {
            isKana = true;
            Debug.Log("Changed to KANA:");
        }
        else
        {
            isKana = false;
            Debug.Log("Changed to ROMA:");
        }
    }
}
