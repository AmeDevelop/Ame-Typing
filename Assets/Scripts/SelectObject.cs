using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class SelectObject : MonoBehaviour
{
    public Text stringTitle;
    public Text stringArtist;
    public Text stringLevel;
    public Text stringTotal;
    public Text stringNo;
    public Text stringUrl;
    public RawImage Sumnail;
    private AutoCompleteComboBox cmbList;

    MusicListModel list;

    // Use this for initialization
    void Start()
    {
        cmbList = GetComponent<AutoCompleteComboBox>();

        // 音楽リスト取得
        StringBuilder path = new StringBuilder(Application.dataPath);
        path.Append("/Lyrics/music_list.xml");

        // 音楽リストXMLファイルを読み込み
        XmlAccess xml = new XmlAccess(typeof(MusicListModel));
        list = (MusicListModel)xml.xmlRead(path.ToString());

        // TODO: コンボボックスを動的に設定したいのだが…
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// コンボボックスで曲が選択されたのち楽曲情報を設定する
    /// </summary>
    /// <param name="curStr"></param>
    public void ShowMusicInfo(string curStr)
    {
        if (curStr.Length < 4) return;
        string id = curStr.Substring(1, 3);
        Music selected = list.music.Find(x => x.id == id);
        if (selected == null) return;
        stringNo.text = id;
        stringTitle.text = selected.title;
        stringArtist.text = selected.artist;
        stringLevel.text = selected.level.ToString();
        stringTotal.text = selected.time;
        stringUrl.text = selected.url;

        StringBuilder path = new StringBuilder("sumnail/");
        path.Append(id);
        Texture txture = Resources.Load<Texture>(path.ToString());
        if (txture == null)
        {
            path.Replace(id, "000");
            txture = Resources.Load<Texture>(path.ToString());
        }
        Sumnail.texture = txture;
    }
}

