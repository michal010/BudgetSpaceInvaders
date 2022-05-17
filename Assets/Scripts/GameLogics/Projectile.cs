using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { Player, Enemy}

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, IBoundable
{
    private Vector3 _ShootDir;
    private float _Speed;
    private float _DestructionTime;
    private int _damageAmount;
    private string[] _ignoreTags;
    private GameObject _shooter;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rigidbody.velocity = _ShootDir * _Speed;
        Destroy(gameObject, _DestructionTime);
    }
    public static Projectile Create(GameObject shooter, string[] ignoreTags,Vector3 spawnPoint, Vector3 direction, float speed, int damageAmount,ProjectileType type,float destructionTime = 4f)
    {
        Transform projectileTransform = null;
        switch (type)
        {
            case ProjectileType.Player:
                projectileTransform = Instantiate(GameAssets.Instance.PFb_PlayerProjectile, spawnPoint, Quaternion.identity).transform;
                break;
            case ProjectileType.Enemy:
                projectileTransform = Instantiate(GameAssets.Instance.PFb_EnemyProjectile, spawnPoint, Quaternion.identity).transform;
                break;
            default:
                break;
        }
        Projectile projectile = projectileTransform.GetComponent<Projectile>();
        projectile.Setup(shooter, ignoreTags,direction, speed, damageAmount, destructionTime);
        return projectile;
    }
    private void Setup(GameObject shooter,string[] ignoreTags, Vector3 dir, float speed, int damageAmount, float destructionTime)
    {
        _ShootDir = dir;
        _Speed = speed;
        _DestructionTime = destructionTime;
        _ignoreTags = ignoreTags;
        _shooter = shooter;
        _damageAmount = damageAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var s in _ignoreTags)
        {
            if (collision.CompareTag(s))
                return;
        }
        IDamagable<int> damagable = collision.GetComponent<IDamagable<int>>();
        if (damagable != null)
        {
            if(damagable.TakeDamage(_damageAmount, _shooter))
                Destroy(gameObject);
        }
    }

    public void OnBoundingBoxExit(BoundingBox box)
    {
        Destroy(gameObject);
    }
}
