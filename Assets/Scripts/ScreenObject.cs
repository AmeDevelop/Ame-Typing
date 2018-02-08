using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenObject : MonoBehaviour {

    public Text txtTitle;
    public Text txtArtist;

    private GameObject _goStartTitle;
    private GameObject _goStartArtist;
    private GameObject _goStartEnd;

    // Use this for initialization
    void Start () {
        _goStartTitle = GameObject.Find("txt_startTitle");
        _goStartArtist = GameObject.Find("txt_startArtist");
        _goStartTitle.SetActive(false);
        _goStartEnd = GameObject.Find("txt_startend");
        _goStartEnd.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// スタート時の曲名・アーティスト名を表示
    /// </summary>
    public void showStartInfo()
    {
        _goStartTitle.SetActive(true);
        Text txtStartTitle = _goStartTitle.GetComponent<Text>();
        txtStartTitle.text = txtTitle.text;
        Text txtStartArtist = _goStartArtist.GetComponent<Text>();
        txtStartArtist.text = "Artist: " + txtArtist.text;
    }

    /// <summary>
    /// スタート時の曲名・アーティスト名を非表示
    /// </summary>
    public void hideStartInfo()
    {
        _goStartTitle.SetActive(false);
    }

    /// <summary>
    /// GAME START/GAME OVER/CLEARなどを表示
    /// <paramref name="info"/>
    /// </summary>
    public void showGameInfo(string info)
    {
        _goStartEnd.SetActive(true);
        TextMeshProUGUI txtStartTitle = _goStartEnd.GetComponent<TextMeshProUGUI>();
        txtStartTitle.text = info;
    }

    /// <summary>
    /// GAME START/GAME OVER/CLEARなどを非表示
    /// </summary>
    public void hideGameInfo()
    {
        _goStartEnd.SetActive(false);
    }

}
