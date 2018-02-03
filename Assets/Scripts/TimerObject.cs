using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerObject : MonoBehaviour {

    public TypingObject typeObj;
    public sldTimelim slider;
    private bool started;
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
    public void Prepare(string num)
    {
        typeObj.GetLirycs(num);
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
        StopCoroutine(SwitchPage(0));
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

            // ページ遷移
            StartCoroutine(SwitchPage(i));
        }

        gameObj = GameObject.Find("btn_play");
        Toggle btnPlay = gameObj.GetComponent<Toggle>();
        btnPlay.isOn = false;
    }

    /// <summary>
    /// ページ遷移
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private IEnumerator SwitchPage(int i)
    {
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
        yield break;
    }
}
