using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatform.Creatures.Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private bool _invertX;

        private int _direction;
        private Rigidbody2D _rBody;
        private void Start()
        {
            var mod = _invertX ? -1 : 1;
            _direction =mod * transform.lossyScale.x > 0 ? 1 : -1;
            _rBody = GetComponent<Rigidbody2D>();
            var force = new Vector2(_direction * _speed, 0);
            _rBody.AddForce(force, ForceMode2D.Impulse);
        }


        //private void FixedUpdate()
        //{
        //    var position = _rBody.position;
        //    position.x +=_direction * _speed;
        //    _rBody.MovePosition(position);//������ ������� ��� ����� rigidbody - ����� ��������, ���� �� ��(GameObject) ���� ��������� rigidbody ����� ������� ����� ����, ����� �� ���� ������� � �������������� ���������� � ����������� �������

        //}
    }
}

