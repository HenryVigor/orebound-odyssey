using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OreBlockInfo
{
    public GameObject oreBlockPrefab; // Reference to the ore block prefab
    public float rarity; // Rarity of the ore block (higher values are more common, less rare)
}
[System.Serializable]
public class CrateInfo
{
    public GameObject cratePrefab; // Reference to crate prefab
    public float rarity; // For rarity based spawning
}
[System.Serializable]
public class EnemyInfo
{
    public GameObject enemyPrefab; // Reference to enemy prefab
    public float rarity; // For rarity based spawning
}
[System.Serializable]
public class StoneInfo
{
    public GameObject stonePrefab; // Reference to stone prefab - allows for variants
    public float rarity; // For rarity based spawning
}

public class CavernGenerator : MonoBehaviour
{
    [Header("NOTE: These get overwritten by FloorVariation themes!")]
    ///// Level Generation Objects /////
    [Header("Level Generation Objects")]
    public GameObject cavernSpritePrefab; // Reference to your cavern sprite prefab
    public GameObject backgroundSpritePrefab; // Reference to your background sprite prefab
    public GameObject blockHolder;
    public GameObject exitBlock;
    public GameObject spikesPrefab;
    public GameObject trappedBlock;
    public GameObject torchPrefab;
    public GameObject[] structures;
    public StoneInfo[] stoneBlocks;
    public OreBlockInfo[] oreBlocks; // Array of ore block information
    public CrateInfo[] crates;
    public EnemyInfo[] enemies;

    ///// Level Generation Settings /////
    private int width = 100; // Width of the cavern in tiles
    private int height = 100; // Height of the cavern in tiles
    private List<Vector3> structurePos;
    [Header("World Generation Settings")]
    public float threshold = 0.42f; // Adjust this threshold to control density of cavern
    private float noiseScale; // Random float that will be between 6f and 13f when caves are generated.
    // Ore spawn settings
    [Header("Block Generation Settings")]
    public float oreChance = 0.2f; // Chance of ore spawning
    public float trapBlockChance = 0.005f;
    public Color floorBaseColor = new(255f / 255f, 255f / 255f, 255f / 255f);
    public bool colorSpikes = true;
    [Header("Extra Generation Settings")]
    public float structureChance = 0.0005f;
    public float stairChance = 0.0012f;
    public float spikeChance = 0.02f;
    public float crateChance = 0.02075f;
    public float torchChance = 0.0209f;
    // Enemy spawn settings --------------- These are pretty much all hard-coded / set in FloorVariation
    [Header("Enemy Spawn Placeholder Variables - Actually set in FloorVariation")]
    public int enemyMax = 25;
    public float enemyChance = 0.01f; // Chance for enemy to spawn in empty space
    public float enemyHealthBonus = 0f;
    // Settings for tracking generation
    private int enemyCount = 0;
    private int playerSpawned = 0;
    private int exitSpawned = 0;

    void Start()
    {
        //cavernSpritePrefab.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        GenerateCavern();
    }

