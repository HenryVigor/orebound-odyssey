using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerPlace : MonoBehaviour
{
    // Torch prefab
    public GameObject torchPrefab;

    // Get player inventory
    private InventorySystem inventorySystem;

    // Get player input
    PlayerInput pi;

    // Get block holder
    private GameObject blockHolder;

    [Header("Torch Place Settings")]
    public bool canPlace = true;
    public float placeCooldown = 0.5f;

    // How much coal needed for 1 torch
    [SerializeField] int torchCost = 1;

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
        if (canPlace && inventorySystem.CoalValue >= 1)
        {
            canPlace = false;

            // Get player position
            Vector3 placePosition = transform.position;

            // Place at player
            GameObject newTorch = Instantiate(torchPrefab, placePosition, Quaternion.identity);
            newTorch.transform.SetParent(blockHolder.transform);

            Invoke("ResetPlace", placeCooldown);
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
