using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class UserSettingsManager : MonoBehaviour
{
    [SerializeField] private OptionsMenuController _optionsMenuController;

    private void Awake() 
    {
        _optionsMenuController = GetComponent<OptionsMenuController>();    
    }

    private void Start() 
    {
        // SaveUserSettings();
        LoadUserSettings();
    }

    public void SaveUserSettings()
    {
        // Saves current stats to struct


        FileStream file = new FileStream(Application.persistentDataPath + "/UserSettings.dat", FileMode.OpenOrCreate);

        try
        {
            // Try to serialize data
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, _optionsMenuController.userSettings);
        }
        catch (SerializationException e)
        {
            Debug.LogError("There was an issue serializing this data: " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }

    public void LoadUserSettings()
    {
        if (!System.IO.File.Exists(Application.persistentDataPath + "/UserSettings.dat"))
        {
            SaveUserSettingsFromDefault();
            return;
        }

        FileStream file = new FileStream(Application.persistentDataPath + "/UserSettings.dat", FileMode.Open);

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            _optionsMenuController.userSettings = (UserSettings)formatter.Deserialize(file); // Same as writing "formatter.Deserialize(file) as Stats"
        }
        catch (SerializationException e)
        {
            Debug.LogError("There was an issue de-serializing this data: " + e.Message);
        }
        finally
        {
            file.Close();
            // Set UI?
        }
    }

    public void SaveUserSettingsFromDefault()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/UserSettings.dat", FileMode.OpenOrCreate);

        try
        {
            // Try to serialize data
            BinaryFormatter formatter = new BinaryFormatter();
            _optionsMenuController.userSettings = _optionsMenuController.userSettings.GetDefaultUserSettings();
            formatter.Serialize(file, _optionsMenuController.userSettings);
        }
        catch (SerializationException e)
        {
            Debug.LogError("There was an issue serializing this data: " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }
}
