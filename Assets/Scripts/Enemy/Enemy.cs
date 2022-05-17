using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { TopEnemy, MiddleEnemy, BottomEnemy, Ufo}

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour, IDamagable<int>, IBoundable
{
    private int _maxHP;
    private int _currentHP;
    private int _ScoreValue;
    private EnemyType _type;
    public Vector2 Position() => transform.position;
    //IoC
    EnemiesController _controller;
    private BoundingBox _boundingBox;
    public EnemyMovement _enemyMovement { get; private set; }

    public static Enemy Create(Vector2 spawnPoint, EnemyType type,int _ScoreValue, EnemiesController controller, BoundingBox boundingBox = null,int hp = 1)
    {
        GameObject enemyPrefab = null;
        switch (type)
        {
            case EnemyType.TopEnemy:
                enemyPrefab = GameAssets.Instance.PFb_TopEnemy;
                break;
            case EnemyType.MiddleEnemy:
                enemyPrefab = GameAssets.Instance.PFb_MiddleEnemy;
                break;
            case EnemyType.BottomEnemy:
                enemyPrefab = GameAssets.Instance.PFb_BottomEnemy;
                break;
            case EnemyType.Ufo:
                enemyPrefab = GameAssets.Instance.PFb_Ufo;
                break;
        }
        Transform enemyTransform = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity).transform;
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        enemy._maxHP = hp;
        enemy._ScoreValue = _ScoreValue;
        enemy._controller = controller;
        enemy._type = type;
        enemy._boundingBox = boundingBox;
        return enemy;
    }
    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }
    private void Start()
    {
        _currentHP = _maxHP;
    }
    public bool TakeDamage(int amount, GameObject source)
    {
        if (source.CompareTag(gameObject.tag))
            return false;
        _currentHP -= amount;
        if(_currentHP <= 0)
        {
            OnDeath();
        }
        return true;
    }
    private void OnDeath()
    {
        //Explosion effect, sound, add score.
        GameData.Instance.CurrentScore += _ScoreValue;
        _controller.RemoveEnemyFromList(this);
        GameEventsManager.Instance.OnEnemyDestroyed.Invoke();
        Destroy(gameObject);
    }

    public void OnBoundingBoxExit(BoundingBox bbox)
    {
        if (bbox != _boundingBox)
            return;
        if(_type == EnemyType.Ufo)
        {
            Destroy(gameObject);
        }
    }
}
