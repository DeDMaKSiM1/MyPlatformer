using MyPlatform.Creatures.Hero;
using UnityEngine;
using MyPlatform.Model.Definitions;
namespace MyPlatform.Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryIdAtribute][SerializeField] private string _id;
        [SerializeField] private int _count;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
                hero.AddInInventory(_id, _count);
        }

    }   
}

