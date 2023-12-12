using UnityEngine;
using UnityEngine.InputSystem;
namespace MyPlatform
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        private void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }
        private void OnClicked(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Saying();
            }

        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Interact();
            }
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Attack();
            }
        }
    }
}

