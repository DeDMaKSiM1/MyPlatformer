using UnityEngine;
namespace MyPlatform.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        public void Spawn()
        {
           var instantiate = Instantiate(_prefab, _target.position, Quaternion.identity);
            instantiate.transform.localScale = _target.lossyScale;
        }
    }
}

