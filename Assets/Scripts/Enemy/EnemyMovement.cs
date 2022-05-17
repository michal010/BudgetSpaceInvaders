using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Vector2 _moveToPosition;
    [SerializeField] float _moveTime;

    float _currentMoveTime;
    Vector2 _startingPositon;
    bool doMove = false;

    void FixedUpdate()
    {
        if (!doMove)
            return;
        _currentMoveTime += Time.deltaTime;
        float step = _currentMoveTime / _moveTime;
        transform.position = Vector2.Lerp(_startingPositon, _moveToPosition, step);
        if (Vector2.Distance(transform.position, _moveToPosition) < 0.01f)
        {
            transform.position = _moveToPosition;
            doMove = false;
        }
    }

    public void MoveTo(Vector2 position, float moveTime)
    {
        _moveToPosition = position;
        _moveTime = moveTime;
        _startingPositon = transform.position;
        _currentMoveTime = 0;
        doMove = true;
    }
}
