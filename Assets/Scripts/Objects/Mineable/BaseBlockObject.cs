using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBlockObject : MonoBehaviour, IDamageable
{

    //
    // Abstract class that serves a base for all mineable block objects to build on
    //

    // Default Block Stats
    [Header("Block Durability Settings")]
    [SerializeField] protected bool isBreakable = true; // if a block should be damageable (can be disabled if indestructible blocks are wanted e.g. borders of levels?)
    [SerializeField] protected int maxDurability = 100; // The starting durability (health) of a block
    protected int currentDurability;

    // Block Sprite Variables
    [Header("Block Sprite Settings")]
    [SerializeField] protected bool randomizeSpriteFlips = false;
    [SerializeField][Range(0f, 100f)] protected float flipChance = 20f;
    [SerializeField] protected List<Sprite> breakSpriteList = new List<Sprite>(); // A list of the sprites used as the block is broken
    protected int breakStage = 0; // The current break sprite being used for the block (from breakSpriteList)
    protected int breakTextureThreshold; // The amount of damage required to display the next break sprite in breakSpriteList

    // May add further animation/particle stuff here

    // Interface Methods
    public abstract void Damage(int damageAmount);

    public abstract void ObjectDestroy();

}
