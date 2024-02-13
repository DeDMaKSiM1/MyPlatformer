using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatform.Creatures.Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private int _direction;
        private Rigidbody2D _rBody;
        private void Start()
        {
            _direction = transform.lossyScale.x > 0 ? 1 : -1;
            _rBody = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            var position = _rBody.position;
            position.x +=_direction * _speed;
            _rBody.MovePosition(position);//������ ������� ��� ����� rigidbody - ����� ��������, ���� �� ��(GameObject) ���� ��������� rigidbody ����� ������� ����� ����, ����� �� ���� ������� � �������������� ���������� � ����������� �������

        }
    }
}

