using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponSO", menuName = "Scriptable Objects/MeleeWeaponSO")]
public class MeleeWeaponSO : ScriptableObject
{
    public GameObject WeaponPrefab;
    public GameObject HitVFXPrefab;
    public int Damage = 10;
    public float AttackRate = 1.0f;
    public float AttackRange = 2f;
    public LayerMask HitLayers;
}
