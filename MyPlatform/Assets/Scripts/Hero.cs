
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
        private void Awake()
        {
            rbody = GetComponent<Rigidbody2D>();
        }
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        } // ѕринимающий данные о Input метод

        private void FixedUpdate()
        {
            rbody.velocity = new Vector2(_direction.x * _speed, rbody.velocity.y);

            var isJumping = _direction.y > 0;
            if (isJumping)
            {
                if (IsGrounded() && rbody.velocity.y <= 0)
                {
                    rbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }
            }
            else if (rbody.velocity.y > 0)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, rbody.velocity.y * 0.5f);
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

