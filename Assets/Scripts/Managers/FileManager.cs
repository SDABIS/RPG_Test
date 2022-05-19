using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager : Singleton<FileManager>
{
    [System.Serializable]
    public class SaveData
    {
        public List<CharacterStats> characters;
        public Inventory inventory;
        public Zone currentZone;
    }

    public SaveData data;

    public SaveData Data => data;

    protected override void Awake()
    {
        base.Awake();

        LoadGameData();
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
        }
    }

    public void SaveGameData()
    {
        CollectApplicationData();

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    private void CollectApplicationData()
    {
        data.characters = PersistentParty.Instance.Characters;
        data.inventory = PersistentParty.Instance.Inventory;
        data.currentZone = PersistentParty.Instance.CurrentZone;
    }

    void OnApplicationQuit()
    {
        SaveGameData();
    }
}
