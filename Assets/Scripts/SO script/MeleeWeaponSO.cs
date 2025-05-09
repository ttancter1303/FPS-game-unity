using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponSO", menuName = "Scriptable Objects/MeleeWeaponSO")]
public class MeleeWeaponSO : WeaponSO
{
    public float AttackRate = 1.0f;
    public float AttackRange = 2f;
    public float WeaponForce = 10f;
}
