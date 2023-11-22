using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseAbility : MonoBehaviour
{
    public delegate void AbilityAction();
    protected void SetAbilityAmount(Text amount, int newValue)
    {
        amount.text = newValue.ToString();
    }

    protected bool CheckAbilityAvailable(bool isAvailable, int abilityAmount, Button abilityButton)
    {
        if (abilityAmount > 0)
        {
            isAvailable = true;
            abilityButton.interactable = true;
        }
        else
        {
            isAvailable = false;
            abilityButton.interactable = false;
        }
        return isAvailable;
    }

    protected abstract void ActivateAbility();
}
