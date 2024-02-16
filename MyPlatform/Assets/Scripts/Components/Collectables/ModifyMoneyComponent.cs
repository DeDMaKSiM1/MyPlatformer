using MyPlatform.Creatures.Hero;
using UnityEngine;

namespace MyPlatform.Components.Collectables
{
    public class ModifyMoneyComponent : MonoBehaviour
    {

        [SerializeField] private int _moneyDelta;

        public void AddMoney(GameObject target)
        {

            var moneyComponent = target.GetComponent<Hero>();
            if (moneyComponent != null)
            {
                moneyComponent.ModifyMoney(_moneyDelta);
            }


        }



    }
}