    public void GenerateCavern()
    {
        // Get the floor theme and change the settings to match
        GetComponent<FloorVariation>().SetFloorTheme();
        playerSpawned = 0;
        enemyCount = 0;
        exitSpawned = 0;
        noiseScale = Random.Range(6f, 13f);
        structurePos = new List<Vector3>(); // Holds positions to spawn structures after placing all blocks/extras

        blockHolder = new GameObject("BlockHolder");

        // Random Color Variation
        float randomColorVar = Random.Range(-25, 25) / 255f;
        floorBaseColor = new(Mathf.Clamp((floorBaseColor.r + randomColorVar), 0, 1)
            , Mathf.Clamp((floorBaseColor.g + randomColorVar), 0, 1)
            , Mathf.Clamp((floorBaseColor.b + randomColorVar), 0, 1));
        
        // Apply color to spikes (if enabled) and trapped block
        if (colorSpikes)
        {
            spikesPrefab.GetComponent<SpriteRenderer>().color = floorBaseColor;
        }
        trappedBlock.GetComponent<SpriteRenderer>().color = floorBaseColor;

        // Generate background sprite
        GameObject backgroundSprite = Instantiate(backgroundSpritePrefab, transform.position, Quaternion.identity, blockHolder.transform);
        backgroundSprite.GetComponent<SpriteRenderer>().color = floorBaseColor;
        backgroundSprite.transform.localScale = new Vector3(width, height, 1);

        // Place background behind normal sprites
        backgroundSprite.transform.position = new Vector3(width / 2f - 0.5f, height / 2f - 0.5f, 0);
        backgroundSprite.GetComponent<SpriteRenderer>().sortingOrder = -1;

        // Calculate spawn weights for spawnable items and set colors
        float totalEnemyRarity = 0f;
        foreach (EnemyInfo enemyInfo in enemies)
        {
            totalEnemyRarity += enemyInfo.rarity;
        }

        float totalCrateRarity = 0f;
        foreach (CrateInfo crateInfo in crates)
        {
            totalCrateRarity += crateInfo.rarity;
        }

        float totalRarity = 0f;
        foreach (OreBlockInfo oreBlockInfo in oreBlocks)
        {
            totalRarity += oreBlockInfo.rarity;
            oreBlockInfo.oreBlockPrefab.GetComponent<SpriteRenderer>().color = floorBaseColor;
        }

        float totalStoneRarity = 0f;
        foreach (StoneInfo stoneInfo in stoneBlocks)
        {
            totalStoneRarity += stoneInfo.rarity;
            stoneInfo.stonePrefab.GetComponent<SpriteRenderer>().color = floorBaseColor;
        }

        // Begin generation
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float perlinValue = Mathf.PerlinNoise(x / noiseScale, y / noiseScale);

                if (perlinValue > threshold)
                {
                    Vector3 position = new Vector3(x, y, 0);

                    // Randomly determine if no ore should spawn
                    if (Random.value <= oreChance && !Physics2D.OverlapPoint(position, LayerMask.GetMask("Background")))
                    {
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
                    else if (Random.value <= trapBlockChance)
                    {
                        Instantiate(trappedBlock, position, Quaternion.identity, blockHolder.transform);
                    }
                    else
                    {
                        float randomValue = Random.Range(0f, totalStoneRarity);
                        float currentStoneRarity = 0f;
                        foreach (StoneInfo stoneInfo in stoneBlocks)
                        {
                            currentStoneRarity += stoneInfo.rarity;
                            if (randomValue <= currentStoneRarity)
                            {
                                GameObject stoneBlock = Instantiate(stoneInfo.stonePrefab, position, Quaternion.identity, blockHolder.transform);
                                break;
                            }
                        }
                        //Instantiate(cavernSpritePrefab, position, Quaternion.identity, blockHolder.transform);
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
                        if (randomValue <= structureChance)
                        {
                            structurePos.Add(position); // Adds the location of the structure, spawns it later (need all blocks to be spawned to properly remove area)
                        }
                        else if (randomValue <= stairChance && exitSpawned == 0)
                        {
                            // Spawn exit
                            Instantiate(exitBlock, position, Quaternion.identity, blockHolder.transform);
                            exitSpawned++;
                            Player.Obj.transform.Find("HUD").Find("MapText").GetComponent<ExitIndicator>().TrackNewItem(position);
                        }
                        else if (randomValue <= enemyChance)
                        {
                            if (enemyCount < enemyMax) // Spawn an enemy at spots where no ore spawns (if max enemy count not reached)
                            {
                                float randomEnemyValue = Random.Range(0f, totalEnemyRarity);
                                float currentEnemyRarity = 0f;
                                foreach (EnemyInfo enemyInfo in enemies)
                                {
                                    currentEnemyRarity += enemyInfo.rarity;
                                    if (randomEnemyValue <= currentEnemyRarity)
                                    {
                                        GameObject enemySpawn = Instantiate(enemyInfo.enemyPrefab, position, Quaternion.identity, blockHolder.transform);
                                        enemySpawn.GetComponent<BasicEnemy>().SetHealthBonus(enemyHealthBonus);
                                        enemyCount++;
                                        break;
                                    }
                                }
                            }
                        }
                        else if (randomValue <= spikeChance)
                        {
                            // Spawn spikes
                            Instantiate(spikesPrefab, new(position.x, position.y, 0), Quaternion.identity, blockHolder.transform);
                        }
                        else if (randomValue <= crateChance)
                        {
                            float randomCrateValue = Random.Range(0f, totalCrateRarity);
                            float currentCrateRarity = 0f;
                            foreach (CrateInfo crateInfo in crates)
                            {
                                currentCrateRarity += crateInfo.rarity;
                                if (randomCrateValue <= currentCrateRarity)
                                {
                                    GameObject crateSpawn = Instantiate(crateInfo.cratePrefab, position, Quaternion.identity, blockHolder.transform);
                                    break;
                                }
                            }
                        }
                        else if (randomValue <= torchChance)
                        {
                            // Random generation torch placement (rare)
                            Instantiate(torchPrefab, position, Quaternion.identity, blockHolder.transform);
                        }
                    }

                }
            }
        }

        // Spawn structures at the saved positions in structurepos, clear out an area of blocks around them
        foreach(Vector3 pos in structurePos)
        {
            int structChoice = Random.Range(0, structures.Length);
            GameObject structure = Instantiate(structures[structChoice], pos, Quaternion.identity, blockHolder.transform);
            Collider2D[] blocksAround = Physics2D.OverlapBoxAll(pos, new Vector2(5f, 5f), 0f, LayerMask.GetMask("Mineables"));
            foreach (Collider2D block in blocksAround)
            {
                if (block.transform.parent != structure.transform)
                {
                    Destroy(block.gameObject);
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

