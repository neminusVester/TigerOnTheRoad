using System;

public class GameEvents : MonoSingleton<GameEvents>
{
    public event Action<bool> OnHolding; //true if player hold finger on screen
    public event Action OnSaveZonePassed;
    public event Action<bool> OnInvincibility; //true if invincibility active
    public event Action<bool> OnFreeMana; //true if free mana ability active
    public event Action OnObstacleDestroyer; //when obstacle destroyer ability activated
    public event Action OnDoubleScore; //when double score ability activated
    public event Action OnGameStarted; //player started move
    public event Action OnGameStoped;
    public event Action OnGameLosed;
    public event Action OnLostGameContinues; //revived to game
    public event Action OnManaEmpty;
    public event Action OnPlayPressed;
    public event Action OnHomePressed;
    public event Action OnRestartPressed;
    public event Action OnCoinsAmountDecreased;
    public event Action OnDelayPanelActivated;
    public event Action<ShopItemView> OnBuyButtonClicked;
    public event Action OnStartedDynamicObstacles;

    public void PlayerHoldongFinger(bool isHolding) => OnHolding?.Invoke(isHolding);

    public void PlayerPasedObstacle() => OnSaveZonePassed?.Invoke();

    public void InvincibilityActive(bool isActive) => OnInvincibility?.Invoke(isActive);

    public void FreeManaActive(bool isActive) => OnFreeMana?.Invoke(isActive);

    public void DestroyObstacles() => OnObstacleDestroyer?.Invoke();

    public void DoubleScoreActivated() => OnDoubleScore?.Invoke();

    public void StartGame() => OnGameStarted?.Invoke();

    public void PlayerStopGame() => OnGameStoped?.Invoke();

    public void PlayerLoseGame() => OnGameLosed?.Invoke();

    public void PlayerSelectReviveButton() => OnLostGameContinues?.Invoke();

    public void ManaIsOver() => OnManaEmpty?.Invoke();

    public void PlayerPressPlay() => OnPlayPressed?.Invoke();

    public void PlayerPressHome() => OnHomePressed?.Invoke();

    public void PlayerPressRestart() => OnRestartPressed?.Invoke();

    public void PlayerSpentCoins() => OnCoinsAmountDecreased?.Invoke();

    public void ActivateDelayPanel() => OnDelayPanelActivated?.Invoke();

    public void PlayerBuyAbility(ShopItemView shopItemView) => OnBuyButtonClicked?.Invoke(shopItemView);

    public void StartPreparingDynamicObstacle() => OnStartedDynamicObstacles?.Invoke();

}
