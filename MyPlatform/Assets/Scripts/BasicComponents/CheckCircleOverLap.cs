using MyPlatform.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyPlatform
{
    public class CheckCircleOverLap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
     
        private readonly Collider2D[] _ineractionResult = new Collider2D[5];

        public GameObject[] GetObjectInRange()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position,_radius,_ineractionResult);//ћетод позвол€ющий пересекающий объект, но не будет выдел€ть лишнюю пам€ть

            var overlaps = new List<GameObject>();
            for(var i = 0; i < size; i++)
            {
                overlaps.Add(_ineractionResult[i].gameObject);
            }

            return overlaps.ToArray();
        }

        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position,Vector3.forward,_radius);
        }

    }
}

