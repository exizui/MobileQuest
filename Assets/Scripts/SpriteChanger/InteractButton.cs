using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    private IInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<IInteractable>();
    }

    public void OnClick()
    {
        interactable?.Interact();
    }
}
