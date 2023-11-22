using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleDestroyer : BaseAbility
{
     [SerializeField] private Button obstacleDestroyerButton;
    [SerializeField] private Text abilityAmountText;
    [SerializeField] private float cooldawnAbility = 10f;
    private bool _isAbilityAvailable = false;

    private void Start()
    {
        SetAmount(SLS.Data.ObstacleDestroyerAmount.Value);
        CheckAbilityAvailable();
        obstacleDestroyerButton.onClick.AddListener(ActivateAbility);
        SLS.Data.ObstacleDestroyerAmount.OnValueChanged += SetAmount;
    }

    private void OnDestroy()
    {
        SLS.Data.ObstacleDestroyerAmount.OnValueChanged -= SetAmount;
    }

    protected override void ActivateAbility()
    {
        if (_isAbilityAvailable)
        {
            GameEvents.Instance.DestroyObstacles();
            SLS.Data.ObstacleDestroyerAmount.Value--;
            _isAbilityAvailable = false;
            obstacleDestroyerButton.interactable = false;

            StartCoroutine(CountAbilityCooldown(cooldawnAbility));
        }
    }

    private IEnumerator CountAbilityCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        CheckAbilityAvailable();
    }

    private bool CheckAbilityAvailable()
    {
        return _isAbilityAvailable = base.CheckAbilityAvailable(_isAbilityAvailable, SLS.Data.ObstacleDestroyerAmount.Value, obstacleDestroyerButton);
    }

    private void SetAmount(int newAmount)
    {
        base.SetAbilityAmount(abilityAmountText, newAmount);
    }
}
