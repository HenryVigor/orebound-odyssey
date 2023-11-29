using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractableObject : MonoBehaviour, IUsable
{

    //
    // Abstract class that serves a base for all interactble objects like boxes, levers, etc.
    //

    [Header("Object Information")]
    [SerializeField] protected string itemName = "Object Name"; // Name of item or object being interacted with
    [SerializeField] protected string itemText = "Object Description"; // Description or subtext of item being interacted with
    [SerializeField] protected string itemAction = "Object Action"; // The action to display next to [E], interact text (e.g. Pickup, Interact, Open, etc.)

    [Header("Interaction Settings")]
    [SerializeField] protected bool isInteractable = true;
    [SerializeField] protected float interactCooldown = 0.5f;
    [SerializeField] protected bool limitInteractions = true;
    [SerializeField] protected int maxInteractions = 1;
    [SerializeField] protected int interactState = 0;
    protected int timesInteracted = 0;

    public abstract string GetInteractName();
    public abstract string GetInteractDesc();
    public abstract string GetInteractAction();
    public abstract void Use();
    public abstract void Interaction();

}
