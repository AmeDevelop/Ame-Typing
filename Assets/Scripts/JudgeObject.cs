using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System.Text.RegularExpressions;
using DG.Tweening;

public class JudgeObject : MonoBehaviour {

    public TextMeshProUGUI txtJudge1;
    public TextMeshProUGUI txtJudge2;
    public TextMeshProUGUI txtJudge3;
    public TextMeshProUGUI txtJudge4;

    private List<List<int>> _judgePoint;

    private Encoding utf8Enc = Encoding.GetEncoding("UTF-8");
    private Regex re;
    //[SerializeField]
    //private RectTransform rectTran;
    //private Sequence sequence;

    // Use this for initialization
    void Start () {
        _judgePoint = new List<List<int>>() { new List<int>(), new List<int>(), new List<int>(), new List<int>() };
        //txtJudge1.margin = new Vector4(22, 0, 0, 0);
        re = new Regex(@"[^a-zA-Z0-9]"); //半角英数字識別用
    }

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// 行・スペースでの打ち切りポイントを設定する
    /// </summary>
    /// <param name="pagestr"></param>
    public void SetJudgePoint(string[] pagestr)
    {
        _judgePoint[0].Clear();
        _judgePoint[1].Clear();
        _judgePoint[2].Clear();
        _judgePoint[3].Clear();

        int i = 0;
        foreach (string inputstr in pagestr)
        {
            // 文字列を、スペースで分ける
            string[] eachstr = inputstr.Split(null);

            int inputtedLen = 0;
            foreach (string str in eachstr)
            {
                // 各文字列の全角文字数・半角文字数を取得
                string hankaku = re.Replace(str, "");
                int hankakuLen = hankaku.Length; //1は半角スペース分
                int zenkakuLen = str.Length - hankakuLen;
                // 全体の文字列長を計測: 半角文字 * 12 + 全角文字 23
                inputtedLen += (hankakuLen - 1) * 10 + zenkakuLen * 23;
                Debug.Log("(str: " + str + ")" + "inputtedLen:" + inputtedLen + " hankakuLen:" + hankakuLen + " zenkakuLen:" + zenkakuLen);

                _judgePoint[i].Add(inputtedLen);
                inputtedLen += 12 * 2;
                //Debug.Log("_judgePoint[" + i + "]: " + inputtedLen + " (str: " + str + ")");
            }
            i++;
        }
    }

    /// <summary>
    /// ジャッジを元に判定文字表示
    /// </summary>
    /// <param name="lineNum"></param>
    /// <param name="judgeType"></param>
    public void ShowJudge(int lineNum, int judgeType)
    {
        //Debug.Log("ShowJudge: " + lineNum + ", " + judgeType);
        //txtJudge1.margin = new Vector4(22, 0, 0, 0);
        int margin = _judgePoint[lineNum - 1][0];
        _judgePoint[lineNum - 1].RemoveAt(0);

        string text = "";
        switch (judgeType)
        {
            case MusicObject.JUDGE_PF:
                text = "PERFECT";
                margin -= (text.Length - 2) * 12;
                break;
            case MusicObject.JUDGE_GR:
                text = "GREAT";
                margin -= text.Length * 12;
                break;
            case MusicObject.JUDGE_GD:
                text = "GOOD";
                margin -= text.Length * 12;
                break;
            case MusicObject.JUDGE_OK:
                text = "OK";
                margin -= text.Length * 12;
                break;
            default:
                break;
        }
        Vector4 mar = new Vector4(margin, 0, 0, 0);
        JudgeTween(lineNum, text, mar);
    }

    /// <summary>
    /// TWEENしたいけど仮
    /// </summary>
    /// <param name="lineNum"></param>
    /// <param name="text"></param>
    private void JudgeTween(int lineNum, string text, Vector4 margin)
    {
        switch (lineNum)
        {
            case 1:
                GameObject.Find("JudgeObject1").GetComponent<JudgeTweenObject1>().JudgeTween(margin, text);
                break;
            case 2:
                GameObject.Find("JudgeObject2").GetComponent<JudgeTweenObject2>().JudgeTween(margin, text);
                break;
            case 3:
                GameObject.Find("JudgeObject3").GetComponent<JudgeTweenObject3>().JudgeTween(margin, text);
                break;
            case 4:
                GameObject.Find("JudgeObject4").GetComponent<JudgeTweenObject4>().JudgeTween(margin, text);
                break;
            default:
                break;
        }
        //StopCoroutine(tempAnime(0, null, margin));
        //StartCoroutine(tempAnime(lineNum, text, margin));
    }

    /// <summary>
    /// アニメーション仮
    /// </summary>
    /// <returns></returns>
    private IEnumerator tempAnime(int lineNum, string text, Vector4 margin)
    {
        //Debug.Log("tempAnime: " + lineNum + ", " + text);

        switch (lineNum)
        {
            case 1:
                txtJudge1.margin = margin;
                txtJudge1.text = text;
                txtJudge1.DOFade(0.0f, 0.5f);
                yield return new WaitForSeconds(0.5f);
                txtJudge1.text = "";
                break;
            case 2:
                txtJudge2.margin = margin;
                txtJudge2.text = text;
                yield return new WaitForSeconds(0.5f);
                txtJudge2.text = "";
                break;
            case 3:
                txtJudge3.margin = margin;
                txtJudge3.text = text;
                yield return new WaitForSeconds(0.5f);
                txtJudge3.text = "";
                break;
            case 4:
                txtJudge4.margin = margin;
                txtJudge4.text = text;
                yield return new WaitForSeconds(0.5f);
                txtJudge4.text = "";
                break;
            default:
                break;
        }
    }
}
