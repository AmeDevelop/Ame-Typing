using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Lyrics : MonoBehaviour {

    //public TextAsset resouce;
    private LyricsModel lyrics;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 歌詞データのロード
    /// <param name=""></param>
    /// <return></return>
    /// </summary>
    public void LoadLyrics()
    {
        // リストで指定された楽曲Noを取得
        StringBuilder path = new StringBuilder(Application.dataPath);
        path.Append("/Lyrics/");
        path.Append("001");
        path.Append(".xml");

        // 楽曲Noの歌詞XMLファイルを読み込み
        // TODO: サーバー側にするとしたらパス指定はやりたくないかも…リソースでやりたい
        XmlAccess xml = new XmlAccess(typeof(LyricsModel));
        lyrics = (LyricsModel)xml.xmlRead(path.ToString());
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
}
