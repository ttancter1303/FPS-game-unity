using UnityEngine;
using UnityEngine.Events;

public abstract class PickupBase : MonoBehaviour
{
    public string message;
    public UnityEvent pickup;
    const string PLAYER_STRING = "Player";

    void Start()
    {
        HandleEvent();
    }

    public void HandleEvent()
    {
        ActiveWeapon activeWeapon = GetComponentInChildren<ActiveWeapon>();
        if (activeWeapon != null)
        {
            pickup.Invoke();
            OnPickup(activeWeapon);
            Destroy(gameObject); 
        }
        else
        {
            Debug.LogError("Không tìm thấy ActiveWeapon trong scene.");
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(PLAYER_STRING))
    //    {
    //        ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
    //        pickup.Invoke();
    //        OnPickup(activeWeapon);
    //        Destroy(activeWeapon);
    //    }
    //}

    protected abstract void OnPickup(ActiveWeapon active);
}
