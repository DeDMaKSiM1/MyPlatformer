using MyPlatform.Model.Definitions;
using MyPlatform.Model;
using MyPlatform.Model.Data;
using UnityEngine;
using UnityEngine.Events;

namespace MyPlatform.Components.Interactable
{
    public class RequireItemComponent : MonoBehaviour
    {
        [SerializeField] private InventoryItemData[] _required;
        [SerializeField] private bool _removeAfterUse;

        [SerializeField] private UnityEvent _OnSucces;
        [SerializeField] private UnityEvent _OnFail;


        public void Check()
        {
            var session = FindObjectOfType<GameSession>();
            var areAllRequirementsMet = true;
            foreach (var item in _required)
            {
                var numItems = session.Data.Inventory.Count(item.Id); 
                if (numItems < item.Value)
                    areAllRequirementsMet = false;
            }

            if (areAllRequirementsMet)
            {
                if (_removeAfterUse)
                {
                    foreach (var item in _required)
                        session.Data.Inventory.Remove(item.Id, item.Value); 
                }

                _OnSucces?.Invoke();
            }
            else
            {
                _OnFail?.Invoke();
            }

        }
    }
}

