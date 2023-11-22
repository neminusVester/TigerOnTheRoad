using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Invincibility : BaseAbility
{
    [SerializeField] private Button invincibilityButton;
    [SerializeField] private Text abilityAmountText;
    [SerializeField] private float cooldawnAbility = 10f;
    [SerializeField] private float durationAbility = 5f;
    private bool _isAbilityAvailable = false;

    private void Start()
    {
        SetAmount(SLS.Data.InvincibilityAmount.Value);
        CheckAbilityAvailable();
        invincibilityButton.onClick.AddListener(ActivateAbility);
        SLS.Data.InvincibilityAmount.OnValueChanged += SetAmount;
    }

    private void OnDestroy()
    {
        SLS.Data.InvincibilityAmount.OnValueChanged -= SetAmount;
    }

    protected override void ActivateAbility()
    {
        if (_isAbilityAvailable)
        {
            GameEvents.Instance.InvincibilityActive(true);
            SLS.Data.InvincibilityAmount.Value--;
            _isAbilityAvailable = false;
            invincibilityButton.interactable = false;

            StartCoroutine(CountAbilityDuration(durationAbility));
        }
    }

    private IEnumerator CountAbilityDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        GameEvents.Instance.InvincibilityActive(false);
        StartCoroutine(CountAbilityCooldown(cooldawnAbility));
    }

    private IEnumerator CountAbilityCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        CheckAbilityAvailable();
    }

    private bool CheckAbilityAvailable()
    {
        return _isAbilityAvailable = base.CheckAbilityAvailable(_isAbilityAvailable, SLS.Data.InvincibilityAmount.Value, invincibilityButton);
    }

    private void SetAmount(int newAmount)
    {
        base.SetAbilityAmount(abilityAmountText, newAmount);
    }
}
