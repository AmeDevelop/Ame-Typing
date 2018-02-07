using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sldTimelim : MonoBehaviour {

    Slider sldTime;
    public bool loop;
    public float countTime;
    public Image fill;
    SliderColor colorList;

    // Use this for initialization
    void Start () {
        // スライダーを取得する
        sldTime = GetComponent<Slider>();
        loop = false;
        colorList = new SliderColor();
    }

    // Update is called once per frame
    void Update () {
        if (loop)
        {
            sldTime.value += sldTime.maxValue / countTime * Time.deltaTime;
        }
    }

    public void InitVal()
    {
        loop = false;
        sldTime.value = 0;
        ChangeColor(MusicObject.JUDGE_PF);
    }

    /// <summary>
    /// スライダーの色を変える
    /// </summary>
    /// <param name="judge"></param>
    public void ChangeColor(int judge)
    {
        switch (judge)
        {
            case MusicObject.JUDGE_PF:
                fill.color = colorList.colorPF;
                break;
            case MusicObject.JUDGE_GR:
            case MusicObject.JUDGE_GD:
            case MusicObject.JUDGE_OK:
                break;
            case MusicObject.JUDGE_BD:
                fill.color = colorList.colorBD;
                break;
        }
    }
}

public class SliderColor
{
    public Color colorPF;
    public Color colorGR;
    public Color colorGD;
    public Color colorOK;
    public Color colorBD;

    public SliderColor ()
    {
        colorPF = new Color(0.0f, 1.0f, 253.0f / 255.0f);
        colorGR = new Color(0.0f, 1.0f, 0.0f);
        colorGD = new Color(1.0f, 1.0f, 0.0f);
        colorOK = new Color(1.0f, 135.0f / 255.0f, 0.0f);
        colorBD = new Color(1.0f, 135.0f / 255.0f, 0.0f);
    }
}