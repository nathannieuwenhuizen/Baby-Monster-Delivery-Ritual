using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{

    private Image img;
    private Color color;

    void Start()
    {
        img = GetComponent<Image>();
    }

    public IEnumerator FadeTo(float _startAlpha, float _endAlpha, float _duration)
    {
        StopAllCoroutines();


        color = img.color;


        float startAlpha = _startAlpha;
        float index = 0;

        while (index < _duration)
        {
            index += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            color.a = Mathf.Lerp(startAlpha, _endAlpha, index);
            img.color = color;
        }
        color.a = _endAlpha;
        img.color = color;
    }

}
