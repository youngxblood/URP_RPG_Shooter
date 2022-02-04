using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveManager : MonoBehaviour
{
    private Player _player;
    private WeaponManager _weaponManager;

    private void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();
    }

    public void Save()
    {
        // Saves current stats to struct
        _player.SaveStats(); 
        _weaponManager.SaveStats();

        FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.OpenOrCreate);

        try
        {
            // Try to serialize data
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, _player.playerStats.stats);
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

    public void Load()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.Open);

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            _player.playerStats.stats = (Stats)formatter.Deserialize(file); // Same as writing "formatter.Deserialize(file) as Stats"
        }
        catch (SerializationException e)
        {
            Debug.LogError("There was an issue de-serializing this data: " + e.Message);
        }
        finally
        {
            file.Close();
            _player.LoadFromStats();
            _weaponManager.LoadFromStats();
        }
    }
}
