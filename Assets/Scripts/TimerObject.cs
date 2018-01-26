using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerObject : MonoBehaviour {

    public TypingObject typeObj;
    private bool started;

    // Use this for initialization
    void Start () {
        typeObj.GetLirycs();
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
            string milisec = typeObj.GetInterval(i);
            yield return new WaitForSeconds(float.Parse(milisec));
            typeObj.UpdateText(i);
        }
            
    }
}
