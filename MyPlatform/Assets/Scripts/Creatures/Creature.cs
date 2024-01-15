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

        //Сервисные переменные
        protected Rigidbody2D Rbody;
        protected Vector2 Direction;
        protected Animator _Animator;
        protected bool IsGrounded;
        private bool _isJumping;

        //Анимационные ключи
        private static readonly int isGroundKey = Animator.StringToHash("is-ground");
        private static readonly int isRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        //Чтобы перекинуть сюда методы из Hero нужно вызвать часть метода в Creature и дополнительно вызвать в классе Hero
        //Для этого воспользуемся переопределением методов, а в классе Hero мы можем воспользоваться protected override void Awake
        //тем самым переопределив Awake с базового класса, потом с помощью base вызываем метод Awake в базовом классе и дальше
        //пишем код который нужен именно в классе наследнике. Protected позволяет видеть классы и переменные в классах-наследниках
        //Также расправляемся с остальными методами, которые можно выделить в общие методы Creatures
        protected virtual void Awake()
        {
            Rbody = GetComponent<Rigidbody2D>();
            _Animator = GetComponent<Animator>();
        }
        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        } // Принимающий данные о Input метод
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
            //Для того, чтобы герой подлетел наверх
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
/*
 * Создать новый скрипт DoInteractionComponent в namespace .Components
 * в классе public class DoInteractionComponent : MonoBehavior{
 * public void DoInteraction(GameObject go)
 *      {
 *          var interactable = go.GetComponent<InteractableComponent>(); 
 *          if (interactable != null)
 *              interactable.Intetact();
 *      }
 * 
 * 
 * }
 * 
 * 
 * 
 */
/*
 * Создание ИИ через Coroutine
 * Создаем класс MobAI : MonoBehavior в .Creatures
 * {
 *  [SerializeField] private LayerCheck _vision;
 *  [SerializeField] private LayerCheck _canAttack;
 *    
 *  
 *  [SerializeField] private float _alarmDelay = 0.5f;
 *  [SerializeField] private float _attackCooldown = 1f;
 *  [SerializeField] private float _missHeroCooldown = 0.5f;
 *  private Coroutine _current;
 *  private GameObject _target;
 *  
 *  private static readonly int IsDeadKey = Animator.KeyToHash("is_dead")
 *  
 *  private SpawnListComponent _particles;
 *  private Creature _creature;
 *  private Animator _animator;
 *  private bool _isDead;
 *  private Patrol _patrol;
 *  
 *  private void Awake()
 *  {
 *      _particles =  GetComponent<SpawnListComponent>();
 *      _creature = GetComponent<Creature>();
 *      _animator = GetComponent<Animator>();
 *      _patrol = GetComponent<Patrol>();
 *  }
 *  private void Start()
 *  {
 *      StartState(_patrol.DoPatrol());
 *      
 *  }  
 *  
 *  
 *  public void OnHeroInVision(GameObject go)
 *  {
 *      if(_isDead) return;
 *      _target = go;
 *      StartState(AgroToHero());
 *  }
 *  
 *  
 *  
 *  private IEnumerator AgroToHero()
 *  {
 *      _particles.Spawn("Exclamation");
 *      yield return new WaitForSecond(_alarmDelay);
 *      
 *      StartState(GoToHero);
 *  }
 *  
 *  private IEnumerator GoToHero()
 *  {
 *      While(_vision.IsTouchingLayer)
 *      {
 *          if(_canAttack.IsTouchingLayer)
 *          {
 *              StartState(Attack());
 *          }
 *          else
 *          {
 *              SetDirectionToTarget();
 *          }         
 *          yield return null;
 *     
 *      }
 *      
 *      _particles.Spawn("MissHero");
 *      yield return new WaitForSeconds(_missHeroCooldown);
 *      
 *  }
 *  
 *  private IEnumerator Attack()
 *  {
 *      While(_canAttack.IsTouchingLayer)
 *          {
 *              _creature.Attack();
 *              yield return new WaitForSeconds(_attackCooldown);
 *          }
 *          
 *          
 *      StartState(GoToHero());
 *  }
 *  
 *  private void SetDirectionToTarget()
 *  {
 *      var direction = _target.transform.position - transform.position;
 *      direction.y = 0;
 *      _creature.SetDirection(direction.normalized);
 *  }
 *  
 *  

 *  
 *  
 *  
 *  
 *  private void StartState(IEnumerator coroutine)
 *  {
 *      _creature.SetDirection(Vector2.zero);
 *      
 *      if(_current != null)
 *          StopCoroutine(_current);
 *      _current = StartCoroutine(coroutine);
 *  }
 *  
 *  public void OnDie()
 *  {
 *      _isDead = true;
 *      _animator.SetBool(IsDeadKey,true)
 *      
 *      if(_current != null)
 *          StopCoroutine(_current);
 *  }
 *
 *} 
 *
 *
 *  Создадим новый класс чтобы вынести туда рутину для патрулирования в namespace .Creature (Родительский класс)
 *  public abstract class Patrol : Monobehavior
 *  {
 *      public abstact IEnumerator DoPatrol();

 *  
 *  }
 *  
 *  
 *
 * Создадим класс-наследник Patrol в namespace .Creatures
 * public class PointPatrol : Patrol
 * {
 *      [SerializeField] private Transform[] _points;
 *      [SerializeField] private float _treshold = 1f;
 *      
 *      private Creature _creature;
 *      private int _currentPoint;
 *      
 *      private void Awake()
 *      {
 *          _creature = GetComponent<Creature>();
 *      }
 *      
 *      public override IEnumerator DoPatrol()
 *      {
 *          while(enabled)
 *          {
 *              if(IsOnPoint())
 *              {
 *                  _currentPoint = (int) Mathf.Repeat(_currentPoint + 1; _points.Length);
 *              }
 *              var direction = _points[_currentPoint].position - transform.position;
 *              direction.y = 0;
 *              _creature.SetDirection(direction.normalized);
 *              yield return null;
 *          }
 *      
 *      }
 *      
 *      private bool IsOnPoint()
 *      {
 *          return (_points[_currentPoint].position - tranform.position).magnitude < _treshold;
 *      }
 * 
 * 
 * }
 * 
 * Создадим еще одного наследника Patrol в том же namespace
 * public class PlatformPatrol : Patrol
 * {
 *      public override IEnumerator DoPatrol()
 *      {
 *          yield return null;
 *      }
 * 
 * }
 *
 * 
 */


