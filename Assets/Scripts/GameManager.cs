using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum gameState
{
    setup,
    running,
    end,

}
public class GameManager : MonoBehaviour
{

    //score
    [Header("score info")]
    public int amountOfBabiesCollected = 0;
    public int timeScore = 0;
    public int totalScore = 0;

    //time info
    private float startTime = 0;
    private float duration = 1;
    
    //prefabs
    public GameObject babyPrefab;

    //ingame objects
    [Header("ingame objects")]
    public Pan wokPan;
    public List<Baby> aliveBabies;
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

    //backend
    private SceneLoader sceneLoader;
    private gameState state;


    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

        startTime = Time.time;

        sceneLoader = GetComponent<SceneLoader>();
        aliveBabies = new List<Baby>();

        //fade camera from black
        StartCoroutine( fadeImage.FadeTo(1, 0, 0.5f));

        SpawnFirstBaby();
        state = gameState.running;
    }

    public void SpawnFirstBaby()
    {
        Baby newBaby = PoolManager.instance.ReuseObject(babyPrefab,wokPan.transform.position + new Vector3(0,.5f,0), Quaternion.identity).GetComponent<Baby>();
        aliveBabies.Add(newBaby);
    }

    // Update is called once per frame
    void Update()
    {
        if (state != gameState.running) { return; }

        if (aliveBabies.Count <= 0)
        {
            state = gameState.end;
            scrollBg.scrollSpeed = 0;
            duration = Time.time - startTime;
            timeScore = Mathf.RoundToInt(100 / (1 + (duration / 10)));

            if (amountOfBabiesCollected == 0 )
            {
                StartCoroutine(Fail());
            } else
            {
                EndScreen();
            }

        }
    }

    public void OnApplicationPause(bool pause)
    {
        //Pause(true);
    }

    public void Pause(bool val)
    {
        Time.timeScale = val ? 0 : 1f;
        pauseButton.SetActive(!val);
        pauseScreen.SetActive(val);
    }

    IEnumerator Fail()
    {
        yield return StartCoroutine(fadeImage.FadeTo(0, 1, 0.5f));
        sceneLoader.ReloadScene();
    }

    public void EndScreen()
    {
        resultScreen.gameObject.SetActive(true);
        StartCoroutine(resultScreen.ShowingResults(amountOfBabiesCollected, timeScore));
    }
}
