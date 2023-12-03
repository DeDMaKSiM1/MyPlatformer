using UnityEngine;
using UnityEngine.Events;

namespace MyPlatform.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        //Создадим Unity-Event ,которые позволят нам создать поведение события когда мы получаем урон и когда умираем.
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;

        public void ApplyDamage(int damageValue)
        {
            _health -= damageValue;
            _onDamage?.Invoke();
            if(_health <= 0)
            {
                _onDie?.Invoke();
            }
        }

    }
}

