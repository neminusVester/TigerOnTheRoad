using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Sprite background;
    [SerializeField] private Image abilityImage;
    [SerializeField] private Text abilityAmount;
    [SerializeField] private Text abilityPrice;
    [SerializeField] private Text abilityName;
    public event Action<ShopItemView> OnBuyClicked;
    private Image _backgroundImage;

    public ShopItem Item { get; private set; }

    public int Price => Item.Price;

    private void Start()
    {
        buyButton.onClick.AddListener(() => OnBuyClicked?.Invoke(this));
        var v = SLS.Data.FreeManaAmount;
    }

    public void Initialize(ShopItem item)
    {
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = background;
        Item = item;

        abilityName.text = item.Name;
        abilityImage.sprite = item.Icon;
        abilityPrice.text = item.Price.ToString();
        SetAbilityAmount(item.AbilityType);
    }

    public void SetAbilityAmount(Abilities abilities)
    {
        switch (abilities)
        {
            case Abilities.Invincibillity:
                abilityAmount.text = SLS.Data.InvincibilityAmount.Value.ToString();
                break;
            case Abilities.DoubleScore:
                abilityAmount.text = SLS.Data.DoubleScoreAbilityAmount.Value.ToString();
                break;
            case Abilities.ObstacleDestroyer:
                abilityAmount.text = SLS.Data.ObstacleDestroyerAmount.Value.ToString();
                break;
            case Abilities.FreeMana:
                abilityAmount.text = SLS.Data.FreeManaAmount.Value.ToString();
                break;
        }
    }
}
