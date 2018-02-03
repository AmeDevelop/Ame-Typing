using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicObject : MonoBehaviour {

    public AudioSource audioSource;
    private AudioClip audioClip;

    private bool started;

    // Use this for initialization
    void Start () {
        started = false;
    }

    // Update is called once per frame
    void Update () {
        if (!started) return;

	}

    /// <summary>
    /// 曲スタート
    /// </summary>
    public void StartMusic (string num)
    {
        audioClip = (AudioClip)Resources.Load(num);
        audioSource.PlayOneShot(audioClip);
        started = true;
    }

    /// <summary>
    /// 曲キャンセル
    /// </summary>
    public void CancelMusic ()
    {
        audioSource.Stop();
        // TODO: Disposeの仕方がわからない…
        audioClip = null;
        started = false;
    }

}
