using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponPickupInteract : Interactable
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] ActiveWeapon activeWeapon;
    Outline outline;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void OnInteract()
    {
        Debug.Log("Play Audio");
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
        activeWeapon.AddWeapon((FirearmWeaponSO)weaponSO);
        Destroy(gameObject);
    }
}
