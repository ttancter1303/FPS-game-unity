using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public Sprite weaponIcon;
    public GameObject HitVFXPrefab;
    public GameObject WeaponPrefab;
    public string WeaponName;
    public int Damage = 1;
}
