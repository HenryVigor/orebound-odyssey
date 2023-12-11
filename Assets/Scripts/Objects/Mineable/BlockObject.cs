using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockObject : BaseBlockObject
{
    //
    // For generic block objects, like a basic stone wall with no drops
    //

    SpriteRenderer blockSpriteRenderer;
    public GameObject breakParticles;
    private void Start()
    {
        blockSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        currentDurability = maxDurability; // set the block's durability

        breakTextureThreshold = currentDurability / breakSpriteList.Count; // determine the threshold of damage to change to the next break sprite

        // Randomly has a chance to flip the block's sprite around; might look cool, add variation?
        // randomizeSpriteFlipsEnabled set to false in BaseBlockObject by default for now
        // flipChance set to 20% in BaseBlockObject by default for now
        randomizeSpriteFlips = true;
        if (randomizeSpriteFlips)
        {
            float randomFlipX = Random.Range(0f, 100f);
            float randomFlipY = Random.Range(0f, 100f);
            if (randomFlipX <= flipChance)
            {
                blockSpriteRenderer.flipX = true;
            }
            if (randomFlipY <= flipChance)
            {
                blockSpriteRenderer.flipY = true;
            }
        }

    }

    public override void Damage(int damageAmount)
    {

        if (isBreakable)
        {
            currentDurability -= damageAmount;
        }

        // Determine and assign block break sprite
        if (currentDurability <= 0)
        {
            ObjectDestroy();
        }
        else
        {
            int currentBreakStage = (maxDurability - currentDurability) / breakTextureThreshold;
            if (currentBreakStage > breakSpriteList.Count - 1)
            {
                blockSpriteRenderer.sprite = breakSpriteList[breakSpriteList.Count - 1];
            } else {
                blockSpriteRenderer.sprite = breakSpriteList[currentBreakStage];
            }
        }

    }

    public override void ObjectDestroy()
    {
        GameObject breakObj = Instantiate(breakParticles, transform.position, Quaternion.identity);
        ParticleSystem.MainModule main = breakObj.GetComponent<ParticleSystem>().main;
        main.startColor = (blockSpriteRenderer.color/2.25f);
        breakObj.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }

}
