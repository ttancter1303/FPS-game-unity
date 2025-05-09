using UnityEngine;

[CreateAssetMenu(fileName = "FirearmWeaponSO", menuName = "Scriptable Objects/FirearmWeaponSO")]
public class FirearmWeaponSO : WeaponSO
{

    public float WeaponForce = 20f;
    public float RotationSpeed = 1f;
    public float FireRate = .5f;
    public bool IsAutomatic = false;
    public bool CanZoom = false;
    public float ZoomAmount = 10f;
    public bool CrosshairOff = false;
    public int MagazineSize = 20;

}
