using UnityEngine;

public class WeaponPickup : PickupBase
{
    [SerializeField] WeaponSO weaponSO;
    const string PLAYER_STRING = "Player";
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    protected override void OnPickup(ActiveWeapon active)
    {
        Debug.Log("Play Audio");
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
        active.AddWeapon((FirearmWeaponSO)weaponSO);
    }
}
