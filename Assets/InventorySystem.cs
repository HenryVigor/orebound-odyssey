using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    // Text objects for each ore type
    public TextMeshProUGUI copperText;
    public TextMeshProUGUI ironText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI crystalText;
    public TextMeshProUGUI coinsText;

    // Variables to store ore values
    [SerializeField] private int _copperValue = 0;
    [SerializeField] private int _ironValue = 0;
    [SerializeField] private int _goldValue = 0;
    [SerializeField] private int _crystalValue = 0;
    [SerializeField] private int _coinsValue = 0;

    public int CopperValue
    {
        get { return _copperValue; }
        set
        {
            if (_copperValue != value)
            {
                _copperValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int IronValue
    {
        get { return _ironValue; }
        set
        {
            if (_ironValue != value)
            {
                _ironValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int GoldValue
    {
        get { return _goldValue; }
        set
        {
            if (_goldValue != value)
            {
                _goldValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int CrystalValue
    {
        get { return _crystalValue; }
        set
        {
            if (_crystalValue != value)
            {
                _crystalValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int CoinsValue
    {
        get { return _coinsValue; }
        set
        {
            if (_coinsValue != value)
            {
                _coinsValue = value;
                UpdateInventoryText();
            }
        }
    }

    void UpdateInventoryText()
    {
        copperText.text = $"Copper: {_copperValue}";
        ironText.text = $"Iron: {_ironValue}";
        goldText.text = $"Gold: {_goldValue}";
        crystalText.text = $"Crystal: {_crystalValue}";
        coinsText.text = $"<b>Coins: {_coinsValue}</b>";
    }


    private void OnValidate()
    {
        // OnValidate() will update the text in the Editor when any ore/coin value is changed through the Inspector window, otherwise only scripts can update the value and text.
        UpdateInventoryText();
    }
}
