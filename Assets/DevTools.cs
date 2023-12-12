using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DevTools : MonoBehaviour
{
    private float addCooldown = 0.3f;
    private bool addOnCooldown = false;
    PlayerInput pi;
    private void Awake()
    {
        pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (pi.actions["RegenLevel"].IsPressed()) RegenLevel();
        if (pi.actions["TPStairs"].IsPressed()) TPStairs();
        if (pi.actions["TPCrate"].IsPressed()) TPCrate();
        if (pi.actions["AddStats"].IsPressed()) AddStats();
        if (pi.actions["AddHealth"].IsPressed()) AddHealth();
        if (pi.actions["SubHealth"].IsPressed()) SubHealth();
    }

    private void RegenLevel()
    {
        Destroy(GameObject.Find("BlockHolder"));
        gameObject.GetComponent<CavernGenerator>().GenerateCavern();
    }
    private void TPStairs()
    {
        GameObject stair = GameObject.Find("BlockHolder").transform.Find("Stairs(Clone)").gameObject;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(stair.transform.position.x, stair.transform.position.y - 2f, player.transform.position.z);
    }

    private void TPCrate()
    {
        if (GameObject.Find("BlockHolder").transform.Find("WoodenCrate(Clone)") != null)
        {
            GameObject crate = GameObject.Find("BlockHolder").transform.Find("WoodenCrate(Clone)").gameObject;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = new Vector3(crate.transform.position.x, crate.transform.position.y - 2f, player.transform.position.z);
        }
        else if (GameObject.Find("BlockHolder").transform.Find("MetalCrate(Clone)") != null)
        {
            GameObject crate2 = GameObject.Find("BlockHolder").transform.Find("MetalCrate(Clone)").gameObject;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = new Vector3(crate2.transform.position.x, crate2.transform.position.y - 2f, player.transform.position.z);
        }
        else if (GameObject.Find("BlockHolder").transform.Find("DwarvenCrate(Clone)") != null)
        {
            GameObject crate3 = GameObject.Find("BlockHolder").transform.Find("DwarvenCrate(Clone)").gameObject;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = new Vector3(crate3.transform.position.x, crate3.transform.position.y - 2f, player.transform.position.z);
        }
        else
        {
            Debug.Log("No crates available to TP!");
        }
    }

    private void AddStats()
    {
        if (!addOnCooldown)
        {
            var inv = GameObject.FindGameObjectWithTag("Player").transform.Find("HUD").Find("Inventory").GetComponent<InventorySystem>();
            inv.CoalValue += 2;
            inv.IronValue += 2;
            inv.GoldValue += 2;
            inv.CrystalValue += 2;
            inv.CopperValue += 2;
            inv.CoinsValue += 2;
            addOnCooldown = true;
            Invoke("ResetCooldown", addCooldown);
        }
    }

    private void AddHealth()
    {
        if (!addOnCooldown)
        {
            PlayerCombat.Heal(1);
            addOnCooldown = true;
            Invoke("ResetCooldown", addCooldown);
        }
    }

    private void SubHealth()
    {
        if (!addOnCooldown)
        {
            PlayerCombat.Hit(1, "Zombie");
            addOnCooldown = true;
            Invoke("ResetCooldown", addCooldown);
        }
    }

    private void ResetCooldown()
    {
        addOnCooldown = false;
    }

}
