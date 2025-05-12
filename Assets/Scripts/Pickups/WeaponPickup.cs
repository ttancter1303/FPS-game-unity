using UnityEngine;

public class WeaponPickup : PickupBase
{
    [SerializeField] WeaponSO weaponSO;
    const string PLAYER_STRING = "Player";

    protected override void OnPickup(ActiveWeapon active)
    {
        //active.SwitchWeapon(weaponSO);
        active.AddWeapon((FirearmWeaponSO)weaponSO);
    }
}
