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

    [SerializeField]
    private GameObject newHighscoreText;
    [SerializeField]
    private GameObject levelUnlockedText;

    public IEnumerator ShowingResults(int babies, int timeScore, bool newHighscore, bool levelUnlocked)
    {
        levelUnlockedText.SetActive(levelUnlocked);

        yield return AnimateText(babyText, babies, " x" + babyMultiplier);
        AudioManager.instance?.PlaySound(AudioEffect.resultScreen);
        yield return AnimateText(timeText, timeScore);
        AudioManager.instance?.PlaySound(AudioEffect.resultScreen);
        yield return AnimateText(totalText, timeScore + babies * babyMultiplier);
        AudioManager.instance?.PlaySound(AudioEffect.resultScreen);
        newHighscoreText.SetActive(newHighscore);

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
