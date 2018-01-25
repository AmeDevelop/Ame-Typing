using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Lyrics : MonoBehaviour {

    //public TextAsset resouce;
    private LyricsModel lirycs;

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
        StringBuilder path = new StringBuilder("../../Lirycs/");
        path.Append("001");
        path.Append(".xml");

        // 楽曲Noの歌詞XMLファイルを読み込み
        // TODO: サーバー側にするとしたらパス指定はやりたくないかも…リソースでやりたい
        XmlAccess xml = new XmlAccess(typeof(LyricsModel));
        lirycs = (LyricsModel)xml.xmlRead("Assets/Lyrics/001.xml");
        Debug.Log("XMLパースOK！");
    }

    /// <summary>
    /// 歌詞データのアンロード
    /// <param name=""></param>
    /// <return></return>
    /// </summary>
    public void UnloadLyrics()
    {
        lirycs = null;
    }

    /// <summary>
    /// スタート時間の取得
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public string GetStartTime(int page)
    {
        if (page > lirycs.pages.Count)
        {
            return "0";
        }
        return lirycs.pages[page].startTIme;
    }


    /// <summary>
    /// 歌詞データの取得
    /// </summary>
    /// <returns></returns>
    public string GetLines(int page, int line)
    {
        if (page > lirycs.pages.Count)
        {
            return "終了だよ！";
        }
        if (line > lirycs.pages[page].lines.Count)
        {
            return "";
        }

        return lirycs.pages[page].lines[line].lyric;
    }
}
