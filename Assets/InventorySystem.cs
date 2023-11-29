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
    public static int staticCopperValue = 0;
    [SerializeField] private int _ironValue = 0;
    public static int staticIronValue = 0;
    [SerializeField] private int _goldValue = 0;
    public static int staticGoldValue = 0;
    [SerializeField] private int _crystalValue = 0;
    public static int staticCrystalValue = 0;
    [SerializeField] private int _coinsValue = 0;
    public static int staticCoinsValue = 0;

    public int CopperValue
    {
        get { return _copperValue; }
        set
        {
            if (_copperValue != value)
            {
                _copperValue = value;
                UpdateInventoryText();
                staticCopperValue = value;
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
                staticIronValue = value;
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
                staticGoldValue = value;
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
                staticCrystalValue = value;
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
                staticCoinsValue = value;
            }
        }
    }
    
    void Start() {
        CopperValue = staticCopperValue;
        IronValue = staticIronValue;
        GoldValue = staticGoldValue;
        CrystalValue = staticCrystalValue;
        CoinsValue = staticCoinsValue;
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
