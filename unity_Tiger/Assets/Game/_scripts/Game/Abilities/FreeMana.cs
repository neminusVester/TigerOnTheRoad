using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FreeMana : BaseAbility
{
    [SerializeField] private Button freeManaButton;
    [SerializeField] private Text abilityAmountText;
    [SerializeField] private float cooldawnAbility = 20f;
    [SerializeField] private float durationAbility = 10f;
    private bool _isAbilityAvailable = false;

    private void Start()
    {
        SetAmount(SLS.Data.FreeManaAmount.Value);
        CheckAbilityAvailable();
        freeManaButton.onClick.AddListener(ActivateAbility);
        SLS.Data.FreeManaAmount.OnValueChanged += SetAmount;
    }

    private void OnDestroy()
    {
        SLS.Data.FreeManaAmount.OnValueChanged -= SetAmount;
    }

    protected override void ActivateAbility()
    {
        if (_isAbilityAvailable)
        {
            GameEvents.Instance.FreeManaActive(true);
            SLS.Data.FreeManaAmount.Value--;
            _isAbilityAvailable = false;
            freeManaButton.interactable = false;

            StartCoroutine(CountAbilityDuration(durationAbility));
        }
    }

    private IEnumerator CountAbilityDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        GameEvents.Instance.FreeManaActive(false);
        StartCoroutine(CountAbilityCooldown(cooldawnAbility));
    }

    private IEnumerator CountAbilityCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        CheckAbilityAvailable();
    }

    private bool CheckAbilityAvailable()
    {
        return _isAbilityAvailable = base.CheckAbilityAvailable(_isAbilityAvailable, SLS.Data.FreeManaAmount.Value, freeManaButton);
    }

    private void SetAmount(int newAmount)
    {
        base.SetAbilityAmount(abilityAmountText, newAmount);
    }
}
