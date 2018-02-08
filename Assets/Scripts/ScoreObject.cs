using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreObject : MonoBehaviour {

    private const int SCORE_RATIO_PF = 8;
    private const int SCORE_RATIO_GR = 5;
    private const int SCORE_RATIO_GD = 3;
    private const int SCORE_RATIO_OK = 1;
    private const int START_BONUS_CHAIN = 4;
    private const int MAX_BONUS_CHAIN = 63;

    private int _baseScore;
    private Dictionary<int, int> _scoreBase;

    private int _score;
    private int _chain;
 
    public ScoreTweenObject sctwObj;
    //public TextMeshProUGUI txtGauge;


    // Use this for initialization
    void Start () {
        _scoreBase = new Dictionary<int, int>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// スタート時のスコア設定
    /// </summary>
    public void Init()
    {
        // TODO: 曲別判定スコアを設けるとしたらここでデータから引っ張ってきて設定
        _baseScore = 13;
        _scoreBase.Add(MusicObject.JUDGE_OK, _baseScore);
        _scoreBase.Add(MusicObject.JUDGE_GD, _baseScore * SCORE_RATIO_GD);
        _scoreBase.Add(MusicObject.JUDGE_GR, _baseScore * SCORE_RATIO_GR);
        _scoreBase.Add(MusicObject.JUDGE_PF, _baseScore * SCORE_RATIO_PF);

        _score = 0;
        _chain = 0;
        sctwObj.Num = _score;
    }

    /// <summary>
    /// スコアのクリア
    /// </summary>
    public void Clear()
    {
        _scoreBase.Clear();
        _score = 0;
        _chain = 0;
        sctwObj.Num = _score;
    }

    /// <summary>
    /// スコアの更新
    /// </summary>
    public void UpdateScore(int judge)
    {
        //Debug.Log(judge);

        // ゲージがなくなっていたら、それ以上カウントしない
        if (GaugeObject.curGaugeCnt == 0) return;

        // BADもしくはNGであれば連鎖数をストップする
        if (judge <= MusicObject.JUDGE_BD)
        {
            _chain = 0;
            return;
        }

        // 連鎖数をカウントアップ
        _chain++;

        // ボーナスを設定
        float bonus = 1.0f;
        // TODO: 複数人プレイで実装必要
        //float bonus = _chain - (START_BONUS_CHAIN - 1);
        //if (bonus <= 0)
        //{
        //    bonus = 1.0f;
        //}
        //else if (bonus > MAX_BONUS_CHAIN - (START_BONUS_CHAIN - 1))
        //{
        //    bonus = 6.0f;
        //}
        //else
        //{
        //    bonus = 1.0f + bonus * 0.1f;
        //}

        // ジャッジによる基本スコアにボーナスポイントをかけて加算点を算出
        int scrScore = _score;
        _score += (int) Math.Truncate(_scoreBase[judge] * bonus);

        //Debug.Log("[judge] "+ judge + " [chain] " + _chain + " [bonus] " + bonus + " [src score] " + scrScore + " [cur score]" + _score);

        // スコア更新
        sctwObj.Num = _score;
        sctwObj.ChangeScore();
    }

}
