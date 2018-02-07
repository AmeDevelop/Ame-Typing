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

    public GaugeObject gaugeObj;
    public ScoreObject scoreObj;

    public const int PLUMI_PF = 3;
    public const int PLUMI_GR = 2;
    public const int PLUMI_GD = 1;
    public const int PLUMI_OK = 0;
    public const int PLUMI_BD = -2;
    public const int PLUMI_NG = -3;


    // Use this for initialization
    void Start () {
        audioClip_OK = (AudioClip)Resources.Load("se_tp_ok_02");
        audioClip_NG = (AudioClip)Resources.Load("se_tp_ng");
        audioClip_COMB_PF = (AudioClip)Resources.Load("se_cmb_pf");
        audioClip_COMB_GR = (AudioClip)Resources.Load("se_cmb_gr");
        audioClip_COMB_GD = (AudioClip)Resources.Load("se_cmb_gd");
        audioClip_COMB_OK = (AudioClip)Resources.Load("se_cmb_ok");
        audioClip_COMB_BD = (AudioClip)Resources.Load("se_cmb_bd");
        audioClip_COMB_NG = (AudioClip)Resources.Load("se_cmb_ng");
    }

    // Update is called once per frame
    void Update () {
		
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
    /// </summary>
    public void Judge(bool judgeNG, int ngCnt)
    {
        if (judgeNG)
        {
            audioSource.PlayOneShot(audioClip_COMB_NG);
            gaugeObj.PlusMinus(ngCnt * PLUMI_NG);
            scoreObj.UpdateScore(MusicObject.JUDGE_NG);
            return;
        }

        switch (MusicObject.judgeInterval.Count)
        {
            case MusicObject.JUDGE_PF:
                audioSource.PlayOneShot(audioClip_COMB_PF);
                gaugeObj.PlusMinus(PLUMI_PF);
                scoreObj.UpdateScore(MusicObject.JUDGE_PF);
                break;
            case MusicObject.JUDGE_GR:
                audioSource.PlayOneShot(audioClip_COMB_GR);
                gaugeObj.PlusMinus(PLUMI_GR);
                scoreObj.UpdateScore(MusicObject.JUDGE_GR);
                break;
            case MusicObject.JUDGE_GD:
                audioSource.PlayOneShot(audioClip_COMB_GD);
                gaugeObj.PlusMinus(PLUMI_GD);
                scoreObj.UpdateScore(MusicObject.JUDGE_GD);
                break;
            case MusicObject.JUDGE_OK:
                audioSource.PlayOneShot(audioClip_COMB_OK);
                scoreObj.UpdateScore(MusicObject.JUDGE_OK);
                break;
            case MusicObject.JUDGE_BD:
                audioSource.PlayOneShot(audioClip_COMB_BD);
                gaugeObj.PlusMinus(PLUMI_BD);
                scoreObj.UpdateScore(MusicObject.JUDGE_BD);
                break;
            default:
                break;
        }

    }
}
