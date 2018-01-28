using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sldTimelim : MonoBehaviour {

    Slider sldTime;
    public bool roop;
    public float countTime;

    // Use this for initialization
    void Start () {
        // スライダーを取得する
        sldTime = GetComponent<Slider>();
        roop = false;
    }

    // Update is called once per frame
    void Update () {
        if (roop)
        {
            sldTime.value += sldTime.maxValue / countTime * Time.deltaTime;
        }
    }

    public void InitVal()
    {
        roop = false;
        sldTime.value = 0;
    }
}
