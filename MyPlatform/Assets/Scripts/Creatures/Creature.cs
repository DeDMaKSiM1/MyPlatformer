using MyPlatform.Components;
using UnityEngine;

namespace MyPlatform.Creatures
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _damageJumpSpeed;
        [SerializeField] private int _damage;

        [SerializeField] private LayerCheck _groundCheck;

        [SerializeField] private CheckCircleOverLap _attackRange;
        [SerializeField] private SpawnListComponent _particles;

        //—ервисные переменные
        private Rigidbody2D rbody;
        Vector2 _direction;
        private Animator _animator;
        protected bool _isGrounded;
        private bool _isJumping;

        //јнимационные ключи
        private static readonly int isGroundKey = Animator.StringToHash("is-ground");
        private static readonly int isRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        //„тобы перекинуть сюда методы из Hero нужно вызвать часть метода в Creature и дополнительно вызвать в классе Hero
        //ƒл€ этого воспользуемс€ переопределением методов, а в классе Hero мы можем воспользоватьс€ protected override void Awake
        //тем самым переопределив Awake с базового класса, потом с помощью base вызываем метод Awake в базовом классе и дальше
        //пишем код который нужен именно в классе наследнике. Protected позвол€ет видеть классы и переменные в классах-наследниках
        //“акже расправл€емс€ с остальными методами, которые можно выделить в общие методы Creatures
        protected virtual void Awake()
        {
            rbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        } // ѕринимающий данные о Input метод
        protected virtual void Update()
        {
            _isGrounded = _groundCheck.IsTouchingLayer;
        }
        private void FixedUpdate()
        {
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            rbody.velocity = new Vector2(xVelocity, yVelocity);


            _animator.SetBool(isGroundKey, _isGrounded);
            _animator.SetBool(isRunning, _direction.x != 0);
            _animator.SetFloat(VerticalVelocity, rbody.velocity.y);

            UpdateSpriteDirection();

        }
        protected virtual float CalculateYVelocity()
        {
            var yVelocity = rbody.velocity.y;
            var isJumpPressing = _direction.y > 0;
            if (_isGrounded)
            {
                _isJumping = false;
            }

            if (isJumpPressing)
            {
                _isJumping = true;

                var isFalling = rbody.velocity.y <= 0.001f;
                if (!isFalling) return yVelocity;

                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            else if (rbody.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }
            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {

            if (_isGrounded)
            {
                yVelocity = _jumpSpeed;
            }

            return yVelocity;
        }


    }
}


