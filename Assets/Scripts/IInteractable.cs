using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{    
     public Sprite InteractionPrompt { get; }
     public bool Interact(Interactor interactor);
}
