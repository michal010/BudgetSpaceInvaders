using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoundingBox : MonoBehaviour
{
    public BoxCollider2D BoxCollider;

    private void Awake()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IBoundable boundable = collision.GetComponent<IBoundable>();
        if (boundable != null)
            boundable.OnBoundingBoxExit(this);
    }
}
