using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MusicObject : MonoBehaviour {

    private AudioSource audioSource;
   // private AudioClip audioClip;

    public TypingObject typeObj;
    public sldTimelim slider;
    public Toggle isEdit;

    private GameObject _gameObj;
    private int _pageCnt;
    private int _editPageCnt;
    //private float playTime;
    private float _startSec;
    private int _maxPage;
    private bool _isSwitching;
    private bool _isEditing;
    //private bool _isPlaying;
    public static List<float> judgeInterval { get; private set; }

    public const int JUDGE_PF = 4;
    public const int JUDGE_GR = 3;
    public const int JUDGE_GD = 2;
    public const int JUDGE_OK = 1;
    public const int JUDGE_BD = 0;
    public const int JUDGE_NG = -1;

    // Use this for initialization
    void Start () {
        //playTime = 0f;
        _pageCnt = 0;
        _editPageCnt = 0;
        _startSec = 0f;
        _maxPage = 0;
        _isSwitching = false;
        _isEditing = false;
        audioSource = GetComponent<AudioSource>();
        judgeInterval =  new List<float>();
    }

    // Update is called once per frame
    void Update () {

        if (!audioSource.isPlaying) return;

        // 編集モードの場合は時間の計測
        StartCoroutine(EditTiming(_editPageCnt, audioSource.time));

        // 開始時間が来たらページ遷移をする
        if (audioSource.time >= _startSec)
        {
            if (_pageCnt < _maxPage)
            {
                // ページ遷移
                StartCoroutine(SwitchPage(_pageCnt, _startSec));
                _pageCnt++;
                if (_pageCnt < _maxPage)
                {
                    _startSec = float.Parse(typeObj.GetStartTime(_pageCnt));
                }
            }
            else
            {
                // MAXページ数を過ぎたらキャンセルさせる
                btnPlay.isClear = (GaugeObject.curGaugeCnt == 0) ? false : true;
                _gameObj = GameObject.Find("btn_play");
                Toggle btn = _gameObj.GetComponent<Toggle>();
                btn.isOn = false;
            }
        }

        // 判定基準の更新
        StartCoroutine(UpdateJudge(audioSource.time));
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
        _pageCnt = 0;
        _editPageCnt = 0;
        _startSec = float.Parse(typeObj.GetStartTime(_pageCnt));
        _maxPage = typeObj.GetMaxPage();
        _isSwitching = false;
        _isEditing = false;
        judgeInterval.Clear();

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
        _pageCnt = 0;
        _editPageCnt = 0;
        _startSec = 0f;
        judgeInterval.Clear();

        StopCoroutine(EditTiming(0, 0));
        StopCoroutine(SwitchPage(0, 0f));
        StopCoroutine(UpdateJudge(0f));

        // 編集モードの場合はXMLデータ書き込み
        if (isEdit.isOn)
        {
            typeObj.editLyricsFile();
        }

        _isSwitching = false;
        _isEditing = false;
        typeObj.InitText();
        //typeObj.CancelTyping();
        slider.InitVal();
        slider.loop = false;
    }

    /// <summary>
    /// 曲キャンセル時の表示初期化
    /// </summary>
    public void PostProcess()
    {
        typeObj.CancelTyping();
    }


    /// <summary>
    /// ページ遷移
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private IEnumerator SwitchPage(int i, float startSec)
    {
        if (_isSwitching) yield break;
        _isSwitching = true;

        // 時間が来たらページ遷移
        // 1. テキストをアップデート
        typeObj.JudgeAllEnded();
        typeObj.SetText(i);

        // 2. 時間スライダーの表示を初期化
        slider.InitVal();
        if (i < _maxPage - 1)
        {
            // インターバルを取得
            float interval = float.Parse(typeObj.GetInterval(i + 1));
            slider.countTime = interval;
            slider.loop = true;

            // 各判定の切替タイミングを設定
            // PF:GR:GD:OK:BD=1:2:3:2:2で設定
            judgeInterval.Clear();
            float per = interval / 10;
            startSec += per * 1.0f;
            judgeInterval.Add(startSec);
            startSec += per * 2.0f;
            judgeInterval.Add(startSec);
            startSec += per * 3.0f;
            judgeInterval.Add(startSec);
            startSec += per * 2.0f;
            judgeInterval.Add(startSec);
        }

        _isSwitching = false;
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
        if (!isEdit.isOn || _isEditing) yield break;
        _isEditing = true;

        // スペースキーで編集
        if (Input.GetKeyDown("space"))
        {

            Debug.Log("[Edit mode] page:" + page + " start time:" + startTime);
            typeObj.editTime(page, startTime - 0.2f);
            _editPageCnt++;
        }

        _isEditing = false;
        yield break;
    }

    /// <summary>
    /// ジャッジの更新
    /// </summary>
    /// <param name="currentTime"></param>
    private IEnumerator UpdateJudge(float currentTime)
    {
        if (judgeInterval.Count == 0) yield break;

        if (currentTime >= judgeInterval[0])
        {
            // 時間が来たらスライダーの色を更新し、次のジャッジインターバルに更新する
            judgeInterval.RemoveAt(0);
            slider.ChangeColor(judgeInterval.Count);
        }

        yield break;
    }
}
