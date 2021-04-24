using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class InteractionHighlight : MonoBehaviour
    {
        private SpriteRenderer indicator;

        void Awake()
        {
            indicator = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            Hide();
        }

        public void Show()
        {
            indicator.enabled = true;
        }

        public void Hide()
        {
            indicator.enabled = false;
        }
    }
}
