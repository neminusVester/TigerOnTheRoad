using System;

[Serializable]
public class SettingsData
{
    public StoredValue<bool> SoundsMuted;
    public StoredValue<bool> MusicMuted;

    public SettingsData()
    {
        SoundsMuted = new StoredValue<bool>(false);
        MusicMuted = new StoredValue<bool>(false);
    }
}
