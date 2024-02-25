using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatform.Creatures.Weapon
{
    public class Projectile : BaseProjectile
    {

        protected override void Start()
        {
            base.Start();
            var force = new Vector2(Direction * _speed, 0);
            RBody.AddForce(force, ForceMode2D.Impulse);
        }


        //private void FixedUpdate()
        //{
        //    var position = _rBody.position;
        //    position.x +=_direction * _speed;
        //    _rBody.MovePosition(position);//почему двигаем тут через rigidbody - юнити советует, если на ГО(GameObject) есть компонент rigidbody нужно двигать через него, чтобы не было проблем с синхронизацией трансформа и физического объекта

        //}
    }
}

