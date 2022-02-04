using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    private Player _player;

    private void Awake() 
    {
        _player = GameObject.FindObjectOfType<Player>();
    }

    public void Save()
    {
        _player.SaveStats(); // Saves current stats to struct

        // Open or create file
        FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.OpenOrCreate);
        BinaryFormatter formatter = new BinaryFormatter();

        // Serialize data
        formatter.Serialize(file, _player.playerStats.stats);
        file.Close();
        Debug.Log("Game is Saved!");
    }

    public void Load()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();

        _player.playerStats.stats = (Stats) formatter.Deserialize(file); // Same as writing "formatter.Deserialize(file) as Stats"
        _player.LoadFromStats();
        file.Close();
        Debug.Log("Loaded from Stats.");
    }
}
