using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyPlatform.Components.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta;

        //метод, который дамаг этот и нанесет
        public void Apply(GameObject target) //мы не просто так прописали в EnterCollision свой GameObject - теперь тут и воспользуемся.
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(_hpDelta);
            }

        }
    }

}
