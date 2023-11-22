using System;

[Serializable]
public class GameDataSaves
{
    public StoredValue<int> InvincibilityPrice;
    public StoredValue<int> DoubleScorePrice;
    public StoredValue<int> PriceReviveToGame;
    public StoredValue<bool> TutorialCompleted;

    public GameDataSaves()
    {
        InvincibilityPrice = new StoredValue<int>(400);
        DoubleScorePrice = new StoredValue<int>(600);
        PriceReviveToGame = new StoredValue<int>(1000);
        TutorialCompleted = new StoredValue<bool>(false);
    }
}