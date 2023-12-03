
using MyPlatform.Components;
using UnityEngine;
using UnityEngine.Events;
namespace MyPlatform
{
    public class Hero : MonoBehaviour
    {
        //Нужно воплотить вертикальное передвижение, без физики в одном методе
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;

        [SerializeField] private LayerCheck _groundCheck;

        [SerializeField] private float _damageJumpSpeed;

        [SerializeField] private float _ineractionRadius;
        
        [SerializeField] private LayerMask _interactionLayer;

        Vector2 _direction;
        private Rigidbody2D rbody;
        private Animator _animator;

        private static readonly int isGroundKey = Animator.StringToHash("is-ground");
        private static readonly int isRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");



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
        } // Принимающий данные о Input метод

        private void FixedUpdate()
        {
            rbody.velocity = new Vector2(_direction.x * _speed, rbody.velocity.y);

            var isJumping = _direction.y > 0;
            var isGrounded = IsGrounded();
            if (isJumping)
            {
                if (isGrounded && rbody.velocity.y <= 0)
                {
                    rbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }
            }
            else if (rbody.velocity.y > 0)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, rbody.velocity.y * 0.5f);
            }

            _animator.SetBool(isGroundKey, isGrounded);
            _animator.SetBool(isRunning, _direction.x != 0);
            _animator.SetFloat(VerticalVelocity, rbody.velocity.y);

            UpdateSpriteDirection();

        }
        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                _sprite.flipX = false;
            }
            else if (_direction.x < 0)
            {
                _sprite.flipX = true;
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

        }

        public void Ineract()//метод возвращает количество результатов, который он получил, в рамках его работы - сделает сферу вокруг его позиции и запишет все резы в массив
            //и вернет размер
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position, 
                _ineractionRadius, 
                _ineractionResult, 
                _interactionLayer);//Метод позволяющий пересекающий объект, но не будет выделять лишнюю память
            for(int i = 0; i < size; i++)
            {
                var interactable = _ineractionResult[i].GetComponent<InteractableComponent>();
                if(interactable != null )
                {
                    interactable.Interact();
                }
            }
        }
    }
}

