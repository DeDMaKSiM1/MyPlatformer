using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyPlatform.Components
{
    public class ModifyMoneyComponent : MonoBehaviour
    {

        [SerializeField] private int _moneyDelta;

        public void AddMoney(GameObject target)
        {

            var moneyComponent = target.GetComponent<MoneyComponent>();
            if(moneyComponent != null )
            {
                moneyComponent.ModifyMoney(_moneyDelta);
            }


        }



    }
}

