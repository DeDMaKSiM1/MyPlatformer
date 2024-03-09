using MyPlatform.Model.Data;
using UnityEngine;
using MyPlatform.Model.Definitions;
using MyPlatform.Utils;

namespace MyPlatform.Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryIdAtribute]
        [SerializeField] private string _id;
        [SerializeField] private int _count;

        public void Add(GameObject go)
        {
            var hero = go.GetInterface<ICanAddInInventory>();
            if (hero != null)
                hero.AddInInventory(_id, _count);
        }

    }   
}

