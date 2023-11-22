using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    private List<ShopItemView> _shopItems = new List<ShopItemView>();

    [SerializeField] private Transform itemsParent;
    [SerializeField] private ShopItemViewFactory shopItemViewFactory;
    [SerializeField] private NotEnoughCoinsPanel notEnoughCoinsPanel;

    public void Show(IEnumerable<ShopItem> items)
    {
        Clear();
        foreach (ShopItem item in items)
        {
            ShopItemView spawnedItem = shopItemViewFactory.Get(item, itemsParent);
            spawnedItem.OnBuyClicked += BuyAbility;

            _shopItems.Add(spawnedItem);
        }
    }

    private void BuyAbility(ShopItemView ability)
    {
        if (ability.Price <= SLS.Data.TotalCoinsAmount.Value)
        {
            SLS.Data.TotalCoinsAmount.Value -= ability.Price;
            switch (ability.Item.AbilityType)
            {
                case Abilities.Invincibillity:
                    SLS.Data.InvincibilityAmount.Value++;
                    break;
                case Abilities.DoubleScore:
                    SLS.Data.DoubleScoreAbilityAmount.Value++;
                    break;
                case Abilities.ObstacleDestroyer:
                    SLS.Data.ObstacleDestroyerAmount.Value++;
                    break;
                case Abilities.FreeMana:
                    SLS.Data.FreeManaAmount.Value++;
                    break;
            }
            ability.SetAbilityAmount(ability.Item.AbilityType);
        }
        else
        {
            OpenNotEnoughPanel();
        }
    }

    private void OpenNotEnoughPanel()
    {
        if(notEnoughCoinsPanel.isActiveAndEnabled)
        {
            notEnoughCoinsPanel.gameObject.SetActive(false);
        }
        notEnoughCoinsPanel.gameObject.SetActive(true);
    }

    private void Clear()
    {
        foreach (ShopItemView item in _shopItems)
        {
            item.OnBuyClicked -= BuyAbility;
            Destroy(item.gameObject);
        }

        _shopItems.Clear();
    }
}
