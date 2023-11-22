using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<ShopItem> abilityItems;

    public IEnumerable<ShopItem> AbilityItems => abilityItems;

    private void OnValidate()
    {
        var abilitiesDuplicate = abilityItems.GroupBy(item => item.AbilityType)
            .Where(array => array.Count() > 1);
        if(abilitiesDuplicate.Count() > 0)
        {
            throw new InvalidOperationException(nameof(abilityItems));
        }
    }
}
