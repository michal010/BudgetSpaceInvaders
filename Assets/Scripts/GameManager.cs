using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _EndScreen;
    [SerializeField] AnimatedText _EndScreenAnimText;
    [SerializeField] GameObject _MainMenuButton;

    [SerializeField] EventTrigger EnemyAtPlayerLevelTrigger;
    private void Awake()
    {
        Time.timeScale = 1f;
        GameEventsManager.Instance.OnEnemyDestroyed.AddListener(OnEnemyDestoryed);
        GameEventsManager.Instance.OnGameOver.AddListener(OnGameOver);
    }
    private void Start()
    {
        EnemyAtPlayerLevelTrigger.OnTriggerEnter.AddListener(OnEnemiesAtPlayerLevel);
    }

    private void OnEnemyDestoryed()
    {
        //calculate new speed for enemies
        GameData.Instance.EnemyMoveTime -= GameData.Instance.OnEnemyDeathEnemyMoveTimeModifier;
    }
    private void OnEnemiesAtPlayerLevel()
    {
        GameData.Instance.PlayerHP = 0;
    }

    private void OnGameOver()
    {
        Time.timeScale = 0f;
        StartCoroutine(GameOverCoroutine());
    }
    IEnumerator GameOverCoroutine()
    {
        _EndScreen.SetActive(true);
        _MainMenuButton.SetActive(false);
        _EndScreenAnimText.TextToAnimate = string.Format("Game over! You've managed to get: {0} points!", GameData.Instance.CurrentScore);
        yield return _EndScreenAnimText.Animate();
        _MainMenuButton.SetActive(true);
    }
}
