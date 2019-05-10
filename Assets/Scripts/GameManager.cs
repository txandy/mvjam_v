using System;
using Player;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Events

    public static Action StartTutorialEvent = delegate { };
    public static Action SecondTutorialEvent = delegate { };

    #endregion

    public static GameManager Instance;

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
}