using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool IsMobile;
    public bool IsDoubleSpeed;
    public int WhichLevel;
    public float LevelTime = 60;
    public bool GameStarted;
    public float TheScore;
    public float GameTime;
    public Text TimeText;
    public Text GameOverTimeText;
    public Text ScoreText;
    public Text GameOverScoreText;
    public Player TheP;
    public GameObject GameOverHUD;
    public GameObject WinHUD;
    public GameObject LoseHUD;
    public GameObject StartHUD;
    public GameObject InGameHUD;
    public GameObject NoHighScoreObj;
    public GameObject HighScoreObj;
    public Text HighScoreText;
    public Text HighScore2Text;
    bool passed;
    public Text LevelText;
    public Text LevelTimeText;
    public GameObject AdToContinue;
    public GameObject[] PlayerUnlockedHolder;
    public Text[] MuteText;
    public GameObject PauseHolder;
    public GameObject CounterPauseText;
    float StartRealtimeCounter;
    float EndRealtimeCounter;
    bool WasPaused;
    public GameObject LockedObj;
    public GameObject TrialsLeft;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("TheLevel") == 0)
        {
            TrialsLeft.GetComponentInChildren<Text>().text = "UNLIMITED TRIALS";
        }
        else
        {
            TrialsLeft.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("Trials").ToString()+" TRIALS LEFT";
            if (PlayerPrefs.GetInt("Trials") <= 0)
            {
                StartOver();
            }
        }
        Time.timeScale = 1;
        Difficulty(PlayerPrefs.GetFloat("Difficulty"));
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            IsMobile = true;
        }
        else
        {
            IsMobile = false;
        }
        if (PlayerPrefs.GetInt("Begin") == 0)
        {
            PlayerPrefs.SetInt("Begin", 1);
            PlayerPrefs.SetInt("LevelTime", 20);
        }
        LevelTime = PlayerPrefs.GetInt("LevelTime");
        WhichLevel = PlayerPrefs.GetInt("TheLevel");
        LevelText.text ="LEVEL "+ (WhichLevel + 1).ToString();
        LevelTimeText.text = LevelTime.ToString()+"s";
        if (PlayerPrefs.GetInt("AdContinue") > 2)
        {
            if (!Application.isEditor)
            {
                AdToContinue.SetActive(true);
            }
        }
        else
        {
            PlayerPrefs.SetInt("AdContinue", PlayerPrefs.GetInt("AdContinue") + 1);
            AdToContinue.SetActive(false);
        }
        refreshMute();
    }
    public void StartOver()
    {
        PlayerPrefs.SetInt("TheLevel", 0);
        PlayerPrefs.SetInt("LevelTime", 20);
        Restart();
    }
    void refreshMute()
    {
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            for(int i = 0; i < MuteText.Length; i++)
            {
                MuteText[i].text = "Mute ON";

            }
            AudioListener.volume = 0;
        }
        else
        {
            for (int i = 0; i < MuteText.Length; i++)
            {
                MuteText[i].text = "Mute OFF";

            }
          AudioListener.volume = 1;
        }
    }
    public void ToggleMute()
    {
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            PlayerPrefs.SetInt("Mute", 0);
          
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 1);
        }
        refreshMute();
    }
    public void UnlockRandomPlayer()
    {
        OpenPlayerUnlock();
    }
    public void OpenPlayerUnlock()
    {
        for (int i = 0; i < PlayerUnlockedHolder.Length; i++)
        {
            PlayerUnlockedHolder[i].SetActive(true);
        }
    }
    public void ClosePlayerUnlock()
    {
        for(int i = 0; i < PlayerUnlockedHolder.Length; i++)
        {
            PlayerUnlockedHolder[i].SetActive(false);
        }
    }
    public void AdContinueWatched()
    {
        AdToContinue.SetActive(false);
        PlayerPrefs.SetInt("AdContinue", 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameStarted == true)
        {
            if (PauseHolder.activeSelf == true)
            {
                Time.timeScale = 0;
                EndRealtimeCounter = 3;
                WasPaused = true;
            }
            else
            {
                if (EndRealtimeCounter >0&&WasPaused)
                {
                    Time.timeScale = 0;
                    CounterPauseText.SetActive(true);
                    CounterPauseText.GetComponent<Text>().text = Mathf.RoundToInt(EndRealtimeCounter).ToString();


                }
                else
                {
                    CounterPauseText.SetActive(false);
                    Time.timeScale = 1;
                    WasPaused = false;

                }
                EndRealtimeCounter -=Time.unscaledDeltaTime;
            }
            StartHUD.SetActive(false);
            if (!TheP.IsDead&&GameTime<LevelTime)
            {
                GameTime += Time.deltaTime;
                InGameHUD.SetActive(true);
                GameOverHUD.SetActive(false);
                DisplayTime(TimeText);
                if (GameTime> LevelTime - 10)
                {
                    TimeText.color = Color.red;
                    IsDoubleSpeed = true;
                }
                else
                {
                    TimeText.color = Color.white;
                    IsDoubleSpeed = false;
                }
                DisplayScore(ScoreText);
            }
            else
            {
                GameOverAction();
                //DisplayTime(GameOverTimeText);
                DisplayScore(GameOverScoreText);
                InGameHUD.SetActive(false);
                GameOverHUD.SetActive(true);
                WinHUD.SetActive(!TheP.IsDead);
                LoseHUD.SetActive(TheP.IsDead);
            }
        }
        else
        {
             InGameHUD.SetActive(false);
            StartHUD.SetActive(true);
            GameOverHUD.SetActive(false);
        }
       
    }
    public void Revive()
    {
        TheP.IsDead = false;
        passed = false;
    }
    void GameOverAction()
    {
        HighScore2Text.text ="HighScore "+ PlayerPrefs.GetFloat("HighScore").ToString();
        if (passed == false)
        {
            if (PlayerPrefs.GetFloat("HighScore") < TheScore)
            {
                HighScoreObj.SetActive(true);
                NoHighScoreObj.SetActive(false);
                PlayerPrefs.SetFloat("HighScore", TheScore);
                HighScoreText.text =  TheScore.ToString();
            }
            else
            {
                NoHighScoreObj.SetActive(true);
                HighScoreObj.SetActive(false);
            }
            passed = true;
            if (!TheP.IsDead)
            {
                int levelTime = PlayerPrefs.GetInt("LevelTime") + 20;
                if (levelTime > 180)
                {
                    levelTime = 180;
                }
                PlayerPrefs.SetInt("LevelTime", levelTime);
                PlayerPrefs.SetInt("TheLevel", WhichLevel + 1);
                int TheLevel = PlayerPrefs.GetInt("TheLevel");
                if (TheLevel == 5 || TheLevel == 10 || TheLevel == 20)
                {
                    StartCoroutine(ShowUnlock(false));
                }
                else if (TheLevel == 25 || TheLevel == 30 )
                {
                    StartCoroutine(ShowUnlock(true));
                }
                PlayerPrefs.SetInt("DeadCounter", 0);
                PlayerPrefs.SetInt("Trials", 3);

            }
            else
            {
                PlayerPrefs.SetInt("Trials", PlayerPrefs.GetInt("Trials")-1);
                PlayerPrefs.SetInt("DeadCounter", PlayerPrefs.GetInt("DeadCounter") + 1);
            }
        }
    }
    public void Difficulty(float Amount)
    {
        PlayerPrefs.SetFloat("Difficulty", Amount);
    }
    IEnumerator ShowUnlock(bool IsZombie)
    {
        yield return new WaitForSeconds(1);
        TheP.Anim.GetComponent<Model>().UnlockRandPlayer(IsZombie);
    }
    public void AddScore(int HowMuch)
    {
        if (!TheP.IsDead&&GameTime<LevelTime)
        {
            TheScore += HowMuch;
        }
    }
    void DisplayTime(Text TargetText)
    {
        if (GameTime < 10)
        {
           TargetText.text = Mathf.RoundToInt(GameTime).ToString();

        }
        else
        {
           TargetText.text = Mathf.RoundToInt(GameTime).ToString();

        }
    }
    void DisplayScore(Text TargetText)
    {
        TargetText.text ="Score "+ TheScore.ToString();
       
    }
    public void NextLevel()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        GameStarted = true;
    }
}
