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
            var force = new Vector2(_direction * _speed, 0);
            _rBody.AddForce(force, ForceMode2D.Impulse);
        }


        //private void FixedUpdate()
        //{
        //    var position = _rBody.position;
        //    position.x +=_direction * _speed;
        //    _rBody.MovePosition(position);//почему двигаем тут через rigidbody - юнити советует, если на ГО(GameObject) есть компонент rigidbody нужно двигать через него, чтобы не было проблем с синхронизацией трансформа и физического объекта

        //}
    }
}

