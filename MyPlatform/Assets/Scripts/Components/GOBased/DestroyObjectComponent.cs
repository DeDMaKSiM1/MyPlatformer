using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyPlatform.Components.GOBased
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        public void DestroyObject()
        {
            Destroy(_objectToDestroy);
        }

        public void HeroLayerIgnoring()
        {
            gameObject.layer = 3;
        }

    }

}
