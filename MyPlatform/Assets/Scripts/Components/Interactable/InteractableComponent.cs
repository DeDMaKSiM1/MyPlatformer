using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyPlatform.Components.Interactable
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;


        public  void Interact()
        {
            _action?.Invoke();//������ ���� Interact ����� ������ �� �������, �������� �� �����
        }
    }
}

