using System;
using UnityEngine;

public class AmmoPickupInteract : Interactable
{
    [SerializeField] int ammoAmount = 30;
    private ActiveWeapon active;
    private AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    public override void OnInteract()
    {
        if (audio != null && audio.clip != null)
        {
            AudioSource.PlayClipAtPoint(audio.clip, transform.position);
        }

        // Tìm lại ActiveWeapon từ player
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            active = player.GetComponentInChildren<ActiveWeapon>();
            if (active != null)
            {
                active.AdjustAmmo(ammoAmount);
            }
        }
    }


}
