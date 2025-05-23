// using System;
// using System.IO;
// using System.Linq;
// using UnityEngine;
// using System.Collections.Generic;
//
// public static class SaveSystem
// {
//     public const string FILENAME_SAVEDATA = "/savedata.json";
//     public static List<FirearmWeaponSO> allWeapons = new List<FirearmWeaponSO>();
//
//     public static void SaveGameState()
//     {
//         string filePathSaveData = Application.persistentDataPath + FILENAME_SAVEDATA;
//
//         WeaponData weaponData = new WeaponData(ActiveWeapon.Instance);
//         HealthData healthData = new HealthData(PlayerHealth.Instance);
//
//         SaveData saveData = new SaveData(weaponData, healthData);
//
//         string json = JsonUtility.ToJson(saveData, true); // pretty print
//         File.WriteAllText(filePathSaveData, json);
//
//         Debug.Log("Saved to: " + filePathSaveData);
//     }
// }
//
// [Serializable]
// public class WeaponData
// {
//     [SerializeField] public List<string> weaponIDs;
//     [SerializeField] public List<int> weaponAmmos;
//     [SerializeField] public string currentWeaponID;
//
//
//     public WeaponData(ActiveWeapon activeWeapon)
//     {
//         weaponIDs = new List<string>();
//         weaponAmmos = new List<int>();
//         currentWeaponID = "";
//
//         if (activeWeapon == null) return;
//         if (activeWeapon.availableWeapons != null)
//             weaponIDs = activeWeapon.availableWeapons.Select(x => x.WeaponID).ToList();
//
//         if (activeWeapon.weaponAmmos != null)
//             weaponAmmos = activeWeapon.weaponAmmos.ToList();
//
//         if (activeWeapon.currentWeaponSO != null)
//             currentWeaponID = activeWeapon.currentWeaponSO.WeaponID;
//     }
// }
//
// [Serializable]
// public class HealthData
// {
//     public int playerHealth;
//     public Vector3 playerPosition;
//     public HealthData(PlayerHealth player)
//     {
//         playerHealth = player.currentHealth;
//         playerPosition = player.transform.position;
//     }
// }
// [Serializable]
// public class SaveData
// {
//     public WeaponData weaponData;
//     public HealthData healthData;
//
//     public SaveData(WeaponData weaponData, HealthData healthData)
//     {
//         this.weaponData = weaponData;
//         this.healthData = healthData;
//     }
// }
//
