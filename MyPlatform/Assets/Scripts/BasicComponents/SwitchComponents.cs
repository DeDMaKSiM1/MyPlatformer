using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyPlatform.Components
{
    public class SwitchComponents : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _state;
        [SerializeField] private string _animationKey;
        public void Switch()
        {
            _state = !_state; 
            _animator.SetBool(_animationKey, _state);
        }

        //Отладочный метод
        [ContextMenu("Switch")]
        public void SwitchIt()
        {
            Switch();
        }

    }
}

