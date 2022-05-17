using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _playerHeatlhText;

    private void Awake()
    {
        GameEventsManager.Instance.OnScoreChanged.AddListener(OnScoreChanged);
        GameEventsManager.Instance.OnPlayerHealthChanged.AddListener(OnPlayerHealthChanged);
    }

    public void OnPlayerHealthChanged(int value)
    {
        int HealthDiffrence = GameData.Instance.PlayerStartingHP - value;
        _playerHeatlhText.text = string.Format("Lives: ");
        for (int i = 0; i < value; i++)
        {
            _playerHeatlhText.text += " <sprite=0> ";
        }
        for (int i = 0; i < HealthDiffrence; i++)
        {
            _playerHeatlhText.text += " <sprite=1> ";
        }
    }
    public void OnScoreChanged(int value)
    {
        _scoreText.text = string.Format("Score: {0}", value);
    }
}
