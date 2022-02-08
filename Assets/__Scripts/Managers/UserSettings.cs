using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SerializableVector2
{
    public float width;
    public float height;

    public Vector2 GetPosition()
    {
        return new Vector2(width, height);
    }
}

// This is what is used to save player settings from the option menu
[System.Serializable]
public class UserSettings
{
    public SerializableVector2 screenResolution;
    public bool isFullscreen;
    public int graphicsQualityIndex;
    public float globalVolume;

    public UserSettings GetDefaultUserSettings()
    {
        UserSettings settings = new UserSettings();
        settings.isFullscreen = true;
        settings.graphicsQualityIndex = 2;
        settings.globalVolume = 0;

        return settings;
    }
}
