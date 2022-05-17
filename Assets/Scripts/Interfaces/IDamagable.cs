using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable<T>{
    public bool TakeDamage(T value, GameObject source);
}