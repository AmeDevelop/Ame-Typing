using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MusicObject : MonoBehaviour {

    private AudioSource audioSource;
    private AudioClip audioClip;

    public TypingObject typeObj;
    public sldTimelim slider;
    public Toggle isEdit;

    private GameObject gameObj;
    private int pageCnt;
    private int editPageCnt;
    //private float playTime;
    private float startSec;
    private int maxPage;
    private bool isSwitching;
    private bool isEditing;
    private bool isPlaying;

    // Use this for initialization
    void Start () {
        //playTime = 0f;
        pageCnt = 0;
        editPageCnt = 0;
        startSec = 0f;
        maxPage = 0;
        isSwitching = false;
        isEditing = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {

        if (!audioSource.isPlaying) return;

        // 時間計測開始
        // playTime += Time.deltaTime;
        // Debug.Log(audioSource.time);

        // 編集モードの場合は時間の計測
        StartCoroutine(EditTiming(editPageCnt, audioSource.time));

        // 開始時間が来たらページ遷移をする
        //if (playTime >= startSec)
        if (audioSource.time >= startSec)
        {
            if (pageCnt < maxPage)
            {
                // ページ遷移
                StartCoroutine(SwitchPage(pageCnt));
                pageCnt++;
                if (pageCnt < maxPage)
                {
                    startSec = float.Parse(typeObj.GetStartTime(pageCnt));
                }
            }
            else
            {
                // MAXページ数を過ぎたらキャンセルさせる
                gameObj = GameObject.Find("btn_play");
                Toggle btn = gameObj.GetComponent<Toggle>();
                btn.isOn = false;
            }
        }
    }

    /// <summary>
    /// 歌詞の開始準備
    /// </summary>
    public void Prepare(string num)
    {
        typeObj.GetLirycs(num);
        typeObj.InitText();
    }

    /// <summary>
    /// 曲スタート
    /// </summary>
    public void StartMusic (string num)
    {
        // playTime = 0f;
        pageCnt = 0;
        editPageCnt = 0;
        startSec = float.Parse(typeObj.GetStartTime(pageCnt));
        maxPage = typeObj.GetMaxPage();
        isSwitching = false;
        isEditing = false;

        // 音楽読み込み
        StartCoroutine(StreamPlayAudioFile(num));
        //audioClip = (AudioClip)Resources.Load(num);
        //audioSource.PlayOneShot(audioClip);
    }

    /// <summary>
    /// 音楽ファイルを非同期的に読み込み
    /// </summary>
    /// <param name="fileName">ファイル名
    /// <returns></returns>
    IEnumerator StreamPlayAudioFile(string num)
    {
        //ソース指定し音楽流す
        StringBuilder path = new StringBuilder("file:///");
        path.Append(Application.dataPath);
        path.Append("/Music/");
        path.Append(num);
        path.Append(".ogg");


        //音楽ファイルロード
        using (WWW www = new WWW(path.ToString()))
        {
            //読み込み完了まで待機
            yield return www;
            audioSource.clip = www.GetAudioClip(true, true);
            audioSource.Play();
        }
    }

    /// <summary>
    /// 曲キャンセル
    /// </summary>
    public void CancelMusic ()
    {
        audioSource.Stop();
        //// TODO: Disposeの仕方がわからない…
        //audioClip = null;
        StopCoroutine(StreamPlayAudioFile(""));
        // playTime = 0f;
        pageCnt = 0;
        editPageCnt = 0;
        startSec = 0f;

        StopCoroutine(EditTiming(0, 0));
        StopCoroutine(SwitchPage(0));

        // 編集モードの場合はXMLデータ書き込み
        if (isEdit.isOn)
        {
            typeObj.editLyricsFile();
        }

        isSwitching = false;
        isEditing = false;
        typeObj.CancelTyping();
        slider.InitVal();
        slider.roop = false;
    }

    /// <summary>
    /// ページ遷移
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private IEnumerator SwitchPage(int i)
    {
        if (isSwitching) yield break;
        isSwitching = true;

        // 時間が来たらページ遷移
        // 1. テキストをアップデート
        typeObj.SetText(i);
        // 2. 時間スライダーの表示を初期化
        slider.InitVal();
        if (i < maxPage - 1)
        {
            slider.countTime = float.Parse(typeObj.GetInterval(i + 1));
            slider.roop = true;
        }

        isSwitching = false;
        yield break;
    }

    /// <summary>
    /// タイミングの編集
    /// </summary>
    /// <param name="page"></param>
    /// <param name="startTime"></param>
    /// <returns></returns>
    private IEnumerator EditTiming(int page, float startTime)
    {
        if (!isEdit.isOn || isEditing) yield break;
        isEditing = true;

        // スペースキーで編集
        if (Input.GetKeyDown("space"))
        {

            Debug.Log("[Edit mode] page:" + page + " start time:" + startTime);
            typeObj.editTime(page, startTime - 0.2f);
            editPageCnt++;
        }

        isEditing = false;
        yield break;
    }
}
