using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyComponent : MonoBehaviour
{

    [SerializeField] private int _money;

    public void ModifyMoney(int moneyDelta)
    {
        _money += moneyDelta;
    }
}
