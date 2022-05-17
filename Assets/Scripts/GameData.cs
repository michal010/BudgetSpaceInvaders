using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    #region Singleton
    public static GameData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        ResetGameData();
    }

    private void ResetGameData()
    {
        _PlayerHP = _PlayerStartingHP;
        _EnemyMoveTime = _EnemyStaringMoveTime;
    }
    #endregion

    #region Global variables
    private int _PlayerStartingHP = 3;
    public int PlayerStartingHP
    {
        get { return _PlayerStartingHP; }
        private set
        {
            _PlayerStartingHP = value;
        }
    }

    private int _PlayerHP;
    public int PlayerHP {
        get { return _PlayerHP; }
        set {
            _PlayerHP = Mathf.Max(0,value);
            GameEventsManager.Instance.OnPlayerHealthChanged.Invoke(PlayerHP);
            if(_PlayerHP == 0)
            {
                GameEventsManager.Instance.OnGameOver.Invoke();
            }
        }
    }

    private float _EnemyStaringMoveTime = 5;
    public float EnemyStartingMovetime 
    {
        get { return _EnemyStaringMoveTime; }
        private set
        {
            _EnemyStaringMoveTime = value;
        }
    }

    private float _EnemyMoveTime;
    public float EnemyMoveTime { get { return _EnemyMoveTime; } set { _EnemyMoveTime = value; } }
    
    private float _EnemyShootTimeFactor = 0.65f;
    public float EnemyShootTimeFactor { get { return _EnemyShootTimeFactor; } private set { _EnemyShootTimeFactor = value; } }

    private int _CurrentScore;
    public int CurrentScore 
    {
        get { return _CurrentScore; }
        set {
            _CurrentScore = value;
            GameEventsManager.Instance.OnScoreChanged.Invoke(CurrentScore);
        } 
    }

    private float _OnEnemyDeathEnemyMoveTimeModifier = 0.065f;
    public float OnEnemyDeathEnemyMoveTimeModifier
    {
        get { return _OnEnemyDeathEnemyMoveTimeModifier; }
        private set { _OnEnemyDeathEnemyMoveTimeModifier = value; }
    }

    #endregion

}
