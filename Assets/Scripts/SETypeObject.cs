using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SETypeObject : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip audioClip_OK;
    public AudioClip audioClip_NG;
    public AudioClip audioClip_COMB;



    // Use this for initialization
    void Start () {
        audioClip_OK = (AudioClip)Resources.Load("se_tp_ok_01");
        audioClip_NG = (AudioClip)Resources.Load("se_tp_ng");
        audioClip_COMB = (AudioClip)Resources.Load("se_tp_ok_01");
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
    /// </summary>
    public void Combo()
    {
        audioSource.PlayOneShot(audioClip_COMB);

    }
}
