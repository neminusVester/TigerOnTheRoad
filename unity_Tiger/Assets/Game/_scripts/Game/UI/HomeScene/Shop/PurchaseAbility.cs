using UnityEngine;
using UnityEngine.UI;

public class PurchaseAbility : MonoBehaviour
{
    [SerializeField] private Button buyInvincibilityButton;
    [SerializeField] private Button buyDoubleScoreButton;
    [SerializeField] private Text invincibilityAmount;
    [SerializeField] private Text doubleScoreAmount;
    [SerializeField] private Text invincibilityPrice;
    [SerializeField] private Text doubleScorePrice;
    private int _invincibilityPrice => SLS.Data.GameData.InvincibilityPrice.Value;
    private int _doubleScorePrice => SLS.Data.GameData.DoubleScorePrice.Value;

    private void Start()
    {
        SetAbilitiesPrice();
        SetAbilityAmountText(invincibilityAmount, SLS.Data.InvincibilityAmount.Value);
        SetAbilityAmountText(doubleScoreAmount, SLS.Data.DoubleScoreAbilityAmount.Value);
        buyInvincibilityButton.onClick.AddListener(BuyInvincibility);
        buyDoubleScoreButton.onClick.AddListener(BuyDoubleScore);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(SLS.Data.InvincibilityAmount.Value);
        }
    }

    private void BuyInvincibility()
    {
        if (_invincibilityPrice <= SLS.Data.TotalCoinsAmount.Value)
        {
            SLS.Data.TotalCoinsAmount.Value -= _invincibilityPrice;
            GameEvents.Instance.PlayerSpentCoins();
            SLS.Data.InvincibilityAmount.Value++;
            SetAbilityAmountText(invincibilityAmount, SLS.Data.InvincibilityAmount.Value);
        }
    }

    private void BuyDoubleScore()
    {
        if (_doubleScorePrice <= SLS.Data.TotalCoinsAmount.Value)
        {
            SLS.Data.TotalCoinsAmount.Value -= _doubleScorePrice;
            GameEvents.Instance.PlayerSpentCoins();
            SLS.Data.DoubleScoreAbilityAmount.Value++;
            SetAbilityAmountText(doubleScoreAmount, SLS.Data.DoubleScoreAbilityAmount.Value);
        }
    }

    private void SetAbilityAmountText(Text amountText, int amountValue)
    {
        amountText.text = amountValue.ToString();
    }

    private void SetAbilitiesPrice()
    {
        invincibilityPrice.text = _invincibilityPrice.ToString();
        doubleScorePrice.text = _doubleScorePrice.ToString();
    }
}
