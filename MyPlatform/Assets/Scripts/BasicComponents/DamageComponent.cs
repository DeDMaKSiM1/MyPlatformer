using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyPlatform.Components
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;

        //�����, ������� ����� ���� � �������
        public void ApplyDamage(GameObject target) //�� �� ������ ��� ��������� � EnterCollision ���� GameObject - ������ ��� � �������������.
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyDamage(_damage);
            }

        }
    }

}
