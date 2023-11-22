using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent contentItems;
    [SerializeField] private ShopPanel shopPanel;
    // [SerializeField] private NotEnoughCoinsPanel notEnoughCoinsPanel;

    private void Start()
    {
        shopPanel.Show(contentItems.AbilityItems.Cast<ShopItem>());

        // GameEvents.Instance.OnNotEnoughMoney += OpenNotEnoughPanel;
    }

    private void OnDestroy()
    {
        // GameEvents.Instance.OnNotEnoughMoney -= OpenNotEnoughPanel;
    }

   /*  private void OpenNotEnoughPanel()
    {
        notEnoughCoinsPanel.gameObject.SetActive(true);
    } */
}
