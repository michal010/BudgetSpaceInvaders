using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformConstains : MonoBehaviour
{
    [SerializeField] float _distanceConstraint;

    Vector3 _startingPos;
    float _xMin;
    float _xMax;


    private void Awake()
    {
        _startingPos = transform.position;
        _xMin = _startingPos.x - _distanceConstraint;
        _xMin = _startingPos.x + _distanceConstraint;
    }

    private void LateUpdate()
    {
        Vector3 oldPos = transform.position;
        float x = Mathf.Clamp(transform.position.x,_xMin,_xMax);
        transform.position = new Vector3(x,oldPos.y,oldPos.z);
    }
}
