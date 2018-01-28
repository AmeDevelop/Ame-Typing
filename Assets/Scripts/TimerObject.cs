using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerObject : MonoBehaviour {

    public TypingObject typeObj;
    public sldTimelim slider;
    private bool started;
    public InputField iptNum;

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
    /// 曲タイマースタート
    /// </summary>
    public void StartTimer()
    {
        typeObj.GetLirycs(iptNum.text);
        typeObj.InitText();
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
        for (int i = 0; i <= typeObj.GetMaxPage() - 1; ++i)
        {
            // インターバルを取得し、時間が来るまでWait
            yield return new WaitForSeconds(float.Parse(typeObj.GetInterval(i)));
            // 時間が来たらページ遷移
            // 1. テキストをアップデート
            typeObj.UpdateText(i);
            // 2. 時間スライダーの表示を初期化
            slider.InitVal();
            if (i < typeObj.GetMaxPage())
            {
                slider.countTime = float.Parse(typeObj.GetInterval(i + 1));
                slider.roop = true;
            }
        }
            
    }
}
