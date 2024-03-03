using UnityEngine;
using UnityEngine.InputSystem;
using MyPlatform.Creatures;
namespace MyPlatform.Creatures.Hero
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        private void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }
        //private void OnClicked(InputAction.CallbackContext context)
        //{


        //}

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


        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.started)// performed - когда действие совершиться(мы нажали кнопку), а не когда клавиша вернулась в исх состояние
            {
                _hero.StartThrowing();
            }
            if (context.canceled)
            {
                _hero.ThrowingComplete();
            }
        }


    }
}

