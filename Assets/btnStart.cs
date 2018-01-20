using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnStart : MonoBehaviour {

    public Text stringTextMesh;
    public Text alphabetTextMesh;
    private TypingSystem ts;
    private string[] keys = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", };
    public Lirycs lirycs;

    // Use this for initialization
    void Start () {
		
	}

    public void onClick()
    {
        Debug.Log("Button Push !!");
        ts = new TypingSystem();
        ts.SetInputString(lirycs.GetRandomWord());
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (string key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                if (ts.InputKey(key) == 1)
                {
                    UpdateText();
                }
                break;
            }
        }

        if (ts.isEnded())
        {
            ts.SetInputString(lirycs.GetRandomWord());
            UpdateText();
        }
    }

    // Update is called once per frame

    void UpdateText()
    {
        stringTextMesh.text = "<color=red>" + ts.GetInputedString() + "</color>" + ts.GetRestString();
        alphabetTextMesh.text = "<color=red>" + ts.GetInputedKey() + "</color>" + ts.GetRestKey();
    }
}
