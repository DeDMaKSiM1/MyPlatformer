using MyPlatform.Model.Definitions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatform.Model.Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

        public delegate void OnInventoryChanged(string id, int value);

        public OnInventoryChanged onChanged;

        public void Add(string id, int value)
        {
            if (value <= 0) return;


            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            var isFull = _inventory.Count >= DefsFacade.I.Player.InventorySize;

            if (itemDef.IsStackable)
            {
                var item = GetItem(id);
                if (item == null)
                {
                    if (isFull) return;
                    item = new InventoryItemData(id);
                    _inventory.Add(item);
                }

                item.Value += value;
            }
            else
            {
                 
                for (int i = 0; i < value; i++)
                {
                    isFull = _inventory.Count >= DefsFacade.I.Player.InventorySize;
                    if (isFull) return;
                    var item = new InventoryItemData(id) { Value = 1};
                    _inventory.Add(item);

                }
            }
            
            onChanged?.Invoke(id, Count(id));
        }

        public void Remove(string id, int value)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            if (itemDef.IsStackable)
            {
                var item = GetItem(id);
                if (item == null) return;

                item.Value -= value;

                if (item.Value <= 0)
                {
                    _inventory.Remove(item);
                }
            }
            else
            {
                for (int i = 0; i < value; i++)
                {
                    var item = GetItem(id);
                    if (item == null) return;

                    _inventory.Remove(item);
                }
            }

            
            onChanged?.Invoke(id, Count(id));

        }
        public int Count(string id)
        {
            var count = 0;
            foreach (var item in _inventory)
            {
                if (item.Id == id)
                    count += item.Value;
            }
            return count;
        }

        private InventoryItemData GetItem(string id)
        {
            foreach (var itemData in _inventory)
            {
                if (itemData.Id == id) return itemData;
            }
            return null;
        }
    }

    [Serializable]
    public class InventoryItemData
    {
        [InventoryIdAtribute]
        public string Id;
        public int Value;

        public InventoryItemData(string id)
        {
            Id = id;
        }
    }
}

