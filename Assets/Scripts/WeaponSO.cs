using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject HitVFXPrefab;
    public GameObject WeaponPrefab;
    public float WeaponForce = 20f;
    public float RotationSpeed = 1f;
    public int Damage = 1;
    public float FireRate = .5f;
    public bool IsAutomatic = false;
    public bool CanZoom = false;
    public float ZoomAmount = 10f;
    public bool CrosshairOff = false;
}
