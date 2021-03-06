﻿using System;
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
    private int targetLine;

    // private List<string> keys = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "'", "\"", "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "`", "[", "a", "s", "d", "f", "g", "h", "j", "k", "l", "=", ";", "]", "z", "x", "c", "v", "b", "n", "m", ",", ".", "/", "\\"};
    private string[] keys = { "1","2","3","4","5","6","7","8","9","0","-","'","\"","q","w","e","r","t","y","u","i","o","p","`","[","a","s","d","f","g","h","j","k","l","=",";","]","z","x","c","v","b","n","m",",",".","/","\\", };
    public Lyrics lyrics;

    public SETypeObject setype;
    public JudgeObject judgeObj;

    public static int cntCorrect;
    public static int cntInCorrect;


    private void Awake()
    {
        ts1 = new TypingSystem();
        ts2 = new TypingSystem();
        ts3 = new TypingSystem();
        ts4 = new TypingSystem();
        targetLine = 0;
        //pageCnt = 0;
    }

    // Use this for initialization
    void Start()
    {
        cntCorrect = 0;
        cntInCorrect = 0;
    }

    // Update is called once per frame
    void Update () {
        if (btnPlay.play_started) Control();
	}


    /// <summary>
    /// 歌詞データ読み込み
    /// <paramref name="num"/>
    /// </summary>
    public void GetLirycs(string num)
    {
        lyrics.LoadLyrics(num);
    }

    /// <summary>
    /// 開始時間取得
    /// </summary>
    /// <param name="page"/>
    /// <returns>interval</returns>
    public string GetStartTime(int page)
    {
        return lyrics.GetStartTime(page);
    }

    /// <summary>
    /// インターバル時間取得
    /// </summary>
    /// <param name="page"/>
    /// <returns>interval</returns>
    public string GetInterval(int page)
    {
        return lyrics.GetInterval(page);
    }

    /// <summary>
    /// ページ数取得
    /// </summary>
    /// <returns></returns>
    public int GetMaxPage()
    {
        return lyrics.GetMaxPage();
    }

    /// <summary>
    /// タイミングの編集
    /// </summary>
    /// <param name="page"></param>
    /// <param name="startTime"></param>
    public void editTime(int page, float startTime)
    {
        lyrics.editTime(page, startTime);
    }

    /// <summary>
    /// 編集データの書込
    /// </summary>
    public void editLyricsFile()
    {
        lyrics.editLyricsFile();
    }


    /// <summary>
    /// タイピングキャンセル
    /// </summary>
    public void CancelTyping()
    {
        StopCoroutine("UpdateText");
        StopCoroutine(ControlJudge(false, 0 , 0));
        StopCoroutine(SetJudgePoint(null));
        stringTextMesh1.color = Color.white;
        stringTextMesh2.color = Color.white;
        stringTextMesh3.color = Color.white;
        stringTextMesh4.color = Color.white;
        alphabetTextMesh.color = Color.white;
        stringTextMesh1.text = "SELECT MUSIC & PRESS START!!";
        stringTextMesh2.text = "";
        stringTextMesh3.text = "";
        stringTextMesh4.text = "";
        alphabetTextMesh.text = "SELECT MUSIC & PRESS START !!";
        lyrics.UnloadLyrics();
        //pageCnt = 0;
        setype.initCount();
        cntCorrect = 0;
        cntInCorrect = 0;
    }

    /// <summary>
    /// タイピングの判定
    /// <param name=""></param>
    /// <return></return>
    /// </summary>
    void Control ()
    {
        if (targetLine == 0 || targetLine > 4)
            return;

        // どっちが速いかテスト
        #region GetKeyDown
        // シフトキー押下しているかどうか
        bool isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        foreach (string key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                //Debug.Log(key);
                switch (targetLine)
                {
                    case 1:
                        ControlUpdate(ts1, key, isShift, ts2);
                        break;
                    case 2:
                        ControlUpdate(ts2, key, isShift, ts3);
                        break;
                    case 3:
                        ControlUpdate(ts3, key, isShift, ts4);
                        break;
                    case 4:
                        ControlUpdate(ts4, key, isShift);
                        break;
                    default:
                        Debug.Log("ERROR: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
                        break;
                }
            }
        }
        #endregion
        #region anyKeyDown
        // JISで入力するとInput.Stringで取れない文字がある…
        //if (Input.anyKeyDown)
        //{
        //    // シフトキー押下しているかどうか
        //    bool isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        //    foreach (char c in Input.inputString)
        //    {
        //        string key = c.ToString();
        //        if (!keys.Contains(key)) continue;
        //        Debug.Log(key);
        //        switch (targetLine)
        //        {
        //            case 1:
        //                ControlUpdate(ts1, key, isShift, ts2);
        //                break;
        //            case 2:
        //                ControlUpdate(ts2, key, isShift, ts3);
        //                break;
        //            case 3:
        //                ControlUpdate(ts3, key, isShift, ts4);
        //                break;
        //            case 4:
        //                ControlUpdate(ts4, key, isShift);
        //                break;
        //            default:
        //                //Debug.Log("ERROR: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        //                break;
        //        }
        //    }
        //}
        #endregion
    }

    /// <summary>
    /// 音や画面のアップデート制御
    /// </summary>
    /// <param name="ts"></param>
    /// <param name="key"></param>
    /// <param name="isShift"></param>
    /// <param name="nextts"></param>
    void ControlUpdate(TypingSystem ts, string key, bool isShift, TypingSystem nextts = null)
    {
        bool comb = false;
        int curTargetLIne = targetLine;

        if (ts.InputKey(key, btnKanaRoma.isKana, isShift) == 1 )
        {
            setype.OKtype();
            cntCorrect++;
            // スペース判定
            if (ts.GetRestString().Length > 0 && ts.GetRestString()[0] == ' ')
            {
                ts.RemoveSpace(btnKanaRoma.isKana);
                comb = true;
            }
            StartCoroutine("UpdateText");
        }
        else
        {
            setype.NGtype();
            cntInCorrect++;
        }

        if (ts.isEnded())
        {
            if (nextts != null && nextts.GetInputString().Length == 0)
            {
                targetLine = 5;
            }
            else
            {
                ++targetLine;
            }
            comb = true;
        }

        if (comb)
        {
            // 判定サウンドを鳴らす
            StartCoroutine(ControlJudge(false, 0 , curTargetLIne));
        }

    }


    /// <summary>
    /// タイピング文字列の初期化
    /// </summary>
    public void InitText()
    {
        ts1.SetInputString("", btnKanaRoma.isKana);
        ts2.SetInputString("", btnKanaRoma.isKana);
        ts3.SetInputString("", btnKanaRoma.isKana);
        ts4.SetInputString("", btnKanaRoma.isKana);
        stringTextMesh1.text = "";
        stringTextMesh2.text = "";
        stringTextMesh3.text = "";
        stringTextMesh4.text = "";
        alphabetTextMesh.text = "";
        stringTextMesh1.color = Color.red;
        stringTextMesh2.color = Color.red;
        stringTextMesh3.color = Color.red;
        stringTextMesh4.color = Color.red;
        alphabetTextMesh.color = Color.red;
        targetLine = 0;
        setype.initCount();
        cntCorrect = 0;
        cntInCorrect = 0;
    }

    /// <summary>
    /// タイピング文字列の設定
    /// <paramref name="page"/>
    /// </summary>
    public void SetText(int page)
    {
        ts1.SetInputString(lyrics.GetLines(page, 0), btnKanaRoma.isKana);
        ts2.SetInputString(lyrics.GetLines(page, 1), btnKanaRoma.isKana);
        ts3.SetInputString(lyrics.GetLines(page, 2), btnKanaRoma.isKana);
        ts4.SetInputString(lyrics.GetLines(page, 3), btnKanaRoma.isKana);
        stringTextMesh1.text = ts1.GetRestString();
        stringTextMesh2.text = ts2.GetRestString();
        stringTextMesh3.text = ts3.GetRestString();
        stringTextMesh4.text = ts4.GetRestString();
        alphabetTextMesh.text = ts1.GetRestKey(btnKanaRoma.isKana);
        if (ts1.GetInputString().Length != 0)
        {
            targetLine = 1;
        } else
        {
            targetLine = 0;
        }
        string[] inputStr = { ts1.GetRestString(), ts2.GetRestString(), ts3.GetRestString(), ts4.GetRestString() };
        StartCoroutine(SetJudgePoint(inputStr));
    }

    /// <summary>
    /// タイピング文字列の更新
    /// </summary>
    private IEnumerator UpdateText()
    {
        stringTextMesh1.text = "<color=#666666>" + ts1.GetInputedString() + "</color>" + ts1.GetRestString();
        if (targetLine > 1)
        {
            stringTextMesh2.text = "<color=#666666>" + ts2.GetInputedString() + "</color>" + ts2.GetRestString();
            if (targetLine > 2)
            {
                stringTextMesh3.text = "<color=#666666>" + ts3.GetInputedString() + "</color>" + ts3.GetRestString();
                if (targetLine > 3)
                {
                    stringTextMesh4.text = "<color=#666666>" + ts4.GetInputedString() + "</color>" + ts4.GetRestString();
                    alphabetTextMesh.text = "<color=#666666>" + ts4.GetInputedKey(btnKanaRoma.isKana) + "</color>" + ts4.GetRestKey(btnKanaRoma.isKana);
                    if (targetLine > 4)
                    {
                        alphabetTextMesh.text = "";
                    }
                }
                else
                {
                    alphabetTextMesh.text = "<color=#666666>" + ts3.GetInputedKey(btnKanaRoma.isKana) + "</color>" + ts3.GetRestKey(btnKanaRoma.isKana);
                }
            }
            else
            {
                alphabetTextMesh.text = "<color=#666666>" + ts2.GetInputedKey(btnKanaRoma.isKana) + "</color>" + ts2.GetRestKey(btnKanaRoma.isKana);
            }
        }
        else
        {
            alphabetTextMesh.text = "<color=#666666>" + ts1.GetInputedKey(btnKanaRoma.isKana) + "</color>" + ts1.GetRestKey(btnKanaRoma.isKana);
        }
        yield break;
    }

    /// <summary>
    /// ページのタイピングがすべて終了したかどうかでサウンドを鳴らす
    /// </summary>
    /// <returns></returns>
    public void JudgeAllEnded()
    {
        if (0 < targetLine && targetLine < 5)
        {
            int ngCnt
                = ts4.GetRestString().Length - ts4.GetRestString().Replace(" ", "").Length
                + ts3.GetRestString().Length - ts3.GetRestString().Replace(" ", "").Length
                + ts2.GetRestString().Length - ts2.GetRestString().Replace(" ", "").Length
                + ts1.GetRestString().Length - ts1.GetRestString().Replace(" ", "").Length;
            if (!ts4.isEnded()) ngCnt++;
            if (!ts3.isEnded()) ngCnt++;
            if (!ts2.isEnded()) ngCnt++;
            if (!ts1.isEnded()) ngCnt++;
            StartCoroutine(ControlJudge(true, ngCnt, 0));
        }
    }

    /// <summary>
    /// ジャッジ音とゲージの判定
    /// </summary>
    /// <returns></returns>
    private IEnumerator ControlJudge(bool judgeNG, int ngCnt, int lineNum)
    {
        setype.Judge(judgeNG, ngCnt, lineNum);
        yield break;
    }

    /// <summary>
    /// ジャッジの表記ポイント設定
    /// </summary>
    /// <param name="inputstr"></param>
    /// <returns></returns>
    private IEnumerator SetJudgePoint(string[] inputstr)
    {
        judgeObj.SetJudgePoint(inputstr);
        yield break;
    }

}
