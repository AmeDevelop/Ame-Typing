using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SETypeObject : MonoBehaviour {

    public AudioSource audioSource;
    private AudioClip audioClip_OK;
    private AudioClip audioClip_NG;
    private AudioClip audioClip_COMB_PF;
    private AudioClip audioClip_COMB_GR;
    private AudioClip audioClip_COMB_GD;
    private AudioClip audioClip_COMB_OK;
    private AudioClip audioClip_COMB_BD;
    private AudioClip audioClip_COMB_NG;

    public const int AUDIO_START = 1;
    public const int AUDIO_CLEAR = 2;
    public const int AUDIO_OVER = 3;

    private AudioClip audioClip_VOICE;
    private AudioClip audioClip_SOUND;


    public GaugeObject gaugeObj;
    public ScoreObject scoreObj;
    public JudgeObject judgeObj;

    public const int PLUMI_PF = 3;
    public const int PLUMI_GR = 2;
    public const int PLUMI_GD = 1;
    public const int PLUMI_OK = 0;
    public const int PLUMI_BD = -2;
    public const int PLUMI_NG = -3;

    public static int cntPf;
    public static int cntGr;
    public static int cntGd;
    public static int cntOk;
    public static int cntBd;
    public static int cntNg;


    // Use this for initialization
    void Start () {
        audioClip_OK = (AudioClip)Resources.Load("se/se_tp_ok_02");
        audioClip_NG = (AudioClip)Resources.Load("se/se_tp_ng");
        audioClip_COMB_PF = (AudioClip)Resources.Load("se/se_cmb_pf");
        audioClip_COMB_GR = (AudioClip)Resources.Load("se/se_cmb_gr");
        audioClip_COMB_GD = (AudioClip)Resources.Load("se/se_cmb_gd");
        audioClip_COMB_OK = (AudioClip)Resources.Load("se/se_cmb_ok");
        audioClip_COMB_BD = (AudioClip)Resources.Load("se/se_cmb_bd");
        audioClip_COMB_NG = (AudioClip)Resources.Load("se/se_cmb_ng");
    }

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// カウント数のリセット
    /// </summary>
    public void initCount()
    {
        cntPf = 0;
        cntGr = 0;
        cntGd = 0;
        cntOk = 0;
        cntBd = 0;
        cntNg = 0;
    }

    /// <summary>
    /// OKタイピング音
    /// </summary>
    public void OKtype()
    {
        audioSource.PlayOneShot(audioClip_OK);
    }

    /// <summary>
    /// NGタイピング音
    /// </summary>
    public void NGtype()
    {
        audioSource.PlayOneShot(audioClip_NG);

    }

    /// <summary>
    /// コンボ音
    /// <paramref name="judgeNG"/>
    /// <paramref name="ngCnt"/>
    /// <paramref name="lineNum"/>
    /// </summary>
    public void Judge(bool judgeNG, int ngCnt, int lineNum)
    {
        if (judgeNG)
        {
            audioSource.PlayOneShot(audioClip_COMB_NG);
            gaugeObj.PlusMinus(ngCnt * PLUMI_NG);
            scoreObj.UpdateScore(MusicObject.JUDGE_NG);
            cntNg += ngCnt;
            return;
        }

        switch (MusicObject.judgeInterval.Count)
        {
            case MusicObject.JUDGE_PF:
                audioSource.PlayOneShot(audioClip_COMB_PF);
                judgeObj.ShowJudge(lineNum, MusicObject.JUDGE_PF);
                gaugeObj.PlusMinus(PLUMI_PF);
                scoreObj.UpdateScore(MusicObject.JUDGE_PF);
                cntPf++;
                break;
            case MusicObject.JUDGE_GR:
                audioSource.PlayOneShot(audioClip_COMB_GR);
                judgeObj.ShowJudge(lineNum, MusicObject.JUDGE_GR);
                gaugeObj.PlusMinus(PLUMI_GR);
                scoreObj.UpdateScore(MusicObject.JUDGE_GR);
                cntGr++;
                break;
            case MusicObject.JUDGE_GD:
                audioSource.PlayOneShot(audioClip_COMB_GD);
                judgeObj.ShowJudge(lineNum, MusicObject.JUDGE_GD);
                gaugeObj.PlusMinus(PLUMI_GD);
                scoreObj.UpdateScore(MusicObject.JUDGE_GD);
                cntGd++;
                break;
            case MusicObject.JUDGE_OK:
                audioSource.PlayOneShot(audioClip_COMB_OK);
                judgeObj.ShowJudge(lineNum, MusicObject.JUDGE_OK);
                scoreObj.UpdateScore(MusicObject.JUDGE_OK);
                cntOk++;
                break;
            case MusicObject.JUDGE_BD:
                audioSource.PlayOneShot(audioClip_COMB_BD);
                gaugeObj.PlusMinus(PLUMI_BD);
                scoreObj.UpdateScore(MusicObject.JUDGE_BD);
                cntBd++;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ゲームスタートエンド時の音
    /// </summary>
    /// <param name="type"></param>
    public void GameStartEnd(int type)
    {
        switch (type)
        {
            case AUDIO_START:
                audioSource.PlayOneShot((AudioClip)Resources.Load("se/se_game_start_voice"));
                break;
            case AUDIO_CLEAR:
                audioSource.PlayOneShot((AudioClip)Resources.Load("se/se_game_clear"));
                audioSource.PlayOneShot((AudioClip)Resources.Load("se/se_game_clear_voice"));
                break;
            case AUDIO_OVER:
                audioSource.PlayOneShot((AudioClip)Resources.Load("se/se_game_over"));
                audioSource.PlayOneShot((AudioClip)Resources.Load("se/se_game_over_voice"));
                break;
            default:
                break;
        }
    }
}
