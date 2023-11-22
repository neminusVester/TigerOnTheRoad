using UnityEngine;
using UnityEngine.UI;

public class DoubleScore : BaseAbility
{
    [SerializeField] private Button doubleScoreButton;
    [SerializeField] private Text abilityAmountText;
    private bool _isAbilityAvailable;

    private void Start()
    {
        SetAmount(SLS.Data.DoubleScoreAbilityAmount.Value);
        CheckAbilityAvailable();
        doubleScoreButton.onClick.AddListener(ActivateAbility);
        SLS.Data.DoubleScoreAbilityAmount.OnValueChanged += SetAmount;
    }

    private void OnDestroy()
    {
        SLS.Data.DoubleScoreAbilityAmount.OnValueChanged -= SetAmount;
    }

    protected override void ActivateAbility()
    {
        if (_isAbilityAvailable)
        {
            GameEvents.Instance.DoubleScoreActivated();
            SLS.Data.DoubleScoreAbilityAmount.Value--;
            _isAbilityAvailable = false;
            doubleScoreButton.interactable = false;
        }
    }

    private bool CheckAbilityAvailable()
    {
        return _isAbilityAvailable = base.CheckAbilityAvailable(_isAbilityAvailable, SLS.Data.DoubleScoreAbilityAmount.Value, doubleScoreButton);
    }

    private void SetAmount(int newAmount)
    {
        base.SetAbilityAmount(abilityAmountText, newAmount);
    }
}
