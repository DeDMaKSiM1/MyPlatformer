using MyPlatform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace MyPlatform.Components.ColliderBased
{
    public class CheckCircleOverLap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private OnOverLapEvent _onOverLap;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private string[] _tags;
        private readonly Collider2D[] _interactionResult = new Collider2D[10];

        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }

        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _radius,
                _interactionResult,
                _mask);//����� ����������� ������������ ������, �� �� ����� �������� ������ ������
                        
            for (var i = 0; i < size; i++)
            {
                var overlapResult = _interactionResult[i];
                //����� ���������� Any ���������� true ���� ��� ��������, ���������� ����� ��� �������, ������ ���� true
                var isInTags = _tags.Any(tag => _interactionResult[i].CompareTag(tag));
                if (isInTags)
                {
                    _onOverLap?.Invoke(_interactionResult[i].gameObject);
                }

            }

        }

        [Serializable]
        public class OnOverLapEvent : UnityEvent<GameObject>
        {
        }
    }
}

            

 




            
            
   


  


