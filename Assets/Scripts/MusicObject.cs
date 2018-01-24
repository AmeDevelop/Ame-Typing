using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicObject : MonoBehaviour {

    public AudioSource audioSource;
    private AudioClip audioClip;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// 曲スタート
    /// </summary>
    public void StartMusic ()
    {
        audioClip = (AudioClip)Resources.Load("001");
        audioSource.PlayOneShot(audioClip);
    }

    /// <summary>
    /// 曲キャンセル
    /// </summary>
    public void CancelMusic ()
    {
        audioSource.Stop();
        // TODO: Disposeの仕方がわからない…
        audioClip = null;
    }

}
