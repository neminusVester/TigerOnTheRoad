using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Abilityitem", menuName = "Shop/AbilityItem")]
public class ShopItem : ScriptableObject
{
    [field: SerializeField] public Abilities AbilityType { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField, Range(0, 10000)] public int Price { get; private set; }
    [field: SerializeField] public string Name {get; private set;}
}
