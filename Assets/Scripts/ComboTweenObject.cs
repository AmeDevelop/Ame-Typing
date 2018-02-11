using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ComboTweenObject : MonoBehaviour {

    public TextMeshProUGUI txtCombo;

    private CanvasGroup canvasGroup;
    private Sequence sequence;

    // Use this for initialization
    void Start () {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ComboTween(int num)
    {
        txtCombo.text = num + "COMBO";
        sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1.0f, 0.2f));
        sequence.Append(canvasGroup.DOFade(0.0f, 1.5f));
        sequence.Play();

    }

}
