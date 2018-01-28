using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingObject : MonoBehaviour {

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

    public SETypeObject setype;

    // Use this for initialization
    void Start () {

    }

    private void Awake()
    {
        ts1 = new TypingSystem();
        ts2 = new TypingSystem();
        ts3 = new TypingSystem();
        ts4 = new TypingSystem();
        //pageCnt = 0;
    }

    // Update is called once per frame
    void Update () {
		
	}


    /// <summary>
    /// 歌詞データ読み込み
    /// </summary>
    public void GetLirycs()
    {
        lyrics.LoadLyrics();
    }

    /// <summary>
    /// インターバル時間取得
    /// </summary>
    /// <param name="page"/>
    /// <returns>interval</returns>
    public string GetInterval(int page)
    {
        return lyrics.GetStartTime(page);
    }

    /// <summary>
    /// ページ数取得
    /// </summary>
    /// <returns></returns>
    public int GetMaxPage()
    {
        return lyrics.GetMaxPage();
    }

    ///// <summary>
    ///// タイピングスタート
    ///// </summary>
    //public void StartTyping ()
    //{
    //    InitText();
    //}

    /// <summary>
    /// タイピングキャンセル
    /// </summary>
    public void CancelTyping()
    {
        stringTextMesh1.text = "PRESS START !!";
        stringTextMesh2.text = "PRESS START !!";
        stringTextMesh3.text = "PRESS START !!";
        stringTextMesh4.text = "PRESS START !!";
        alphabetTextMesh.text = "PRESS START !!";
        //pageCnt = 0;
    }

    /// <summary>
    /// タイピングの判定
    /// <param name=""></param>
    /// <return></return>
    /// </summary>
    public void Control ()
    {
        if (ts1.isEnded() && ts2.isEnded() && ts3.isEnded() && ts4.isEnded())
        {
            return;
        }

        foreach (string key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                if (ts1.InputKey(key) == 1 || ts2.InputKey(key) == 1 || ts3.InputKey(key) == 1 || ts4.InputKey(key) == 1)
                {
                    setype.OKtype();
                    UpdateText();
                } else
                {
                    setype.NGtype();
                }
                break;
            }
        }
   }


    /// <summary>
    /// タイピング文字列の初期化
    /// </summary>
    public void InitText()
    {
        ts1.SetInputString("");
        ts2.SetInputString("");
        ts3.SetInputString("");
        ts4.SetInputString("");
        stringTextMesh1.text = "";
        stringTextMesh2.text = "";
        stringTextMesh3.text = "";
        stringTextMesh4.text = "";
        alphabetTextMesh.text = "";
    }

    /// <summary>
    /// タイピング文字列の設定
    /// <paramref name="page"/>
    /// </summary>
    public void UpdateText(int page)
    {
        ts1.SetInputString(lyrics.GetLines(page, 0));
        ts2.SetInputString(lyrics.GetLines(page, 1));
        ts3.SetInputString(lyrics.GetLines(page, 2));
        ts4.SetInputString(lyrics.GetLines(page, 3));
        stringTextMesh1.text = ts1.GetRestString();
        stringTextMesh2.text = ts2.GetRestString();
        stringTextMesh3.text = ts3.GetRestString();
        stringTextMesh4.text = ts4.GetRestString();
        alphabetTextMesh.text = ts1.GetRestKey();
    }

    /// <summary>
    /// タイピング文字列の更新
    /// </summary>
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
                    if (ts4.isEnded())
                    {
                        alphabetTextMesh.text = "";
                    }
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
        }
        else
        {
            alphabetTextMesh.text = "<color=red>" + ts1.GetInputedKey() + "</color>" + ts1.GetRestKey();
        }
    }
}
