using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int numFound;
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private IInteractable interactable;
    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
            _interactableMask);
        if (numFound > 0)
        {
             interactable = _colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!interactionPromptUI.isDisplayed)
                {
                    interactionPromptUI.SetUp(interactable.InteractionPrompt);
                }

                if (Keyboard.current.fKey.wasPressedThisFrame)
                {
                    interactable.Interact(this);
                }
            }
        }
        else
        {
            if (interactable != null)
            {
                interactable = null;
            }

            if (interactionPromptUI.isDisplayed)
            {
                interactionPromptUI.Close();
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
