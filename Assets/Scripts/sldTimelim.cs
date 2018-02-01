using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sldTimelim : MonoBehaviour {

    Slider sldTime;
    public bool roop;
    public float countTime;
    public Image fill;

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

    public void ChangeColor(int judge)
    {
        switch (judge)
        {
            case 1:
                fill.color = new Color(0.0f, 1.0f, 253.0f / 255.0f);
                break;
            case 2:
                fill.color = new Color(0.0f, 1.0f, 0.0f);
                break;
            case 3:
                fill.color = new Color(1.0f, 1.0f, 0.0f);
                break;
            case 4:
                fill.color = new Color(1.0f, 135.0f / 255.0f, 0.0f);
                break;
            case 5:
                fill.color = new Color(1.0f, 0.0f, 0.0f);
                break;
        }
    }
}

public static class JudgeRatio
{
    public const float PF_RATIO = 0.1f;
    public const float GR_RATIO = 0.2f;
    public const float GD_RATIO = 0.2f;
    public const float OK_RATIO = 0.3f;
    public const float BD_RATIO = 0.2f;
}
