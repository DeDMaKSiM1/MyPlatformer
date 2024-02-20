using UnityEngine;
using MyPlatform.Utils;

namespace MyPlatform.Components.ColliderBased
{
    public class EnterTrigger : MonoBehaviour
    {

        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private EnterEvent _action;
        private void OnTriggerEnter2D(Collider2D other)
        {

            if (!other.gameObject.IsInLayer(_layer))
            {
                //Debug.Log("�������� �� ���� �� ������");
                return;
            }
            //Debug.Log("�������� �� ���� ������");


            if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag))
            {
                //Debug.Log("�������� �� ��� �� ������");
                return;
            }
            //Debug.Log("�������� �� ��� ������");


            _action?.Invoke(other.gameObject);
        }
    }
}

