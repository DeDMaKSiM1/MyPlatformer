
using MyPlatform.Components;
using UnityEngine;
using UnityEngine.Events;
namespace MyPlatform
{
    public class Hero : MonoBehaviour
    {
        //����� ��������� ������������ ������������, ��� ������ � ����� ������
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;

        [SerializeField] private LayerCheck _groundCheck;

        [SerializeField] private float _damageJumpSpeed;

        [SerializeField] private float _ineractionRadius;

        [SerializeField] private LayerMask _interactionLayer;

        [SerializeField] private CheckCircleOverLap _attackRange;
        [SerializeField] private int _damage;

        [SerializeField] private ParticleSystem _hitParticles;

        Vector2 _direction;
        private Rigidbody2D rbody;
        private Animator _animator;

        private int _coins;

        private bool _isGrounded;
        private bool _allowDoubleJump;

        private static readonly int isGroundKey = Animator.StringToHash("is-ground");
        private static readonly int isRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");



        private SpriteRenderer _sprite;

        private Collider2D[] _ineractionResult = new Collider2D[1];

        private void Awake()
        {
            rbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        } // ����������� ������ � Input �����

        private void Update()
        {
            _isGrounded = IsGrounded();
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

        private float CalculateYVelocity()
        {
            if (_isGrounded) _allowDoubleJump = true;
            var yVelocity = rbody.velocity.y;
            var isJumpPressing = _direction.y > 0;

            if (isJumpPressing)
            {
                yVelocity = CalculateJumpVelocity(yVelocity);
            }
            else if (rbody.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }
            return yVelocity;
        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = rbody.velocity.y <= 0.001f;
            if (!isFalling) return yVelocity;
            if (_isGrounded)
            {
                yVelocity += _jumpSpeed;
            }else if (_allowDoubleJump)
            {
                yVelocity = _jumpSpeed;
                _allowDoubleJump = false;
               
            }
            return yVelocity;
        }

        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-1,1,1);
            }

        }
        private bool IsGrounded()
        {
            return _groundCheck.IsTouchingLayer;
        }
        public void OnDrawGizmos()
        {
            Gizmos.color = IsGrounded() ? Color.green : Color.red;
        }

        public void Saying()
        {
            Debug.Log(message: "Aaaargh");
        }

        //��������� ����� �����. ��������
        public void TakeDamage()
        {
            _animator.SetTrigger(Hit);
            //��� ����, ����� ����� �������� ������
            rbody.velocity = new Vector2(rbody.velocity.x, _damageJumpSpeed);

            if(_coins > 0) SpawnCoins();

        }

        private void SpawnCoins()
        {
            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();

            var numCoinsToDispose = Mathf.Min(_coins, 5);
            _coins -=numCoinsToDispose;
            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);
            Debug.Log(_coins);
        }

        public void ModifyMoney(int moneyDelta)
        {
            _coins += moneyDelta;
            Debug.Log(_coins);
        }

        public void Ineract()//����� ���������� ���������� �����������, ������� �� �������,� ������ ��� ������ - ������� ����� ������ ��� ������� � ������� ��� ���� � ������//� ������ ������
        {
            
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _ineractionRadius, _ineractionResult, _interactionLayer);
            //����� ����������� ������������ ������, �� �� ����� �������� ������ ������
            for (int i = 0; i < size; i++)
            {
                var interactable = _ineractionResult[i].GetComponent<InteractableComponent>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }

        public void Attack()
        {
            _animator.SetTrigger(AttackKey);
            
        }


        public void GetAttack()
        {
            var gos = _attackRange.GetObjectInRange();
            foreach (var go in gos)
            {
                var hp = go.GetComponent<HealthComponent>();
                if (hp != null && go.CompareTag("Enemy"))//��� ����� ����� � ������ ��������� ���
                {
                    hp.ModifyHealth(-_damage);
                }
            }
        }
    }

}

