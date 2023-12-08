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
    // Level generation objects
    public GameObject cavernSpritePrefab; // Reference to your cavern sprite prefab
    public GameObject backgroundSpritePrefab; // Reference to your background sprite prefab
    public GameObject exitBlock;
    public GameObject blockHolder;
    public OreBlockInfo[] oreBlocks; // Array of ore block information

    // Enemy spawning objects
    public GameObject[] enemyPrefabs; // List of easy enemies

    // Trap objects
    public GameObject spikesPrefab;
    public GameObject trappedBlock;

    // Loot objects
    [SerializeField] List<GameObject> lootBoxes = new List<GameObject>();

    // Level generation settings
    public int width = 100; // Width of the cavern in tiles
    public int height = 100; // Height of the cavern in tiles
    public float threshold = 0.4f; // Adjust this threshold to control density of cavern
    private float noiseScale; // Random float that will be between 6f and 13f when caves are generated.

    public float noOreChance = 0.2f; // Chance of no ore spawning
    public float trapBlockChance = 0.005f;

    public int enemyMax = 25;
    public float enemyChance = 0.01f; // Chance for enemy to spawn in empty space
    public float medEnemyChance = 1f; // 0-100% chance for a spawned enemy to be medium difficulty
    public float hardEnemyChance = 0f; // 0-100% chance for a spawned enemy to be hard difficulty
    public float enemyHealthBonus = 0f;

    public float spikeChance = 0.02f;
    public float crateChance = 0.02075f;
    public float stairChance = 0.001f;

    // Settings for tracking generation
    private int enemyCount = 0;
    private int playerSpawned = 0;
    private int exitSpawned = 0;

    [SerializeField] private LayerMask mineableLayers;

    void Start()
    {
        cavernSpritePrefab.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        GenerateCavern();
    }

    public void GenerateCavern()
    {
        // Get the floor theme and change the settings to match
        GetComponent<FloorVariation>().SetFloorTheme();

        noiseScale = Random.Range(6f, 13f);
        playerSpawned = 0;
        enemyCount = 0;
        exitSpawned = 0;

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
                    else
                    {
                        Instantiate(cavernSpritePrefab, position, Quaternion.identity, blockHolder.transform);
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
                        Vector3 position = new Vector3(x, y, Player.Obj.transform.position.z);
                        // Generate a random value between 0 and 1
                        float randomValue = Random.value;


                        if (randomValue <= enemyChance)
                        {
                            if (enemyCount < enemyMax) // Spawn an enemy at spots where no ore spawns (if max enemy count not reached)
                            {
                                int enemyChoice = Random.Range(0, enemyPrefabs.Length); // Randomly select enemy from chosen list
                                GameObject enemy = Instantiate(enemyPrefabs[enemyChoice], position, Quaternion.identity, blockHolder.transform);
                                enemy.GetComponent<BasicEnemy>().SetHealthBonus(enemyHealthBonus);
                                enemyCount++;
                            }
                        }
                        else if (randomValue <= spikeChance)
                        {
                            // Spawn spikes
                            Instantiate(spikesPrefab, new(position.x, position.y, 0), Quaternion.identity, blockHolder.transform);
                        }
                        else if (randomValue <= crateChance)
                        {
                            // Spawn box
                            int randChoice = Random.Range(0, lootBoxes.Count);
                            GameObject boxToSpawn = lootBoxes[randChoice];
                            Instantiate(boxToSpawn, position, Quaternion.identity, blockHolder.transform);
                        }
                        else if (randomValue <= stairChance && exitSpawned == 0)
                        {
                            // Spawn exit
                            Instantiate(exitBlock, position, Quaternion.identity, blockHolder.transform);
                            exitSpawned++;
                            Player.Obj.transform.Find("HUD").Find("MapText").GetComponent<ExitIndicator>().TrackNewItem(position);
                        }

                    }

                }
            }
        }

        // Makes sure that at least one set of stairs gets spawned
        if (exitSpawned == 0)
        {
            Vector3 stairPos = new Vector3(Random.Range(1f, width / 2), Random.Range(1f, height / 2), Player.Obj.transform.position.z);
            Instantiate(exitBlock, stairPos, Quaternion.identity, blockHolder.transform);
            exitSpawned++;
            Player.Obj.transform.Find("HUD").Find("MapText").GetComponent<ExitIndicator>().TrackNewItem(stairPos);
        }

        Debug.Log("Number of Enemies: " + enemyCount);
    }

}


//----------

