
using UnityEngine;

namespace MyPlatform.Components.Movements
{
    public class VerticalMovement : MonoBehaviour
    {
        [SerializeField] private float _amplitude = 1f;
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private bool _randomize;


        private float _originalY;
        private float _seed;
        private Rigidbody2D _rbody;


        private void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
            _originalY = _rbody.position.y;

            if (_randomize)
                _seed = Random.value * Mathf.PI * 2;
        }

        private void Update()
        {
            var pos = _rbody.position;
            pos.y = _originalY + Mathf.Sin((_seed + Time.time * _frequency) * _amplitude);
            _rbody.MovePosition(pos);
        }
    }
}

