using MyPlatform.Model.Data;
using MyPlatform.Model;
using UnityEngine;
using System.Collections.Generic;

namespace MyPlatform.Components.Collectables
{
    public class CollectorComponent : MonoBehaviour, ICanAddInInventory
    {
        [SerializeField] private List<InventoryItemData> _items = new List<InventoryItemData>();

        public void AddInInventory(string id, int value)
        {
            _items.Add(new InventoryItemData(id) { Value = value });
        }

        public void DropInInventory()
        {
            var session = FindObjectOfType<GameSession>();
            foreach (var InventoryItemData in _items) 
            {
                session.Data.Inventory.Add(InventoryItemData.Id, InventoryItemData.Value);
            }
            _items.Clear();
        }
    }
}

