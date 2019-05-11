using System;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    public static float TIME_TO_GET_ENERGY = 3.0f; // 
    public static int AMOUNT_ENERGY_TO_GET = 5; // 

    #region Events

    public static Action StartTutorialEvent = delegate { };
    public static Action SecondTutorialEvent = delegate { };

    #endregion

    public static GameManager Instance;
    public AudioSource musicSource;
    public bool isTutorialEnabled;

    private bool _isFirstGame = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Tutorial.Tutorial.StopTutorialEvent += StopTutorialEvent;
        PlayerController.PlayerCanMove += PlayerCanMove;

        if (_isFirstGame)
        {
            StartTutorial();
        }
    }

    private void OnDestroy()
    {
        Tutorial.Tutorial.StopTutorialEvent -= StopTutorialEvent;
        PlayerController.PlayerCanMove -= PlayerCanMove;
    }

    #region Tutorial

    private void StopTutorialEvent()
    {
        Time.timeScale = 1;

        isTutorialEnabled = false;
    }

    void StartTutorial()
    {
        isTutorialEnabled = true;
        Time.timeScale = 0;

        StartTutorialEvent();
    }

    private void PlayerCanMove()
    {
        if (_isFirstGame)
        {
            _isFirstGame = false;
            isTutorialEnabled = true;
            Time.timeScale = 0;
            SecondTutorialEvent();
        }
    }

    #endregion

    public void RestartGame()
    {
        AnalyticsEvent.Custom("restartGame", new Dictionary<string, object>
        {
            {"score", UI.Score.Instance.ScorePoints},
            {"time_elapsed", Time.timeSinceLevelLoad}
        });

        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}