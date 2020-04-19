using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelButton {
    public int level;
    public Button levelButton;
    public GameObject trophy;
    public Text highScoreText;
}
public class LevelSelect : MonoBehaviour
{

    [SerializeField]
    private LevelButton[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        Debug.Log("Level progression: " + Settings.LevelProgression);

        foreach(LevelButton button in levelButtons)
        {
            if (Settings.LevelProgression >= button.level)
            {
                button.levelButton.interactable = true;
                int highscore = 0;
                switch (button.level)
                {
                    case 1:
                        highscore = Settings.HighscoreLvl1;
                        break;
                    case 2:
                        highscore = Settings.HighscoreLvl2;
                        break;
                    case 3:
                        highscore = Settings.HighscoreLvl3;
                        break;
                    case 4:
                        highscore = Settings.HighscoreLvl4;
                        break;
                }

                if (highscore != 0)
                {
                    button.highScoreText.text = highscore + "";
                    button.trophy.SetActive(true);
                } else
                {
                    button.highScoreText.text = "";
                    button.trophy.SetActive(false);
                }

            } else
            {
                button.levelButton.interactable = false;
                button.highScoreText.text = "";
                button.trophy.SetActive(false);

            }
        }
    }
}
