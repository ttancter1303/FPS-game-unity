// using System;
// using System.Collections.Generic;
// using System.IO;
// using UnityEngine;
//
// public static class LoadSystem
// {
//     public static void LoadGameState()
//     {
//         if (!HasSaveFile())
//         {
//             Debug.Log("Không có file save. Tạo game mới.");
//             return; // hoặc chạy logic khởi tạo New Game
//         }
//
//         try
//         {
//             string filePathSaveData = Application.persistentDataPath + SaveSystem.FILENAME_SAVEDATA;
//             string fileContent = File.ReadAllText(filePathSaveData);
//             SaveData saveData = JsonUtility.FromJson<SaveData>(fileContent);
//
//             ApplyWeaponData(saveData.weaponData);
//             ApplyHealthData(saveData.healthData);
//         }
//         catch (Exception e)
//         {
//             Debug.LogError("Lỗi khi load game: " + e);
//         }
//     }
//     public static bool HasSaveFile()
//     {
//         string filePathSaveData = Application.persistentDataPath + SaveSystem.FILENAME_SAVEDATA;
//         return File.Exists(filePathSaveData);
//     }
//
//
//     private static void ApplyWeaponData(WeaponData data)
//     {
//         var activeWeapon = ActiveWeapon.Instance;
//         if (activeWeapon == null)
//         {
//             Debug.LogError("ActiveWeapon instance not found.");
//             return;
//         }
//
//         // Clear old data
//         for (int i = 0; i < activeWeapon.availableWeapons.Length; i++)
//         {
//             activeWeapon.availableWeapons[i] = null;
//             activeWeapon.weaponAmmos[i] = 0;
//         }
//
//         for (int i = 0; i < data.weaponIDs.Count; i++)
//         {
//             string id = data.weaponIDs[i];
//             int ammo = i < data.weaponAmmos.Count ? data.weaponAmmos[i] : 0;
//
//             var weaponSO = SaveSystem.allWeapons.Find(w => w.WeaponID == id);
//             if (weaponSO == null)
//             {
//                 Debug.LogWarning("WeaponID not found in allWeapons: " + id);
//                 continue;
//             }
//
//             activeWeapon.availableWeapons[i] = weaponSO;
//             activeWeapon.weaponAmmos[i] = ammo;
//         }
//
//         // Gán current weapon nếu tìm được
//         var currentWeaponSO = SaveSystem.allWeapons.Find(w => w.WeaponID == data.currentWeaponID);
//         if (currentWeaponSO != null)
//         {
//             activeWeapon.SwitchWeapon(currentWeaponSO);
//         }
//
//         Debug.Log("Weapon data loaded.");
//     }
//
//     private static void ApplyHealthData(HealthData data)
//     {
//         var player = PlayerHealth.Instance;
//         if (player == null)
//         {
//             Debug.LogError("PlayerHealth instance not found.");
//             return;
//         }
//
//         player.currentHealth = data.playerHealth;
//         player.transform.position = data.playerPosition;
//
//         Debug.Log("Health data loaded.");
//     }
// }
