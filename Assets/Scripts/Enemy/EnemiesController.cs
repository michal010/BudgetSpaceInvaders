using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesController : MonoBehaviour 
{
    List<Enemy> enemies;

    Vector2Int _enemyMoveDirection = Vector2Int.right;

    [SerializeField] Vector2Int _spawnAnchorPoint = new Vector2Int(-8, 5);
    [SerializeField] BoundingBox _EnemyBoundingBox;
    [SerializeField] BoundingBox _UfoBoundingBox;
    [SerializeField] float _enemyProjectileSpeed = 10f;
    [SerializeField] Transform _ufoSpawnPoint;
    [SerializeField] Transform _ufoMovePoint;
    [SerializeField] float _ufoSpawnTime;

    bool _shouldGoDown = false;

    int rows = 6;
    int cols = 11;

    private void Awake()
    {
        ResetLogic();
        GameEventsManager.Instance.OnEnemyDestroyed.AddListener(OnEnemyDestoyed);
        SpawnEnemyWave();
    }
    private void ResetLogic()
    {
        enemies = new List<Enemy>();
        _enemyMoveDirectionCurrent = _enemyMoveDirection = Vector2Int.right;
        GameData.Instance.EnemyMoveTime = GameData.Instance.EnemyStartingMovetime;
        _shouldGoDown = false;
    }

    private void SpawnEnemyWave()
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if(y < 2)
                    CreateEnemy(new Vector2(x+ _spawnAnchorPoint.x,-y+ _spawnAnchorPoint.y), EnemyType.TopEnemy, 20);
                else if (y < 4)
                    CreateEnemy(new Vector2(x + _spawnAnchorPoint.x, -y + _spawnAnchorPoint.y), EnemyType.MiddleEnemy, 15);
                else
                    CreateEnemy(new Vector2(x + _spawnAnchorPoint.x, -y + _spawnAnchorPoint.y), EnemyType.BottomEnemy, 10);
            }
        }
    }
    private Enemy CreateEnemy(Vector2 spawnPoint, EnemyType enemyType, int scoreValue)
    {
        Enemy e = Enemy.Create(new Vector2(spawnPoint.x, spawnPoint.y), enemyType, scoreValue, this, enemyType == EnemyType.Ufo ? _UfoBoundingBox : null);
        if (enemyType!= EnemyType.Ufo)
            enemies.Add(e);
        return e;
    }
    public void RemoveEnemyFromList(Enemy e)
    {
        if(enemies.Contains(e))
            enemies.Remove(e);
    }

    float _elapsedMoveTime = 0;
    float _elapsedShootTime = 0;
    float _elapsedUfoSpawnTime = 0;
    Vector2Int _enemyMoveDirectionCurrent;
    private void Update()
    {
        ProcessMovement();
        ProcessShooting();
        ProcessUfo();
    }
    private void ProcessUfo()
    {
        _elapsedUfoSpawnTime += Time.deltaTime;
        if(_elapsedUfoSpawnTime > _ufoSpawnTime)
        {
            _elapsedUfoSpawnTime = 0;
            Enemy ufo = CreateEnemy(_ufoSpawnPoint.position, EnemyType.Ufo, 500);
            ufo._enemyMovement.MoveTo(_ufoMovePoint.position, 5f);
        }
    }
    private void ProcessShooting()
    {
        _elapsedShootTime += Time.deltaTime;
        if (_elapsedShootTime >= GameData.Instance.EnemyMoveTime * GameData.Instance.EnemyShootTimeFactor)
        {
            MakeEnemyShoot();
            _elapsedShootTime = 0;
        }
    }
    private void ProcessMovement()
    {
        _elapsedMoveTime += Time.deltaTime;
        if (_elapsedMoveTime >= GameData.Instance.EnemyMoveTime)
        {
            CheckNextMovementDirection();
            //Move the enemies
            foreach (Enemy enemy in enemies)
            {
                //Change to coroutine.
                enemy._enemyMovement.MoveTo(new Vector2(enemy.Position().x + _enemyMoveDirectionCurrent.x, enemy.Position().y + _enemyMoveDirectionCurrent.y), 0.25f);
            }
            _elapsedMoveTime = 0;
        }
    }
    private void MakeEnemyShoot()
    {
        //Get random enemy
        if (enemies.Count <= 0)
            return;
        int index = Random.Range(0, enemies.Count-1);
        Enemy shooter = enemies[index];

        //Just to make sure that player didn't destroyed enemy.
        if(shooter != null)
        {
            Projectile.Create(shooter.gameObject, new string[] { "Enemy"}, shooter.transform.position, Vector2.down, _enemyProjectileSpeed, 1, ProjectileType.Enemy);
        }
    }

    private void CheckNextMovementDirection()
    {
        if (enemies.Count <= 0)
            return;
        Enemy edgeEnemy = null;
        if(_enemyMoveDirection == Vector2.right)
        {
            //find most-right enemy
            edgeEnemy = GetEdgeEnemy(false);
        }
        else
        {
            //find most-left enemy
            edgeEnemy = GetEdgeEnemy();
        }
        if(ShouldGoDown(edgeEnemy))
        {
            _enemyMoveDirectionCurrent = Vector2Int.down;
            //Reverse left-right movement dir
            _enemyMoveDirection *= -1;
        }
        else
        {
            _enemyMoveDirectionCurrent = _enemyMoveDirection;
        }
        
    }

    private bool ShouldGoDown(Enemy edgeEnemy)
    {
        float maxMagnitude = (_EnemyBoundingBox.BoxCollider.bounds.size / 2).x - _EnemyBoundingBox.BoxCollider.bounds.center.x;
        return Mathf.Abs(edgeEnemy.Position().x) >= maxMagnitude ? true : false;
    }

    private Enemy GetEdgeEnemy(bool lowest = true)
    {
        Enemy result = enemies[0];
        foreach (Enemy enemy in enemies)
        {
            if(lowest)
            {
                if(enemy.Position().x < result.Position().x)
                {
                    result = enemy;
                }
            }
            else 
            { 
                if(enemy.Position().x > result.Position().x)
                {
                    result = enemy;
                }
            }
        }
        return result;
    }

    private void OnEnemyDestoyed()
    {
        if(enemies.Count <= 0)
        {
            ResetLogic();
            SpawnEnemyWave();
        }
    }


}
