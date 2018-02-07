using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTweenObject : MonoBehaviour {

    public TextMeshProUGUI txtScore;
    [SerializeField]
    private RectTransform rectTran;

    private int _num;

    //  Numがカスタムプロパティ
    public int Num
    {
        set
        {
            _num = value;
            // Numに値が代入されると画面上の文字が更新される
            txtScore.text = _num.ToString();
        }
        get
        {
            return _num;
        }
    }

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
    }

    public void ChangeScore()
    {
        DOTween.To(() => Num, (n) => Num = n, Num, 1f).SetEase(Ease.Linear);
    }
}
