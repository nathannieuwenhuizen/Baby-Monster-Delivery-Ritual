using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{

    [SerializeField]
    private AnimationCurve numberCurve;

    [SerializeField]
    private Text babyText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text totalText;

    [SerializeField]
    private float animationDuration = .5f;

    private int babyMultiplier = 10;

    public IEnumerator ShowingResults(int babies, int timeScore)
    {
        yield return AnimateText(babyText, babies, " x" + babyMultiplier);
        yield return AnimateText(timeText, timeScore);
        yield return AnimateText(totalText, timeScore + babies * babyMultiplier);
    }

    public IEnumerator AnimateText(Text text, float val, string aditionalText = "")
    {
        float current = 0;
        float index = 0;
        while (index < 1)
        {
            index += Time.deltaTime / animationDuration;
            current = Mathf.Lerp(0, val, numberCurve.Evaluate(index));
            text.text = "" + Mathf.Round(current) + aditionalText;
            yield return new WaitForFixedUpdate();

        }
        current = val;
        text.text = "" + Mathf.Round(current) + aditionalText;
    }

}
