using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldsController : MonoBehaviour
{
    [SerializeField] EventTrigger DestroyShieldsEventTrigger;
    [SerializeField] List<Shield> AffectedShields;
    private void Start()
    {
        DestroyShieldsEventTrigger.OnTriggerEnter.AddListener(OnEnemyAtShieldsLevel);
    }

    private void OnEnemyAtShieldsLevel()
    {
        foreach (var shield in AffectedShields)
        {
            AffectedShields.Remove(shield);
            Destroy(shield.gameObject);
        }
    }
}
