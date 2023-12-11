using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{
    // Player input
    PlayerInput pi;

    // Tool highlighting
    public GameObject pickaxeHightlight;
    public GameObject swordHighlight;

    // Text objects for each ore type
    public TextMeshProUGUI coalText;
    public TextMeshProUGUI copperText;
    public TextMeshProUGUI ironText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI crystalText;
    public TextMeshProUGUI coinsText;

    // Text objects for each pickaxe property
    public TextMeshProUGUI mineDamageText;
    public TextMeshProUGUI mineRangeText;
    public TextMeshProUGUI mineSpeedText;
    public TextMeshProUGUI mineWidthText;
    public TextMeshProUGUI oreBonusText;
    public TextMeshProUGUI oreMultiplierText;

    // Text object for each sword property
    public TextMeshProUGUI attackDamageText;
    public TextMeshProUGUI attackRangeText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI attackWidthText;
    public TextMeshProUGUI critChanceText;
    public TextMeshProUGUI critDamageText;

    // Variables to store ore values
    [SerializeField] private int _coalValue = 0;
    [SerializeField] private int _copperValue = 0;
    [SerializeField] private int _ironValue = 0;
    [SerializeField] private int _goldValue = 0;
    [SerializeField] private int _crystalValue = 0;
    [SerializeField] private int _coinsValue = 0;

    // Variables to store pickaxe values
    [SerializeField] private int _mineDamageValue = 0;
    [SerializeField] private int _mineRangeValue = 0;
    [SerializeField] private int _mineSpeedValue = 0;
    [SerializeField] private int _mineWidthValue = 0;
    [SerializeField] private int _oreBonusValue = 0;
    [SerializeField] private int _oreMultiplierValue = 0;

    // Variables to store sword values
    [SerializeField] private int _attackDamageValue = 0;
    [SerializeField] private int _attackRangeValue = 0;
    [SerializeField] private int _attackSpeedValue = 0;
    [SerializeField] private int _attackWidthValue = 0;
    [SerializeField] private int _critChanceValue = 0;
    [SerializeField] private int _critDamageValue = 0;

    // Ore values
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

    // Pickaxe values
    public int mineDamageValue
    {
        get { return _mineDamageValue; }
        set
        {
            if (_mineDamageValue != value)
            {
                _mineDamageValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int mineRangeValue
    {
        get { return _mineRangeValue; }
        set
        {
            if (_mineRangeValue != value)
            {
                _mineRangeValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int mineSpeedValue
    {
        get { return _mineSpeedValue; }
        set
        {
            if (_mineSpeedValue != value)
            {
                _mineSpeedValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int mineWidthValue
    {
        get { return _mineWidthValue; }
        set
        {
            if (_mineWidthValue != value)
            {
                _mineWidthValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int oreBonusValue
    {
        get { return _oreBonusValue; }
        set
        {
            if (_oreBonusValue != value)
            {
                _oreBonusValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int oreMultiplierValue
    {
        get { return _oreMultiplierValue; }
        set
        {
            if (_oreMultiplierValue != value)
            {
                _oreMultiplierValue = value;
                UpdateInventoryText();
            }
        }
    }

    // Sword values
    public int attackDamageValue
    {
        get { return _attackDamageValue; }
        set
        {
            if (_attackDamageValue != value)
            {
                _attackDamageValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int attackRangeValue
    {
        get { return _attackRangeValue; }
        set
        {
            if (_attackRangeValue != value)
            {
                _attackRangeValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int attackSpeedValue
    {
        get { return _attackSpeedValue; }
        set
        {
            if (_attackSpeedValue != value)
            {
                _attackSpeedValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int attackWidthValue
    {
        get { return _attackWidthValue; }
        set
        {
            if (_attackWidthValue != value)
            {
                _attackWidthValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int critChanceValue
    {
        get { return _critChanceValue; }
        set
        {
            if (_critChanceValue != value)
            {
                _critChanceValue = value;
                UpdateInventoryText();
            }
        }
    }

    public int critDamageValue
    {
        get { return _critDamageValue; }
        set
        {
            if (_critDamageValue != value)
            {
                _critDamageValue = value;
                UpdateInventoryText();
            }
        }
    }

    private void Awake()
    {
        // Player input
        pi = GetComponent<PlayerInput>();
        // Turn off sword highlight
        swordHighlight.SetActive(false);
    }

    private void Update()
    {
        // Update highlighted tool
        if (pi.actions["SelectPickaxe"].IsPressed()) {
            pickaxeHightlight.SetActive(true);
            swordHighlight.SetActive(false);
        }
        if (pi.actions["SelectSword"].IsPressed()) {
            pickaxeHightlight.SetActive(false);
            swordHighlight.SetActive(true);
        }
    }

    // Update all text
    void UpdateInventoryText()
    {
        // Ore text
        coalText.text = $"{_coalValue}";
        copperText.text = $"{_copperValue}";
        ironText.text = $"{_ironValue}";
        goldText.text = $"{_goldValue}";
        crystalText.text = $"{_crystalValue}";
        coinsText.text = $"{_coinsValue}</b>";

        // Pickaxe text
        mineDamageText.text = $"{_mineDamageValue}";
        mineRangeText.text = $"{_mineRangeValue}";
        mineSpeedText.text = $"{_mineSpeedValue}";
        mineWidthText.text = $"{_mineWidthValue}";
        oreBonusText.text = $"{_oreBonusValue}";
        oreMultiplierText.text = $"{_oreMultiplierValue}";

        // Sword text
        attackDamageText.text = $"{_attackDamageValue}";
        attackRangeText.text = $"{_attackRangeValue}";
        attackSpeedText.text = $"{_attackSpeedValue}";
        attackWidthText.text = $"{_attackWidthValue}";
        critChanceText.text = $"{_critChanceValue}";
        critDamageText.text = $"{_critDamageValue}";
    }


    private void OnValidate()
    {
        // OnValidate() will update the text in the Editor when any ore/coin value is changed through the Inspector window, otherwise only scripts can update the value and text.
        UpdateInventoryText();
    }
}
