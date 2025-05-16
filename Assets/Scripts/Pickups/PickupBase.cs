using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 60f;
    const string PLAYER_STRING = "Player";
    private void Update()
    {
        transform.Rotate(0f, Time.deltaTime * rotationSpeed, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            OnPickup(activeWeapon);
            Destroy(gameObject);
        }
    }
    protected abstract void OnPickup(ActiveWeapon active);
}