using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lirycs : MonoBehaviour {

    public TextAsset resouce;
    private string[] words;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        LoadDictionary();
    }

    void LoadDictionary()
    {
        string trimedData = resouce.text.Replace("\r", "");
        char[] dem = { '\n' };
        words = trimedData.Split(dem);
    }

    public string GetRandomWord()
    {
        return words[Random.Range(0, words.Length)];
    }
}
