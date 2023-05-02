using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreference<T>
{
    public string Name;
    public T DefaultValue;
}
public class PlayerPrefsStrings
{
    public static readonly PlayerPreference<int> Level = new PlayerPreference<int> {Name = "Level", DefaultValue = 1};
public static readonly PlayerPreference<int> TimesRotated = new PlayerPreference<int> {Name = "TimesRotated", DefaultValue = 0};
    public static readonly PlayerPreference<float> SkinProgress = new PlayerPreference<float>
        {Name = "SkinProgress", DefaultValue = 0.01f};
    public static readonly PlayerPreference<int> SkinNumber = new PlayerPreference<int> {Name = "SkinNumber", DefaultValue = 0};
    public static readonly PlayerPreference<int> SkinsUnlocked = new PlayerPreference<int> {Name = "SkinsUnlocked", DefaultValue = 0};
    public static readonly PlayerPreference<float> MaxWeight = new PlayerPreference<float>
    {
        Name = "MaxWeight", DefaultValue = 1.5f
    };
    public static readonly PlayerPreference<int> UpgradeLevel = new PlayerPreference<int>
    {
        Name = "UpgradeLevel", DefaultValue = 0
    };
    public static readonly PlayerPreference<int> MoneyTotal = new PlayerPreference<int>
    {
        Name = "MoneyTotal", DefaultValue = 0
    };
    public static readonly PlayerPreference<int> UpgradeCost = new PlayerPreference<int>
    {
        Name = "UpgradeCost", DefaultValue = 2
    };

    public static readonly PlayerPreference<int> IsNewUnlocked = new PlayerPreference<int>
    {
        Name = "IsNewUnlocked", DefaultValue = 0
    };

    public static readonly PlayerPreference<int> LevelFixed = new PlayerPreference<int>()
    {
        Name = "LevelFixed", DefaultValue = 1
    };
    public static readonly PlayerPreference<int> NumbersTutorShown = new PlayerPreference<int>()
    {
        Name = "NumbersTutorShown", DefaultValue = 0
    };
    public static readonly PlayerPreference<int> WiresTutorShown = new PlayerPreference<int>()
    {
        Name = "WiresTutorShown", DefaultValue = 0
    };
    public static readonly PlayerPreference<float> PlayerScore = new PlayerPreference<float>()
    {
        Name = "PlayerScore", DefaultValue = 0f
    };

    public static readonly PlayerPreference<int> PlayerPlacement = new PlayerPreference<int>()
    {
        Name = "PlayerPlacement", DefaultValue = 9999
    };
    public static readonly PlayerPreference<int> PlayerBestScore = new PlayerPreference<int>()
    {
        Name = "PlayerBestScore", DefaultValue = 0
    };
    public static int GetIntValue(PlayerPreference<int> preference)
    {
        return PlayerPrefs.GetInt(preference.Name, preference.DefaultValue);
    }

    public static float GetFloatValue(PlayerPreference<float> preference)
    {
        return PlayerPrefs.GetFloat(preference.Name, preference.DefaultValue);
    }
}
