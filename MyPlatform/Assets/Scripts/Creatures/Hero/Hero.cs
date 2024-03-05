using UnityEngine;
using UnityEditor.Animations;
using MyPlatform.Model;
using MyPlatform.Utils;
using MyPlatform.Components.ColliderBased;
using MyPlatform.Components.Health;
using System.Collections;
using MyPlatform.Components;

namespace MyPlatform.Creatures.Hero
{
    public class Hero : Creature
    {
        [SerializeField] private CheckCircleOverLap _interactionCheck;
        [SerializeField] private ColliderCheck _wallCheck;

        [SerializeField] private LayerMask _groundLayer;

        [Header("Super throw")]
        [SerializeField] private Cooldown _superThrowCooldown;
        [SerializeField] private int _superThrowCount;
        [SerializeField] private float _superThrowDelay;



        [Header("Money Drop")]
        [SerializeField] private ProbabilityDropComponent _hitDrop;


        [SerializeField] private float _slamDownVelocity;
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _unarmed;

        private static readonly int ThrowKey = Animator.StringToHash("throw");
        private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");

        private float _defaultGravityScale;

        private bool _allowDoubleJump;
        private bool _isOnWall;
        private bool _superThrow;

        private GameSession _session;

        private int CoinCount => _session.Data.Inventory.Count("Coin");
        private int SwordCount => _session.Data.Inventory.Count("Sword");
        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = Rbody.gravityScale;
            Animator = GetComponent<Animator>();
        }
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();

            _session.Data.Inventory.onChanged += OnInventoryChanged;
            //_session.Data.Inventory.onChanged += AnotherHandler;

            health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }
        private void OnDestroy()
        {
            _session.Data.Inventory.onChanged -= OnInventoryChanged;

        }
        //private void AnotherHandler(string id, int value)
        //{
        //    Debug.Log($"Inventory changed: {id} {value}");
        //}
        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
                UpdateHeroWeapon();
        }
        protected override void Update()
        {
            base.Update();

            //var moveToSameDirectionX = Direction.x * transform.lossyScale.x > 0;

            if (_wallCheck.IsTouchingLayer && Direction.x == transform.localScale.x)
            {
                _isOnWall = true;
                Rbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                Rbody.gravityScale = _defaultGravityScale;
            }

            Animator.SetBool(IsOnWall, _isOnWall);
        }
        protected override float CalculateYVelocity()
        {
            var isJumpPressing = Direction.y > 0;
            if (IsGrounded || _isOnWall)
            {
                _allowDoubleJump = true;
            }

            if (!isJumpPressing && _isOnWall) //дл€ того чтобы зависать на стенках
            {

                return 0f;
            }
            //»наче используем базовый метод в котором мы уже обсчитываем все дл€ прыжка.
            return base.CalculateYVelocity();
        }
        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!IsGrounded && _allowDoubleJump && !_isOnWall)
            {
                _allowDoubleJump = false;
                DoJumpVfx();
                return _jumpSpeed;

            }

            return base.CalculateJumpVelocity(yVelocity);
        }
        //ѕроиграем здесь соотв. анимацию
        public override void TakeDamage()
        {
            base.TakeDamage();
 
            if (CoinCount > 0) SpawnCoins();

        }
        private void SpawnCoins()
        {


            var numCoinsToDispose = Mathf.Min(CoinCount, 5);
            _session.Data.Inventory.Remove("Coin", numCoinsToDispose);

            _hitDrop.SetCount(numCoinsToDispose);
            _hitDrop.CalculateDrop();

            Debug.Log(CoinCount);
        }
        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }
        public void Interact()//метод возвращает количество результатов, который он получил,в рамках его работы - сделает сферу вокруг его позиции и запишет все резы в массив//и вернет размер
        {
            _interactionCheck.Check();

        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _slamDownVelocity)
                {
                    _particles.Spawn("SlamDown");
                }
            }

        }
        public override void Attack()
        {
            var numSwords = SwordCount;

            if (numSwords <= 0) return;
            _particles.Spawn("Attack");
            base.Attack();

        }
        private void UpdateHeroWeapon()
        {

            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _unarmed;
        }
        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }
        public void OnDoThrow()
        {
            if (_superThrow)
            {
                var numThrows = Mathf.Min(_superThrowCount, SwordCount - 1);
                StartCoroutine(DoSuperThrow(numThrows));
            }
            else
            {
                ThrowAndRemoveFromInventory();
            }
            _superThrow = false;
        }
        private IEnumerator DoSuperThrow(int numThrow)
        {
            for (int i = 0; i < numThrow; i++)
            {
                ThrowAndRemoveFromInventory();
                yield return new WaitForSeconds(_superThrowDelay);
            }
        }
        private void ThrowAndRemoveFromInventory()
        {
            Sounds.Play("Range");
            _particles.Spawn("Throw");
            _session.Data.Inventory.Remove("Sword", 1);
        }
        public void StartThrowing()
        {
            _superThrowCooldown.Reset();
        }
        public void ThrowingComplete()
        {
            if (!_throwCooldown.IsReady || SwordCount <= 1) return;

            if (_superThrowCooldown.IsReady) _superThrow = true;

            Animator.SetTrigger(ThrowKey);
            _throwCooldown.Reset();
        }

    }

}

