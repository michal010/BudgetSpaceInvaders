using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private string TriggeringTag;
    private UnityEvent _OnTriggerEnter;
    public UnityEvent OnTriggerEnter
    {
        get { return _OnTriggerEnter; }
        private set { _OnTriggerEnter = value; }
    }


    private void Awake()
    {
        if(OnTriggerEnter == null)
        {
            OnTriggerEnter = new UnityEvent();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TriggeringTag))
        {
            OnTriggerEnter.Invoke();
        }
    }
}
