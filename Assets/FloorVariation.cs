using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FloorThemes are used to apply a preset to the CavernGenerator settings to create variation
[System.Serializable]
public class FloorTheme
{
    public string floorThemeName; // Name of the theme / biome - Could be used for UI?
    public int allowedSpawnFloor; // The level which the floor can begin to spawn at
    [Header("Level Generation Objects")]
    public GameObject spikesPrefab;
    public GameObject trappedBlock;
    public StoneInfo[] stoneBlocks;
    public OreBlockInfo[] oreBlocks;
    public CrateInfo[] crates;
    public EnemyInfo[] enemies;
    [Header("World Generation Settings")]
    public float threshold = 0.4f; // Adjust this threshold to control density of cavern, higher = more open, lower = more dense
    // Ore spawn settings
    [Header("Block Generation Settings")]
    public float oreChance = 0.05f; // Chance of ore spawning
    public float trapBlockChance = 0.005f;
    public Color floorBaseColor = new(255f / 255f, 255f / 255f, 255f / 255f);
    public bool colorSpikes = true; // Whether or not the spikes should have the basecolor applied (set to false for cases like the ice/magma spikes)
    [Header("Extra Generation Settings")]
    public float stairChance = 0.0012f;
    public float spikeChance = 0.02f;
    public float crateChance = 0.02075f;
    public float torchChance = 0.0209f;
}

public class FloorVariation : MonoBehaviour
{
    // Difficulty scaling variables
    private float healthScaling = 1.3f; // This number is used to increase health exponentially by floor: (((currentLevel^healthScaling)/100) * enemyHealth) + enemyHealth
    private float enemyCountScaling = 1.115f; // (((currentLevel^enemyCountScaling)/100) * 25) + 25
    private float enemyChanceScaling = 1.115f;
    private float enemyHealthBonus;
    private float newEnemyCount;
    private float newEnemyChance;

    public FloorTheme[] floorThemes;
    private List<FloorTheme> spawnableThemes = new List<FloorTheme>(); // The list of themes that can be spawned based on the current level

    // Applies the theme's settings to CavernGenerator - called before the cavern begins generation
    public void SetFloorTheme()
    {
        var cavern = GetComponent<CavernGenerator>();
        FloorTheme currentTheme = GetFloorTheme();
        // Set settings of cavern generator to the current theme's settings
        cavern.spikesPrefab = currentTheme.spikesPrefab;
        cavern.trappedBlock = currentTheme.trappedBlock;
        cavern.stoneBlocks = currentTheme.stoneBlocks;
        cavern.oreBlocks = currentTheme.oreBlocks;
        cavern.crates = currentTheme.crates;
        // Could add structures per floor too
        cavern.enemies = currentTheme.enemies;
        cavern.threshold = currentTheme.threshold;
        cavern.oreChance = currentTheme.oreChance;
        cavern.trapBlockChance = currentTheme.trapBlockChance;
        cavern.floorBaseColor = currentTheme.floorBaseColor;
        cavern.colorSpikes = currentTheme.colorSpikes;
        cavern.stairChance = currentTheme.stairChance;
        cavern.spikeChance = currentTheme.spikeChance;
        cavern.crateChance = currentTheme.crateChance;
        cavern.torchChance = currentTheme.torchChance;
        // Scaling difficulty stuff
        cavern.enemyHealthBonus = enemyHealthBonus;
        cavern.enemyMax = Mathf.CeilToInt(newEnemyCount);
        cavern.enemyChance = newEnemyChance;
    }

    private int GetCurrentLevel()
    {
        return GetComponent<LevelIndicator>().LevelValue;
    }

    // Check the current level and determine if any new themes are unlocked; add them to the spawn list
    private void UpdateSpawnableThemes(int level)
    {
        foreach (FloorTheme theme in floorThemes)
        {
            if (!spawnableThemes.Contains(theme) && theme.allowedSpawnFloor <= level)
            {
                spawnableThemes.Add(theme);
                Debug.Log("Added " + theme.floorThemeName);
            }
        }
    }
    
    // Updates the spawnable themes and then randomly selects from it, applies difficulty scaling to variables
    private FloorTheme GetFloorTheme()
    {
        int currentLevel = GetCurrentLevel();
        UpdateSpawnableThemes(currentLevel);

        // Select a random theme out of the possible themes until a theme is selected which can be spawned (based on floor)
        int randomThemeIndex = Random.Range(0, spawnableThemes.Count);
        FloorTheme floorTheme = spawnableThemes[randomThemeIndex];

        // Modify enemy health + count based on current level -- for scaling difficulty
        enemyHealthBonus = Mathf.Pow(currentLevel, healthScaling)/100;
        newEnemyCount = ((Mathf.Pow(currentLevel, enemyCountScaling) / 100) * 25) + 25;
        newEnemyChance = ((Mathf.Pow(currentLevel, enemyChanceScaling) / 100) * 0.01f) + 0.01f;
        Debug.Log("Selected " + floorTheme.floorThemeName);
        return floorTheme;
    }

}
