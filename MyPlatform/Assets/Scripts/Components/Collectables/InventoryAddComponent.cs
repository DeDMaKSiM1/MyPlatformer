using MyPlatform.Creatures.Hero;
using UnityEngine;

namespace MyPlatform.Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private int _count;

        public void Add(GameObject go)
        {
            var _hero = go.GetComponent<Hero>();
            if (_hero != null)
                _hero.AddInInventory(_id, _count);
        }

    }   
}

