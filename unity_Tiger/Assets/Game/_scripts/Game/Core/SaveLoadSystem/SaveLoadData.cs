using System;
using System.Collections.Generic;

[Serializable]
public class SaveLoadData
{
    public SettingsData Settings;
    public GameDataSaves GameData;
    public StoredValue<int> InvincibilityAmount;
    public StoredValue<int> DoubleScoreAbilityAmount;
    public StoredValue<int> ObstacleDestroyerAmount;
    public StoredValue<int> FreeManaAmount;
    public StoredValue<int> TotalCoinsAmount;

    public SaveLoadData()
    {
        Settings = new SettingsData();
        GameData = new GameDataSaves();
        InvincibilityAmount = new StoredValue<int>(2);
        DoubleScoreAbilityAmount = new StoredValue<int>(2);
        ObstacleDestroyerAmount = new StoredValue<int>(2);
        FreeManaAmount = new StoredValue<int>(2);
        TotalCoinsAmount = new StoredValue<int>(1000);
    }
}