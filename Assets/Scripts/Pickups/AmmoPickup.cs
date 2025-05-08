using UnityEngine;

public class AmmoPickup : PickupBase
{
    [SerializeField] int ammoAmount = 30;
    protected override void OnPickup(ActiveWeapon active)
    {
        active.AdjustAmmo(ammoAmount);
    }
}
