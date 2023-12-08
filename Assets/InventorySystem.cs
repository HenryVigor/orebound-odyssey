using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    // Text objects for each ore type
    public TextMeshProUGUI coalText;
    public TextMeshProUGUI copperText;
    public TextMeshProUGUI ironText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI crystalText;
    public TextMeshProUGUI coinsText;

    // Variables to store ore values
    [SerializeField] private int _coalValue = 0;
    [SerializeField] private int _copperValue = 0;
    [SerializeField] private int _ironValue = 0;
    [SerializeField] private int _goldValue = 0;
    [SerializeField] private int _crystalValue = 0;
    [SerializeField] private int _coinsValue = 0;

    public int CoalValue
    {
        get { return _coalValue; }
        set
        {
            if (_coalValue != value)
            {
                _coalValue = value;
                UpdateInventoryText();
            }
        }
    }

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
        coalText.text = $"{_coalValue}";
        copperText.text = $"{_copperValue}";
        ironText.text = $"{_ironValue}";
        goldText.text = $"{_goldValue}";
        crystalText.text = $"{_crystalValue}";
        coinsText.text = $"{_coinsValue}</b>";
    }


    private void OnValidate()
    {
        // OnValidate() will update the text in the Editor when any ore/coin value is changed through the Inspector window, otherwise only scripts can update the value and text.
        UpdateInventoryText();
    }
}
