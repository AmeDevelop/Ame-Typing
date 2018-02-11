﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class JudgeTweenObject4 : MonoBehaviour {

    public TextMeshProUGUI txtJudge4;

    private CanvasGroup canvasGroup;
    private Sequence sequence;

    // Use this for initialization
    void Start () {
        canvasGroup = this.GetComponent<CanvasGroup>();
   }

    // Update is called once per frame
    void Update() {

    }

    public void JudgeTween(Vector4 margin, string text)
    {
        txtJudge4.margin = margin;
        txtJudge4.text = text;
        sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1.0f, 0.2f));
        sequence.Append(canvasGroup.DOFade(0.0f, 1.0f));
        sequence.Play();
    }


}
