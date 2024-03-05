using MyPlatform.Components.GOBased;
using UnityEngine;
using MyPlatform.Components.ColliderBased;
using MyPlatform.Audio;

namespace MyPlatform.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private bool _invertScale;
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damageVelocity;
        [SerializeField] private int _damage;


        [Header("Checkers")]

        [SerializeField] protected ColliderCheck _groundCheck;
        [SerializeField] private CheckCircleOverLap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        //—ервисные переменные
        protected Rigidbody2D Rbody;
        protected Vector2 Direction;
        protected Animator Animator;
        protected PlaySoundsComponent Sounds;
        protected bool IsGrounded;
        private bool isJumping;

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
            Rbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();
        }
        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        } // ѕринимающий данные о Input метод
        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }
        private void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            Rbody.velocity = new Vector2(xVelocity, yVelocity);


            Animator.SetBool(isGroundKey, IsGrounded);
            Animator.SetBool(isRunning, Direction.x != 0);
            Animator.SetFloat(VerticalVelocity, Rbody.velocity.y);

            UpdateSpriteDirection(Direction);

        }
        protected virtual float CalculateYVelocity()
        {
            var yVelocity = Rbody.velocity.y;
            var isJumpPressing = Direction.y > 0;
            if (IsGrounded)
            {
                isJumping = false;
            }

            if (isJumpPressing)
            {
                isJumping = true;

                var isFalling = Rbody.velocity.y <= 0.001f;


                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            else if (Rbody.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }
            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {

            if (IsGrounded)
            {
                yVelocity = _jumpSpeed;
                DoJumpVfx();
            }
            return yVelocity;
        }

        protected void DoJumpVfx()
        {
            Sounds.Play("Jump");
            _particles.Spawn("Jump");

        }

        public void UpdateSpriteDirection(Vector2 direction)
        {
            var multipiller = _invertScale ? -1 : 1;

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(multipiller, 1, 1) ;
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1 * multipiller, 1, 1);
            }

        }

        public virtual void TakeDamage()
        {
            Animator.SetTrigger(Hit);
            //ƒл€ того, чтобы герой подлетел наверх
            Rbody.velocity = new Vector2(Rbody.velocity.x, _damageVelocity);
        }
        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);

        }
        public void GetAttack()
        {
            _attackRange.Check();
            Sounds.Play("Melee");
        }


    }
}



