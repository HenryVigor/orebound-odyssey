using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // Variables to store ore values
    private int copperValue;
    private int ironValue;
    private int crystalValue;
    private int goldValue;
    private int coinsValue;

    // Text objects for each ore type
    public Text copperText;
    public Text ironText;
    public Text crystalText;
    public Text goldText;
    public Text coinsText;

    void Update()
    {
        // Update values from text objects
        UpdateOreValues();
        
        // Display values in the 'Inventory' UI Text
        UpdateInventoryText();
    }

    void UpdateOreValues()
    {
        // Parse values from text objects
        copperValue = ExtractValueFromText(copperText);
        ironValue = ExtractValueFromText(ironText);
        crystalValue = ExtractValueFromText(crystalText);
        goldValue = ExtractValueFromText(goldText);
        coinsValue = ExtractValueFromText(coinsText);
    }

    int ExtractValueFromText(Text oreText)
    {
        // Extract the numeric value from the ore text
        string[] parts = oreText.text.Split(':');
        if (parts.Length == 2)
        {
            if (int.TryParse(parts[1].Trim(), out int value))
            {
                return value;
            }
        }
        // Return 0 if extraction fails
        return 0;
    }

    void UpdateInventoryText()
    {
        // Update the 'Inventory' UI Text with the ore values
        Text inventoryText = GetComponent<Text>();
        inventoryText.text = $"Copper: {copperValue}\nIron: {ironValue}\nCrystal: {crystalValue}\nGold: {goldValue}\nCoins: {coinsValue}";
    }
}
