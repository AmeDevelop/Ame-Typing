using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnUrlObject : MonoBehaviour {

    public Text stringUrl;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClick()
    {
        if (stringUrl.text == "000") return;
        Application.OpenURL(stringUrl.text);
    }
}
