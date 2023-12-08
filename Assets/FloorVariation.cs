using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorTheme
{
    public int floorCanSpawnAt; // The level which the floor can begin to spawn at
    public GameObject cavernSpritePrefab;
    public GameObject backgroundSpritePrefab;
    public OreBlockInfo[] oreBlocks;
    //public GameObject enemyPrefab; // This should be updated to a GameObject list later on when multiple enemies are added
    // For variations in difficulty, once more enemies are added, might also turn this into several lists of enemies (easy, medium, hard) and randomize with weight based on level
    public GameObject exitBlock;
    public GameObject spikesPrefab; // Maybe same to the traps?
    public GameObject trappedBlock;
    public int width = 100; // Width of the cavern in tiles
    public int height = 100; // Height of the cavern in tiles
    public float threshold = 0.4f; // Density of cavern
    public float noOreChance = 0.95f; // Chance of no ore spawning
}

public class FloorVariation : MonoBehaviour
{

    private float healthScaling = 1.085f; // This number is used to increase health exponentially by floor: (((currentLevel^healthScaling)/100) * enemyHealth) + enemyHealth
    private float enemyHealthBonus;
    // The current floor themes are for testing and should be changed later

    public FloorTheme[] floorThemes;
    private List<FloorTheme> spawnableThemes = new List<FloorTheme>();

    // TO-DO: Need to add a system that makes floors more difficult alongside the theme; differences in enemies spawned?
    // Could also maybe make an EnemySettings and have all enemies get their stats from it and then increment every few levels or something

    public void SetFloorTheme()
    {
        var cavern = GetComponent<CavernGenerator>();
        FloorTheme currentTheme = GetFloorTheme();
        // Set settings of cavern generator to the current theme's settings
        cavern.cavernSpritePrefab = currentTheme.cavernSpritePrefab;
        cavern.backgroundSpritePrefab = currentTheme.backgroundSpritePrefab;
        cavern.oreBlocks = currentTheme.oreBlocks;
        //cavern.enemyPrefab = currentTheme.enemyPrefab;
        cavern.exitBlock = currentTheme.exitBlock;
        cavern.spikesPrefab = currentTheme.spikesPrefab;
        cavern.trappedBlock = currentTheme.trappedBlock;
        cavern.threshold = currentTheme.threshold;
        cavern.noOreChance = currentTheme.noOreChance;
        cavern.enemyHealthBonus = enemyHealthBonus;
    }

    private int GetCurrentLevel()
    {
        return GetComponent<LevelIndicator>().LevelValue;
    }

    private void UpdateSpawnableThemes(int level)
    {
        foreach (FloorTheme theme in floorThemes)
        {
            if (!spawnableThemes.Contains(theme) && theme.floorCanSpawnAt <= level)
            {
                spawnableThemes.Add(theme);
            }
        }
    }
    
    private FloorTheme GetFloorTheme()
    {
        int currentLevel = GetCurrentLevel();
        UpdateSpawnableThemes(currentLevel);

        // Select a random theme out of the possible themes until a theme is selected which can be spawned (based on floor)
        int randomThemeIndex = Random.Range(0, spawnableThemes.Count);
        FloorTheme floorTheme = spawnableThemes[randomThemeIndex];

        // Modify enemy health based on current level -- for scaling difficulty
        enemyHealthBonus = Mathf.Pow(currentLevel, healthScaling)/100;
        return floorTheme;
    }

}
