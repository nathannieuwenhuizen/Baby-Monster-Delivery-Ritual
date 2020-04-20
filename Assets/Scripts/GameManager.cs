using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum gameState
{
    setup,
    running,
    end,

}
public class GameManager : MonoBehaviour
{
    public int level = 1;

    //score
    [Header("score info")]
    public int amountOfBabiesCollected = 0;
    public int timeScore = 0;
    public int totalScore = 0;

    //time info
    private float duration = 0;
    
    //prefabs
    public GameObject babyPrefab;

    //ingame objects
    [Header("ingame objects")]
    public Pan wokPan;
    private List<Baby> aliveBabies;
    public ScrollBackGround scrollBg;

    //ui
    [Header("UI")]
    [SerializeField]
    private FadeImage fadeImage;
    [SerializeField]
    private ResultScreen resultScreen;
    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private Text babyCount;
    [SerializeField]
    private GameObject babyCountParent;

    //backend
    public gameState state = gameState.setup;


    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        aliveBabies = new List<Baby>();

    }

    void Start()
    {


        ////fade camera from black
        //StartCoroutine( fadeImage.FadeTo(1, 0, 0.5f));

        SpawnFirstBaby();
        state = gameState.running; 
    }

    public void SpawnFirstBaby()
    {
        Baby newBaby = PoolManager.instance.ReuseObject(babyPrefab,wokPan.transform.position + new Vector3(0,.5f,0), Quaternion.identity).GetComponent<Baby>();
        AliveBabies.Add(newBaby);
        UpdateBabyCount(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (state != gameState.running) { return; }


        duration += Time.deltaTime;

        if (aliveBabies.Count <= 0)
        {
            state = gameState.end;
            //scrollBg.scrollSpeed = 0;
            timeScore = Mathf.RoundToInt(500 / (1 + (duration / 10)));

            if (amountOfBabiesCollected == 0 )
            {
                StartCoroutine(Fail());
            } else
            {
                EndScreen();
            }

        }
    }

    public List<Baby> AliveBabies
    {
        get {
            return aliveBabies;
        }
        set {
            aliveBabies = value;
        }
    }

    public void UpdateBabyCount()
    {
        babyCount.text = "" + aliveBabies.Count;
    }

    public void OnApplicationPause(bool pause)
    {
        //Pause(true);
    }

    public void Pause(bool val)
    {
        AudioManager.instance?.PlaySound(AudioEffect.uiClick);
        Time.timeScale = val ? 0 : 1f;
        pauseButton.SetActive(!val);
        babyCountParent.SetActive(!val);
        pauseScreen.SetActive(val);
    }

    IEnumerator Fail()
    {
        AudioManager.instance?.PlaySound(AudioEffect.fail);

        yield return StartCoroutine(fadeImage.FadeTo(0, 1, 0.5f));
        GetComponent<SceneLoader>().ReloadScene();
    }

    public void EndScreen()
    {
        bool levelUnlocked = false;
        if (level >= Settings.LevelProgression && level != 4)
        {
            levelUnlocked = true;
            Settings.LevelProgression = level + 1;
            //new level unlocked
        }

        bool newHighscore = false;
        //check new highscore
        if (NewHighscore())
        {
            newHighscore = true;
            //new highscore
        }

        pauseButton.SetActive(false);
        babyCountParent.gameObject.SetActive(false);
        resultScreen.gameObject.SetActive(true);
        StartCoroutine(resultScreen.ShowingResults(amountOfBabiesCollected, timeScore, newHighscore, levelUnlocked));
    }

    public bool NewHighscore()
    {
        int score = amountOfBabiesCollected * 10 + timeScore;

        switch (level)
        {
            case 1:
                if (score > Settings.HighscoreLvl1)
                {
                    Settings.HighscoreLvl1 = score;
                    return true;
                }
                break;
            case 2:
                if (score > Settings.HighscoreLvl2)
                {
                    Settings.HighscoreLvl2 = score;
                    return true;
                }
                break;
            case 3:
                if (score > Settings.HighscoreLvl3)
                {
                    Settings.HighscoreLvl3 = score;
                    return true;
                }
                break;
            case 4:
                if (score > Settings.HighscoreLvl4)
                {
                    Settings.HighscoreLvl4 = score;
                    return true;
                }
                break;
        }
        return false;
    }
}
