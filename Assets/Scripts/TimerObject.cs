using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerObject : MonoBehaviour {

    private IEnumerator coroutine;
    public TypingObject typeObj;

    // Use this for initialization
    void Start () {
        typeObj.GetLirycs();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 曲タイマースタート
    /// </summary>
    public void StartTimer()
    {
          
        string milisec = typeObj.GetInterval();
        coroutine = WaitForInterval(float.Parse(milisec));
        StartCoroutine(coroutine);
        typeObj.StartTyping();
    }

    /// <summary>
    /// 曲タイマーキャンセル
    /// </summary>
    public void CancelTimer()
    {
        StopCoroutine(coroutine);
    }

    private IEnumerator WaitForInterval(float sec)
    {
            yield return new WaitForSeconds(sec);
            
    }
}
