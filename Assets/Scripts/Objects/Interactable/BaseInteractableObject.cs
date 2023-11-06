using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractableObject : MonoBehaviour, IUsable
{

    //
    // Abstract class that serves a base for all interactble objects like boxes, levers, etc.
    //

    [Header("Interaction Settings")]
    [SerializeField] protected bool isInteractable = true;
    [SerializeField] protected float interactCooldown = 0.5f;
    [SerializeField] protected bool limitInteractions = false;
    [SerializeField] protected int maxInteractions = 1;
    [SerializeField] protected int interactState = 0;
    protected int timesInteracted = 0;

    public abstract void Use();
    public abstract void Interaction();

}
