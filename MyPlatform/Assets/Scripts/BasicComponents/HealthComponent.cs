using System;
using UnityEngine;
using UnityEngine.Events;

namespace MyPlatform.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        //Создадим Unity-Event ,которые позволят нам создать поведение события когда мы получаем урон и когда умираем.
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _OnHeal;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;


        public void ModifyHealth(int helthDelta)
        {
            _health += helthDelta;
            _onChange?.Invoke(_health);


            if (helthDelta < 0)
            {
                _onDamage?.Invoke();
            }

            if (helthDelta > 0)
            {
                _OnHeal?.Invoke();
            }



            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }
        

        public void SetHealth(int health)
        {
            _health = health;
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {

        }
    }
}

