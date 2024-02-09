
using MyPlatform.Components;
using UnityEngine;
using UnityEditor.Animations;
using MyPlatform.Model;

namespace MyPlatform.Creatures
{
    public class Hero : Creature
    {
        [SerializeField] private CheckCircleOverLap _interactionCheck;

        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private LayerCheck _wallCheck;

        [SerializeField] private float _interactionRadius;

        [SerializeField] private ParticleSystem _hitParticles;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _unarmed;

        

        private float _defaultGravityScale;

        private bool _allowDoubleJump;
        private bool _isOnWall;

        private GameSession _session;

        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = rbody.gravityScale;
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

            if (_wallCheck.IsTouchingLayer && _direction.x == transform.localScale.x)
            {
                _isOnWall = true;
                rbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                rbody.gravityScale = _defaultGravityScale;
            }


        }


        protected override float CalculateYVelocity()
        {
            var isJumpPressing = _direction.y > 0;
            if (_isGrounded || _isOnWall)
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
            if (!_isGrounded && _allowDoubleJump)
            {
                _allowDoubleJump = false;
                return _jumpSpeed;

            }
            return base.CalculateJumpVelocity(yVelocity);
        }

        //ѕроиграем здесь соотв. анимацию
        public override void TakeDamage()
        {
            base.TakeDamage();
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
            _interactionCheck.Check();

        }

        public override void Attack()
        {
            if (!_session.Data.IsArmed) return;
            base.Attack();

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

        /*

        using System.Linq
        namespace MyPlatform.Component
        public class ProbabilityDropComponent : Monobehavior{

        [SerializeField] private int _count;
        [SerializeField] private DropData[] _drop;
        [SerializeField] private DropEvent _onDropCalculated

        public void CalculateDrop(){
            var itemsToDrop = new GameObject[_count]
            var itemCount = 0;
            var total = _drop.Sum(dropData => dropData.Probability);
            var drop = _drop.OrderBy(dropData => drop.Data.Probability)
            while(itemCount < _count){
                
                var random = UnityEngine.Random.value * total
                foreach(var dropData in  sortedDrop){
                    if(dropdata.probability >= random){
                        itemsToDrop[itemCount] = dropData.Drop;
                        itemCount++;
                        break;
                    }
                }
                
            }
        _onDropCalculated?.Invoke(itemsToDrop);
        }


        [Serializable]
        public class DropData{
            public GameObject Drop;
            [Range(0f,100f)] public float Probability;
        }

        [Serializable]
        public class DropEvent : UnityEvent <GameObject[]>{
        }
    }
        using Unity.Engine;
        using Random = UnityEngine.Random;
        namespace MyPlatform
        public class RandomSpawner : MonoBehavior{
            [Header ("Spawn bound:")] [SerializeField]
            private float _sectorAngle = 60;
            
            [SerializeField] private float _sectorRotation;

            [SerializeField] private float _waitTime = 0.1f;
            [SerializeField] private float _speed = 6;
            [SerializeField] private float _itemPerBurst = 2;

            private Coroutine _routine;
            
            public void StartToDrop(GameObject[] items)
            {
                TryStopRoutine();
                
                _routine = StartCoroutine(StartSpawn(items));
            }

            private IEnumarator StartSpawn(GameObject[] particles)
            {
                for(var i = 0; i < particles.Length; i++)
                {
                    for(var i = 0; i < _itemPerBurst && i < particles.Length; i++)
                    {
                        Spawn(particles[i]);
                        i++;
                    }
                }
            }









    }
        */
    }
    /*
    
 namespace MyPlatform.Components{
  public class ShowTargetComponent{
    [Serialize Field] private Transform _target;    
    [Serialize Field] private  CameraStateController _controller;
    [Serialize Field] private float _delay = 0,5f;
    private void OnValidate(){

        if(_controller == null)
            _controller = FindObjectOfType<CameraStateController>();
    }

    public void ShowTarget(){
        _controller.SetPosition(_target.position);
        _controller.SetState(true);
        Invoke(nameof(MoveBack"), _delay);
    }

    public void MoveBack(){
        _controller.SetState(false);
    }


 }
 namespace MyPlatform.Components{
  public class CameraStateControllet{ 

    [Serialize Field] private Animator _animator;
    [Serialize Field] private CinemachineVirtualCamera _camera;

    public static readonly int ShowTargetKey = Animator.StringToHash("ShowTarget");

    public void SetPosition(Vector3 position){
        
        targetPosition.z = _camera.transform.position.z;
        _camera.transform.position = targetPosition;

    }

    public void SetState(bool state){

    _animator.SetBool(ShowTargetKey,state);
    }


    */
}

