using MyPlatform.Components;
using UnityEngine;

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

        [SerializeField] protected LayerCheck _groundCheck;
        [SerializeField] private CheckCircleOverLap _attackRange;
        [SerializeField] private SpawnListComponent _particles;

        //��������� ����������
        protected Rigidbody2D Rbody;
        protected Vector2 Direction;
        protected Animator _Animator;
        protected bool IsGrounded;
        private bool _isJumping;

        //������������ �����
        private static readonly int isGroundKey = Animator.StringToHash("is-ground");
        private static readonly int isRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        //����� ���������� ���� ������ �� Hero ����� ������� ����� ������ � Creature � ������������� ������� � ������ Hero
        //��� ����� ������������� ���������������� �������, � � ������ Hero �� ����� ��������������� protected override void Awake
        //��� ����� ������������� Awake � �������� ������, ����� � ������� base �������� ����� Awake � ������� ������ � ������
        //����� ��� ������� ����� ������ � ������ ����������. Protected ��������� ������ ������ � ���������� � �������-�����������
        //����� ������������� � ���������� ��������, ������� ����� �������� � ����� ������ Creatures
        protected virtual void Awake()
        {
            Rbody = GetComponent<Rigidbody2D>();
            _Animator = GetComponent<Animator>();
        }
        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        } // ����������� ������ � Input �����
        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }
        private void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            Rbody.velocity = new Vector2(xVelocity, yVelocity);


            _Animator.SetBool(isGroundKey, IsGrounded);
            _Animator.SetBool(isRunning, Direction.x != 0);
            _Animator.SetFloat(VerticalVelocity, Rbody.velocity.y);

            UpdateSpriteDirection();

        }
        protected virtual float CalculateYVelocity()
        {
            var yVelocity = Rbody.velocity.y;
            var isJumpPressing = Direction.y > 0;
            if (IsGrounded)
            {
                _isJumping = false;
            }

            if (isJumpPressing)
            {
                _isJumping = true;

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
            }

            return yVelocity;
        }

        private void UpdateSpriteDirection()
        {
            var multipiller = _invertScale ? -1 : 1;

            if (Direction.x > 0)
            {
                transform.localScale = new Vector3(multipiller, 1, 1) ;
            }
            else if (Direction.x < 0)
            {
                transform.localScale = new Vector3(-1 * multipiller, 1, 1);
            }

        }

        public virtual void TakeDamage()
        {
            _Animator.SetTrigger(Hit);
            //��� ����, ����� ����� �������� ������
            Rbody.velocity = new Vector2(Rbody.velocity.x, _damageVelocity);
        }
        public virtual void Attack()
        {
            _Animator.SetTrigger(AttackKey);

        }
        public void GetAttack()
        {
            _attackRange.Check();

        }

    }
}



