using System;
using UnityEngine;

namespace Interaction
{
    public interface IInteractable
    {
        string Name { get; }
        Transform Transform { get; }
        InteractionHighlight HighlightObject { get; }
        void Interact(CharacterController interacter, Action callback = null);
        bool CanInteract(CharacterController interacter);
        void Highlight();
        void RemoveHighlight();
    }
}