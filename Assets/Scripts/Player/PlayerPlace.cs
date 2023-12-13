using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerPlace : MonoBehaviour
{
    [Header("Torch Place Settings")]
    // Torch prefab
    public GameObject torchPrefab;
    public bool canPlace = true;
    public float placeCooldown = 0.5f;

    // How much coal needed for 1 torch
    [SerializeField] int torchCost = 1;

    // Get player inventory
    private InventorySystem inventorySystem;

    // Get player input
    PlayerInput pi;

    // Get block holder
    private GameObject blockHolder;

    

    void Start()
    {
        blockHolder = GameObject.Find("BlockHolder");
    }

    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        inventorySystem = FindObjectOfType<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pi.actions["Place"].IsPressed()) Place();
    }

    void Place() 
    {
        if (canPlace && inventorySystem.CoalValue >= 1) // If player has coal
        {
            canPlace = false;

            // Get player position
            Vector3 placePosition = transform.position;

            // Place at player
            blockHolder = GameObject.Find("BlockHolder");
            GameObject newTorch = Instantiate(torchPrefab, placePosition, Quaternion.identity);
            newTorch.transform.SetParent(blockHolder.transform);

            // Reset canPlace
            Invoke("ResetPlace", placeCooldown);
            // Remove coal from inventory
            RemoveCoal();
        }
    }

    private void ResetPlace()
    {
        canPlace = true;
    }

    private void RemoveCoal()
    {   
        inventorySystem.CoalValue -= torchCost;
    }
}
