using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Lyrics : MonoBehaviour {

    public Toggle isTestPlay;

    private LyricsModel lyrics;
    private LyricsModel editlyrics;
    private string musicNum;

    // Use this for initialization
    void Start () {
        musicNum = "";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 歌詞データのロード
    /// <param name="num"></param>
    /// <return></return>
    /// </summary>
    public void LoadLyrics(string num)
    {
        musicNum = num;

        // リストで指定された楽曲Noを取得
        StringBuilder path = new StringBuilder(Application.dataPath);
        path.Append("/Lyrics/");
        path.Append(musicNum);
        if (isTestPlay.isOn)
        {
            path.Append("_edit");
        }
        path.Append(".xml");

        // 楽曲Noの歌詞XMLファイルを読み込み
        // TODO: サーバー側にするとしたらパス指定はやりたくないかも…リソースでやりたい
        XmlAccess xml = new XmlAccess(typeof(LyricsModel));
        lyrics = (LyricsModel)xml.xmlRead(path.ToString());
        // 編集用のものにもコピーしておく
        editlyrics = (LyricsModel)xml.xmlRead(path.ToString());
        Debug.Log("XMLパースOK！");
    }

    /// <summary>
    /// 歌詞データのアンロード
    /// <param name=""></param>
    /// <return></return>
    /// </summary>
    public void UnloadLyrics()
    {
        lyrics = null;
        editlyrics = null;
        musicNum = "";
    }

    /// <summary>
    /// スタート時間の取得
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public string GetStartTime(int page)
    {
        if (page > lyrics.pages.Count)
        {
            return "0";
        }
        return lyrics.pages[page].startTime;
    }

    /// <summary>
    /// スタート時間の取得
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public string GetInterval(int page)
    {
        if (page > lyrics.pages.Count)
        {
            return "0";
        }
        return lyrics.pages[page].interval;
    }

    /// <summary>
    /// MAXページ数の取得
    /// </summary>
    /// <param name=""></param>
    /// <returns>maxpage</returns>
    public int GetMaxPage()
    {
        return lyrics.pages.Count;
    }

    /// <summary>
    /// 歌詞データの取得
    /// </summary>
    /// <returns></returns>
    public string GetLines(int page, int line)
    {
        if (page > lyrics.pages.Count)
        {
            return "終了だよ！";
        }
        if (line > lyrics.pages[page].lines.Count)
        {
            return "";
        }

        return lyrics.pages[page].lines[line].lyric;
    }

    /// <summary>
    /// タイミングの編集
    /// </summary>
    /// <param name="page"></param>
    /// <param name="startTime"></param>
    public void editTime(int page, float startTime)
    {
        editlyrics.pages[page].startTime = startTime.ToString();
        if (page != 0)
        {
            editlyrics.pages[page].interval = (startTime - float.Parse(editlyrics.pages[page - 1].startTime)).ToString();
        }
        else
        {
            editlyrics.pages[page].interval = startTime.ToString();
        }
    }

    /// <summary>
    /// 編集データの書き込み
    /// </summary>
    public void editLyricsFile()
    {
        StringBuilder path = new StringBuilder(Application.dataPath);
        path.Append("/Lyrics/");
        path.Append(musicNum);
        path.Append("_edit.xml");

        XmlAccess xml = new XmlAccess(typeof(LyricsModel));
        xml.xmlWrite(path.ToString(), editlyrics, false);

    }
}
