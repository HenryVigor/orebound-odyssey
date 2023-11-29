using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OreBlockInfo
{
    public GameObject oreBlockPrefab; // Reference to the ore block prefab
    public float rarity; // Rarity of the ore block (higher values are more common, less rare)
}

public class CavernGenerator : MonoBehaviour
{
    public GameObject cavernSpritePrefab; // Reference to your cavern sprite prefab
    public GameObject backgroundSpritePrefab; // Reference to your background sprite prefab
    public OreBlockInfo[] oreBlocks; // Array of ore block information
    public GameObject enemyPrefab; // Reference to the enemy 
    public GameObject exitBlock;
    public GameObject blockHolder;
    public GameObject spikesPrefab;
    public GameObject trappedBlock;

    public int width = 100; // Width of the cavern in tiles
    public int height = 100; // Height of the cavern in tiles
    public float threshold = 0.4f; // Adjust this threshold to control density of cavern
    public float noOreChance = 0.2f; // Chance of no ore spawning

    private float noiseScale; // Random float that will be between 6f and 13f when caves are generated.
    private int enemyCount = 0;
    private int playerSpawned = 0;
    private int exitSpawned = 0;

    void Start()
    {
<<<<<<< Updated upstream

=======
        cavernSpritePrefab.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
>>>>>>> Stashed changes
        GenerateCavern();
    }

    public void GenerateCavern()
    {
        noiseScale = Random.Range(6f, 13f);
        playerSpawned = 0;

        // Generate background sprite
        GameObject backgroundSprite = Instantiate(backgroundSpritePrefab, transform.position, Quaternion.identity);
        backgroundSprite.transform.localScale = new Vector3(width, height, 1);

        // Place background behind normal sprites
        backgroundSprite.transform.position = new Vector3(width / 2f - 0.5f, height / 2f - 0.5f, 0);
        backgroundSprite.GetComponent<SpriteRenderer>().sortingOrder = -1;

        // Generate background sprite
        blockHolder = new GameObject("BlockHolder");

        // Calculate center of the cavern for player
        Vector3 cavernCenter = new Vector3(width / 2f, height / 2f, 0);
        // Move the player to the center of the cavern
        //Player.Obj.transform.position = new Vector3(cavernCenter.x, cavernCenter.y, Player.Obj.transform.position.z);


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float perlinValue = Mathf.PerlinNoise(x / noiseScale, y / noiseScale);

                if (perlinValue > threshold)
                {
                    Vector3 position = new Vector3(x, y, 0);
                    GameObject stoneBlock = Instantiate(cavernSpritePrefab, position, Quaternion.identity, blockHolder.transform);

                    // Randomly determine if no ore should spawn
                    if (Random.value > noOreChance && !Physics2D.OverlapPoint(position, LayerMask.GetMask("Background")))
                    {
                        float totalRarity = 0f;
                        foreach (OreBlockInfo oreBlockInfo in oreBlocks)
                        {
                            totalRarity += oreBlockInfo.rarity;
                        }

                        float randomValue = Random.Range(0f, totalRarity);
                        float currentRarity = 0f;

                        foreach (OreBlockInfo oreBlockInfo in oreBlocks)
                        {
                            currentRarity += oreBlockInfo.rarity;
                            if (randomValue <= currentRarity)
                            {
                                GameObject oreBlockSprite = Instantiate(oreBlockInfo.oreBlockPrefab, position, Quaternion.identity, blockHolder.transform);
                                break;
                            }
                        }
                    }
                    else if (Random.value > 0.995)
                    {
                        Instantiate(trappedBlock, position, Quaternion.identity, blockHolder.transform);
                    }
                }
                else
                {
                    if (x > width / 2 - 12 && x < width / 2 + 12 && y > height / 2 - 12 && y < height / 2 + 12)
                    {
                        Vector3 position = new Vector3(x, y, Player.Obj.transform.position.z);


                        if (playerSpawned == 0)
                        {
                            // Spawn an enemy at spots where no ore spawns
                            Player.Obj.transform.position = new Vector3(position.x, position.y, Player.Obj.transform.position.z);
                            playerSpawned++;
                            Debug.Log("Spawned Player at " + position);
                        }
                    }
                    if (!(x > width / 2 - 20 && x < width / 2 + 20 && y > height / 2 - 20 && y < height / 2 + 20))
                    {
                        Vector3 position = new Vector3(x, y, enemyPrefab.transform.position.x);
                        // Generate a random value between 0 and 1
                        float randomValue = Random.value;

                        // Check if the random value is less than or equal to 0.05 (5% chance)
                        if (randomValue <= 0.01f)
                        {
                            // Spawn an enemy at spots where no ore spawns
                            Instantiate(enemyPrefab, position, Quaternion.identity, blockHolder.transform);
                            enemyCount++;
                            //Debug.Log("Spawned Enemy at " + position);
                        }
                        else if (randomValue > 0.01f && randomValue <= 0.02f)
                        {
                            // Spawn spikes
                            Instantiate(spikesPrefab, new(position.x, position.y, 0), Quaternion.identity, blockHolder.transform);
                        }
                        if (randomValue <= 0.001f && exitSpawned == 0)
                        {
                            // Spawn an enemy at spots where no ore spawns
                            Instantiate(exitBlock, position, Quaternion.identity, blockHolder.transform);
                            exitSpawned++;
                        }
                    }

                }
            }
        }
        Debug.Log("Number of Enemies: " + enemyCount);
    }

}


//----------

