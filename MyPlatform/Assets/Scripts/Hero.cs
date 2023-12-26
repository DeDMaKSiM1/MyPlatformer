
using MyPlatform.Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.Animations;
using MyPlatform.Model;

namespace MyPlatform.Creatures
{
    public class Hero : Creature
    {
        [SerializeField] private float _ineractionRadius;
        [SerializeField] private LayerMask _interactionLayer;

        [SerializeField] private ParticleSystem _hitParticles;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _unarmed;

        private Collider2D[] _ineractionResult = new Collider2D[1];
        private bool _allowDoubleJump;

        private GameSession _session;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();

            health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }


        protected override void Update()
        {
            base.Update();
        }


        protected override float CalculateYVelocity()
        {
            var isJumpPressing = _direction.y > 0;
            if (_isGrounded || _isOnWall)
            {
                _allowDoubleJump = true;
            }

            if (!isJumpPressing && _isOnWall) //для того чтобы зависать на стенках
            {
                return 0f;
            }
            //Иначе используем базовый метод в котором мы уже обсчитываем все для прыжка.
            return base.CalculateYVelocity() ;
        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = rbody.velocity.y <= 0.001f;
            if (!isFalling) return yVelocity;
            if (_isGrounded)
            {
                yVelocity += _jumpSpeed;
            }
            else if (_allowDoubleJump)
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
                transform.localScale = new Vector3(-1, 1, 1);
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

        //Проиграем здесь соотв. анимацию
        public void TakeDamage()
        {
            _animator.SetTrigger(Hit);
            //Для того, чтобы герой подлетел наверх
            rbody.velocity = new Vector2(rbody.velocity.x, _damageJumpSpeed);

            if (_session.Data.Coins > 0) SpawnCoins();

        }

        private void SpawnCoins()
        {
            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();

            var numCoinsToDispose = Mathf.Min(_session.Data.Coins, 5);
            _session.Data.Coins -= numCoinsToDispose;
            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);
            Debug.Log(_session.Data.Coins);
        }

        public void ModifyMoney(int moneyDelta)
        {
            _session.Data.Coins += moneyDelta;
            Debug.Log(_session.Data.Coins);
        }

        public void Interact()//метод возвращает количество результатов, который он получил,в рамках его работы - сделает сферу вокруг его позиции и запишет все резы в массив//и вернет размер
        {

            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _ineractionRadius, _ineractionResult, _interactionLayer);
            //Метод позволяющий пересекающий объект, но не будет выделять лишнюю память
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
            if (!_session.Data.IsArmed) return;
            _animator.SetTrigger(AttackKey);

        }


        public void GetAttack()
        {
            var gos = _attackRange.GetObjectInRange();
            foreach (var go in gos)
            {
                var hp = go.GetComponent<HealthComponent>();
                if (hp != null && go.CompareTag("Enemy"))//Тут энеми нужно у врагов поставить тег
                {
                    hp.ModifyHealth(-_damage);
                }
            }
        }

        public void ArmHero()
        {
            _session.Data.IsArmed = true;
            UpdateHeroWeapon();
        }

        private void UpdateHeroWeapon()
        {
            _animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _unarmed;
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }


    }

}

