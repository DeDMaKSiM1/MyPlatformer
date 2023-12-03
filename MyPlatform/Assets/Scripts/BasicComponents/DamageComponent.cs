using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyPlatform.Components
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;

        //метод, который дамаг этот и нанесет
        public void ApplyDamage(GameObject target) //мы не просто так прописали в EnterCollision свой GameObject - теперь тут и воспользуемся.
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyDamage(_damage);
            }

        }
    }

}
