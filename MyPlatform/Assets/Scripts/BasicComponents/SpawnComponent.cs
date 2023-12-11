using UnityEngine;
namespace MyPlatform.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        public void Spawn()
        {
            Instantiate(_prefab, _target.position, Quaternion.identity);
        }
    }
}

