using System;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public static class SaveSystem
{
    public const string FILENAME_SAVEDATA = "/savedata.json";

    public static void SaveGameState()
    {
        string filePathSaveData = Application.persistentDataPath + FILENAME_SAVEDATA;
        Debug.Log("Save file path: " + filePathSaveData);
        PlayerData playerData = new PlayerData(ActiveWeapon.Instance);
        SaveData saveData = new SaveData(playerData);
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(filePathSaveData, json);
    }
}

[Serializable]
public class PlayerData
{
    [SerializeField] public List<string> weaponIDs;
    [SerializeField] public List<int> weaponAmmos;
    [SerializeField] public string currentWeaponID;

    public PlayerData(ActiveWeapon activeWeapon)
    {
        weaponIDs = new List<string>();
        weaponAmmos = new List<int>();
        currentWeaponID = "";

        if (activeWeapon == null) return;
        if (activeWeapon.availableWeapons != null)
            weaponIDs = activeWeapon.availableWeapons.Where(w => w != null).Select(w => w.name).ToList();

        if (activeWeapon.weaponAmmos != null)
            weaponAmmos = activeWeapon.weaponAmmos.ToList();

        if (activeWeapon.currentWeapon != null)
            currentWeaponID = activeWeapon.currentWeapon.name;
    }
}


[Serializable]
public class SaveData
{
    [SerializeField] PlayerData playerData;
    public SaveData(PlayerData playerData) => this.playerData = playerData;
}
