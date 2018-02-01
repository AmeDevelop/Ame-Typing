using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerObject : MonoBehaviour {

    public TypingObject typeObj;
    public sldTimelim slider;
    private bool started;
    public InputField iptNum;
    GameObject gameObj;

    // Use this for initialization
    void Start () {
        started = false;
    }

    // Update is called once per frame
    void Update () {
        if (started)
        {
            typeObj.Control();
        }

    }

    /// <summary>
    /// タイマー開始準備
    /// </summary>
    public void Prepare()
    {
        typeObj.GetLirycs(iptNum.text);
        typeObj.InitText();
    }

    /// <summary>
    /// 曲タイマースタート
    /// </summary>
    public void StartTimer()
    {
        StartCoroutine("Coroutine");
        started = true;
    }

    /// <summary>
    /// 曲タイマーキャンセル
    /// </summary>
    public void CancelTimer()
    {
        StopCoroutine("Coroutine");
        typeObj.CancelTyping();
        slider.InitVal();
        slider.roop = false;
        started = false;
    }

    /// <summary>
    /// タイマー毎の処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator Coroutine( )
    {
        for (int i = 0; i < typeObj.GetMaxPage(); ++i)
        {
            // インターバルを取得
            float interval = float.Parse(typeObj.GetInterval(i));
            yield return new WaitForSeconds(interval);

            //// Pefectのタイマー設定
            ////slider.ChangeColor(1);
            //yield return new WaitForSeconds(interval * JudgeRatio.PF_RATIO);

            //// Greatのタイマー設定
            ////slider.ChangeColor(2);
            //yield return new WaitForSeconds(interval * JudgeRatio.GR_RATIO);

            //// Goodのタイマー設定
            ////slider.ChangeColor(3);
            //yield return new WaitForSeconds(interval * JudgeRatio.GD_RATIO);

            //// OKのタイマー設定
            ////slider.ChangeColor(4);
            //yield return new WaitForSeconds(interval * JudgeRatio.OK_RATIO);

            //// Badのタイマー設定
            ////slider.ChangeColor(5);
            //yield return new WaitForSeconds(interval * JudgeRatio.BD_RATIO);

            // 時間が来たらページ遷移
            // 1. テキストをアップデート
            typeObj.UpdateText(i);
            // 2. 時間スライダーの表示を初期化
            slider.InitVal();
            if (i < typeObj.GetMaxPage() - 1)
            {
                slider.countTime = float.Parse(typeObj.GetInterval(i + 1));
                slider.roop = true;
            }
        }

        gameObj = GameObject.Find("btn_play");
        Toggle btnPlay = gameObj.GetComponent<Toggle>();
        btnPlay.isOn = false;
    }
}
