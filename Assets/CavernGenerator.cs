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

    public int width = 100; // Width of the cavern in tiles
    public int height = 100; // Height of the cavern in tiles
    public float noiseScale = 10f; // Adjust the scale of the noise texture
    public float threshold = 0.4f; // Adjust this threshold to control density of cavern
    public float noOreChance = 0.2f; // Chance of no ore spawning

    void Start()
    {
        GenerateCavern();
    }

    void GenerateCavern()
    {
        // Generate background sprite
        GameObject backgroundSprite = Instantiate(backgroundSpritePrefab, transform.position, Quaternion.identity);
        backgroundSprite.transform.localScale = new Vector3(width, height, 1);

        // Place background behind normal sprites
        backgroundSprite.transform.position = new Vector3(width / 2f - 0.5f, height / 2f - 0.5f, 0);
        backgroundSprite.GetComponent<SpriteRenderer>().sortingOrder = -1;

        // Calculate center of the cavern for camera
        Vector3 cavernCenter = new Vector3(width / 2f, height / 2f, 0);
        // Move the camera to the center of the cavern
        Camera.main.transform.position = new Vector3(cavernCenter.x, cavernCenter.y, Camera.main.transform.position.z);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float perlinValue = Mathf.PerlinNoise(x / noiseScale, y / noiseScale);

                if (perlinValue > threshold)
                {
                    Vector3 position = new Vector3(x, y, 0);
                    Instantiate(cavernSpritePrefab, position, Quaternion.identity);

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
                                GameObject oreBlockSprite = Instantiate(oreBlockInfo.oreBlockPrefab, position, Quaternion.identity);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}


//----------

