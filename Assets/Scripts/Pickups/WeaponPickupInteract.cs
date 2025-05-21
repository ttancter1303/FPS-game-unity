using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponPickupInteract : Interactable
{
    [SerializeField] WeaponSO weaponSO;
    private ActiveWeapon active;
    Outline outline;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void OnInteract()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            active = player.GetComponentInChildren<ActiveWeapon>();
            if (active != null)
            {
                Debug.Log("Play Audio");
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                active.AddWeapon((FirearmWeaponSO)weaponSO);
                Destroy(gameObject);
            }
        }
    }
}
