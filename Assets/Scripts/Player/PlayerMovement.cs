using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    #region Settings
    [SerializeField]float _movementSpeed = 10f;
    [SerializeField] float _projectileSpeed = 10f;
    #endregion

    #region References
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] Transform _shootFromPosition;
    #endregion
    float _horizontalInput;
    Projectile projectile;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        _rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _movementSpeed, 0);
        if (Input.GetKeyDown(KeyCode.Space) && (projectile == null))
        {
            projectile = Projectile.Create(gameObject, new string[] { "Player" }, _shootFromPosition.position, Vector3.up, _projectileSpeed, 1, ProjectileType.Player);
        }
    }
}
