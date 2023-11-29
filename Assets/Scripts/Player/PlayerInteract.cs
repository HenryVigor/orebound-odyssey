using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInteract : MonoBehaviour
{

    PlayerInput pi;
    [SerializeField] private LayerMask usableLayers;

    public bool canInteract = true;
    public float interactCooldown = 0.25f;
    public Transform interactPoint;
    public Vector2 interactArea = new Vector2(0.65f, .5f);

    private void Awake()
    {
        pi = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (pi.actions["Interact"].IsPressed()) Interact();
    }

    private void Interact()
    {
        if (canInteract)
        {

            canInteract = false;

            Collider2D[] hitEntities = Physics2D.OverlapBoxAll(interactPoint.position, interactArea, 0f, usableLayers);

            foreach(Collider2D entity in hitEntities)
            {
                entity.GetComponent<IUsable>().Use();
            }

            Invoke("ResetInteract", interactCooldown);

        }
    }

    private void ResetInteract()
    {
        canInteract = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactPoint == null)
        {
            return;
        }

        Gizmos.DrawWireCube(interactPoint.position, interactArea);
    }

}
