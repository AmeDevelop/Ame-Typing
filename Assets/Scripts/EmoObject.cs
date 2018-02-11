using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EmoObject : MonoBehaviour
{

    private Texture tF00;
    private Texture tF01;
    private Texture tF02;
    private Texture tF03;
    private Texture tF04;
    private Texture tF05;
    private Texture tF06;
    private Texture tF07;
    private Texture tF08;
    private Texture tF09;
    private Texture tF10;
    private Texture tF11;
    private Texture tF12;

    private  RawImage _image;
    [SerializeField]
    private RectTransform rectTran;
    private Sequence sequence;

    private string[] keys = { "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "f10", "f11", "f12", };

    // Use this for initialization
    void Start()
    {
        _image = GetComponent<RawImage>();

        tF00 = Resources.Load<Texture>("emo/emo_f00");
        tF01 = Resources.Load<Texture>("emo/emo_f01");
        tF02 = Resources.Load<Texture>("emo/emo_f02");
        tF03 = Resources.Load<Texture>("emo/emo_f03");
        tF04 = Resources.Load<Texture>("emo/emo_f04");
        tF05 = Resources.Load<Texture>("emo/emo_f05");
        tF06 = Resources.Load<Texture>("emo/emo_f06");
        tF07 = Resources.Load<Texture>("emo/emo_f07");
        tF08 = Resources.Load<Texture>("emo/emo_f08");
        tF09 = Resources.Load<Texture>("emo/emo_f09");
        tF10 = Resources.Load<Texture>("emo/emo_f10");
        tF11 = Resources.Load<Texture>("emo/emo_f11");
        tF12 = Resources.Load<Texture>("emo/emo_f12");

    }

    // Update is called once per frame
    void Update()
    {

        foreach (string key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                switch (key)
                {
                    case "f1":
                        Emotion(tF01);
                        break;
                    case "f2":
                        Emotion(tF02);
                        break;
                    case "f3":
                        Emotion(tF03);
                        break;
                    case "f4":
                        Emotion(tF04);
                        break;
                    case "f5":
                        Emotion(tF05);
                        break;
                    case "f6":
                        Emotion(tF06);
                        break;
                    case "f7":
                        Emotion(tF07);
                        break;
                    case "f8":
                        Emotion(tF08);
                        break;
                    case "f9":
                        Emotion(tF09);
                        break;
                    case "f10":
                        Emotion(tF10);
                        break;
                    case "f11":
                        Emotion(tF11);
                        break;
                    case "f12":
                        Emotion(tF12);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void Emotion(Texture emo)
    {
        //StopCoroutine("tempAnime");
        _image.texture = emo;

        rectTran = this.gameObject.GetComponent<RectTransform>();
        sequence = DOTween.Sequence();
        sequence.Append(rectTran.DOScale(new Vector3(1.2f, 1.2f), 0.4f));
        sequence.Append(rectTran.DOScale(new Vector3(1.0f, 1.0f), 0.4f));
        sequence.Append(rectTran.DOScale(new Vector3(1.2f, 1.2f), 0.4f));
        sequence.Append(rectTran.DOScale(new Vector3(1.0f, 1.0f), 0.4f));
        sequence.Append(rectTran.DOScale(new Vector3(1.2f, 1.2f), 0.4f));
        sequence.Append(rectTran.DOScale(new Vector3(0.0f, 0.0f), 0.6f));
        sequence.Play();

        //StartCoroutine("tempAnime");
    }

    /// <summary>
    /// アニメーションがうまくいかないので仮
    /// </summary>
    /// <returns></returns>
    private IEnumerator tempAnime()
    {
        yield return new WaitForSeconds(2.0f);
        _image.texture = tF00;
    }
}
