using UnityEngine;
using UnityEngine.Events;

public  class PickupInteract : Interactable
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] ActiveWeapon activeWeapon;
    Outline outline;
    AudioSource audioSource;
    public string message;
    public UnityEvent onInteraction;
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
