
using UnityEngine;
namespace MyPlatform
{
    public class Hero : MonoBehaviour
    {
        //Ќужно воплотить вертикальное передвижение, без физики в одном методе
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;

        [SerializeField] private LayerCheck _groundCheck;

        Vector2 _direction;
        private Rigidbody2D rbody;
        private Animator _animator;

        private static readonly int isGroundKey = Animator.StringToHash("is-ground");
        private static readonly int isRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");

        private SpriteRenderer _sprite;

        private void Awake()
        {
            rbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        } // ѕринимающий данные о Input метод

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
    }
}

