using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatform.Creatures.Weapon
{
    public class SinusoidalProjectile : BaseProjectile
    {
        [SerializeField] private float _amplitude = 1f;
        [SerializeField] private float _frequency = 1f;
        private float _originalY;
        private float _time;
        protected override void Start()
        {
            base.Start();
            _originalY = RBody.position.y;

        }

        private void FixedUpdate()
        {
            var position = RBody.position;
            position.x += Direction * _speed;
            position.y = _originalY + Mathf.Sin((_time * _frequency) * _amplitude);
            RBody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }
    }
}

