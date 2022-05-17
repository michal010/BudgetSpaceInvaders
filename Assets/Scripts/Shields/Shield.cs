using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shield : MonoBehaviour, IDamagable<int>, IResetable
{
    private int _MaxHp = 3;
    private int _CurrentHp;
    [SerializeField] private string _IgnoreDamageFromTag;

    [SerializeField] TMP_Text _hpText;

    private void Awake()
    {
        _CurrentHp = _MaxHp;
    }

    public bool TakeDamage(int value, GameObject source)
    {
        if(source != null)
        {
            if (source.CompareTag(_IgnoreDamageFromTag))
                return true;
        }
        _CurrentHp--;
        UpdateUI();
        if(_CurrentHp <= 0)
        {
            OnDeath();
        }
        return true;
    }

    private void UpdateUI()
    {
        _hpText.text = _CurrentHp.ToString();
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
    }

    public void ResetLogic()
    {
        _CurrentHp = _MaxHp;
        gameObject.SetActive(true);
    }
}
