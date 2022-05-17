using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreChangedEvent : UnityEvent<int> { };
public class PlayerHealthChangedEvent : UnityEvent<int> { };
public class EnemyDestroyedEvent : UnityEvent { };
public class GameOverEvent : UnityEvent { };

public class GameEventsManager : MonoBehaviour
{
    #region Singleton
    public static GameEventsManager Instance;
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
    }
    #endregion

 
    public GameOverEvent OnGameOver = new GameOverEvent();
    public ScoreChangedEvent OnScoreChanged { get; private set; } = new ScoreChangedEvent();
    public PlayerHealthChangedEvent OnPlayerHealthChanged { get; private set; } = new PlayerHealthChangedEvent();
    public EnemyDestroyedEvent OnEnemyDestroyed { get; private set; } = new EnemyDestroyedEvent();


}
