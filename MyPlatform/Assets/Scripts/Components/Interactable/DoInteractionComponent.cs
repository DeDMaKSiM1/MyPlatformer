using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatform.Components.Interactable
{
    public class DoInteractionComponent : MonoBehaviour
    {
        public void DoInteraction(GameObject go)
        {
            var interactable = go.GetComponent<InteractableComponent>();
            if (interactable != null)
                interactable.Interact();
        }
    }
}

