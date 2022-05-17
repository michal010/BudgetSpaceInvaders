using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour, IDamagable<int>
{
    public bool TakeDamage(int value, GameObject source)
    {
        if(source != null)
        {
            if (source.CompareTag(gameObject.tag))
                return false;
        }
        GameData.Instance.PlayerHP -= value;
        return true;
    }

}
