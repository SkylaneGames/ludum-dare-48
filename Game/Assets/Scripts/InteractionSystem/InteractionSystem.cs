using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractionSystem : MonoBehaviour
    {
        // Use this to disable highlights when NPCs interact with objects.
        public bool UseHighlights = false;

        public IInteractable LastInteractable = null;

        public bool IsInteracting { get; private set; } = false;

        public CharacterController Controller { get; private set; }

        void Awake()
        {
            Controller = GetComponentInParent<CharacterController>();
        }

        public void Interact()
        {
            if (LastInteractable != null)
            {
                IsInteracting = true;
                LastInteractable.Interact(Controller, () => IsInteracting = false);
                LastInteractable = null;
            }
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            var interactable = collider.GetComponent<IInteractable>();

            if (interactable != null && interactable != LastInteractable && interactable.CanInteract(Controller))
            {
                if (LastInteractable != null)
                {
                    // Get the closest interactable and if it's the new one, replace the current interactable object with it.
                    var currentDistance = (transform.position - LastInteractable.Transform.position).sqrMagnitude;
                    var newDistance = (transform.position - interactable.Transform.position).sqrMagnitude;

                    if (newDistance < currentDistance)
                    {
                        LastInteractable.RemoveHighlight();
                        LastInteractable = interactable;

                        if (UseHighlights)
                        {
                            interactable.Highlight();
                        }
                    }
                }
                else
                {
                    if (UseHighlights)
                    {
                        interactable.Highlight();
                    }

                    LastInteractable = interactable;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            var interactable = collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (LastInteractable == interactable)
                {
                    if (UseHighlights)
                    {
                        interactable.RemoveHighlight();
                    }
                    LastInteractable = null;
                }
            }
        }
    }
}