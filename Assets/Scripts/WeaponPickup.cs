using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    const string PLAYER_STRING = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(PLAYER_STRING))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            activeWeapon.SwitchWeapon(weaponSO);
            Destroy(gameObject);
        }
    }

}
