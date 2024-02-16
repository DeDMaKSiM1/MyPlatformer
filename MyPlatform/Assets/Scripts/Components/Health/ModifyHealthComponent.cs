using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyPlatform.Components.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta;

        //�����, ������� ����� ���� � �������
        public void Apply(GameObject target) //�� �� ������ ��� ��������� � EnterCollision ���� GameObject - ������ ��� � �������������.
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(_hpDelta);
            }

        }
    }

}
