using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected LayerMask interactionsLayer;

    public virtual void Attack()
    {

    }
}
