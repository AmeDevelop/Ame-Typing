using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class GaugeObject : MonoBehaviour {

    public TextMeshProUGUI txtGauge;

    public static int curGaugeCnt { get; private set; }

    private const char GAUGE_CHAR = '|';

    private const int GAUGE_MAX_COUNT= 30;
    private const int GAUGE_RED_COUNT = 3;
    private const int GAUGE_ORANGE_COUNT = 3;
    private const int GAUGE_GREEN_COUNT = GAUGE_MAX_COUNT - GAUGE_RED_COUNT - GAUGE_ORANGE_COUNT;

    private const string GAUGE_RED_TAG_START = "<color=red>";
    private const string GAUGE_ORANGE_TAG_START = "<color=orange>";
    private const string GAUGE_GREEN_TAG_START = "<color=green>";
    private const string GAUGE_TAG_END = "</color>";

    // Use this for initialization
    void Start () {
        curGaugeCnt = 0;
    }

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// スタート時のゲージ設定
    /// </summary>
    public void Init()
    {
        StringBuilder gaugeStr = new StringBuilder();
        gaugeStr.Append(GAUGE_RED_TAG_START);
        gaugeStr.Append(GAUGE_CHAR, GAUGE_RED_COUNT);
        gaugeStr.Append(GAUGE_TAG_END);
        gaugeStr.Append(GAUGE_ORANGE_TAG_START);
        gaugeStr.Append(GAUGE_CHAR, GAUGE_ORANGE_COUNT);
        gaugeStr.Append(GAUGE_TAG_END);
        gaugeStr.Append(GAUGE_GREEN_TAG_START);
        gaugeStr.Append(GAUGE_CHAR, GAUGE_ORANGE_COUNT * 2);
        gaugeStr.Append(GAUGE_TAG_END);
        txtGauge.text = gaugeStr.ToString();
        curGaugeCnt = GAUGE_RED_COUNT + GAUGE_ORANGE_COUNT + GAUGE_ORANGE_COUNT * 2;
    }

    /// <summary>
    /// ゲージのクリア
    /// </summary>
    public void Clear()
    {
        txtGauge.text = "";
        curGaugeCnt = 0;
    }

    /// <summary>
    /// ゲージのプラマイ処理
    /// </summary>
    public void PlusMinus(int dif)
    {
        curGaugeCnt = txtGauge.text.Length - txtGauge.text.Replace(GAUGE_CHAR.ToString(), "").Length;

        // 既に0になっていたらそれ以上処理を行わない
        if (curGaugeCnt == 0) return;

        int newGaugeCnt = curGaugeCnt + dif;
        if (newGaugeCnt < 0)
        {
            // 0になったらテキストをクリアしてオシマイ
            Clear();
            return;
        } else if (newGaugeCnt > GAUGE_MAX_COUNT)
        {
            newGaugeCnt = GAUGE_MAX_COUNT;
        }

        // 結果プラマイゼロであれば処理を行わない
        if (curGaugeCnt == newGaugeCnt) return;

        // 差分が発生していたら処理を行う
        StringBuilder gaugeStr = new StringBuilder(GAUGE_RED_TAG_START);
        for (int i = 1; i <= newGaugeCnt; i++) {
            switch (i)
            {
                case GAUGE_RED_COUNT:
                case GAUGE_RED_COUNT + GAUGE_ORANGE_COUNT:
                    gaugeStr.Append(GAUGE_CHAR);
                    gaugeStr.Append(GAUGE_TAG_END);
                    break;
                case GAUGE_RED_COUNT + 1:
                    gaugeStr.Append(GAUGE_ORANGE_TAG_START);
                    gaugeStr.Append(GAUGE_CHAR);
                    break;
                case GAUGE_RED_COUNT + GAUGE_ORANGE_COUNT + 1:
                    gaugeStr.Append(GAUGE_GREEN_TAG_START);
                    gaugeStr.Append(GAUGE_CHAR);
                    break;
                default:
                    gaugeStr.Append(GAUGE_CHAR);
                    break;
            }
        }
        gaugeStr.Append(GAUGE_TAG_END);
        txtGauge.text = gaugeStr.ToString();
    }

}
