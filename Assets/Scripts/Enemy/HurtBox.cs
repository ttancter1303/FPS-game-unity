using UnityEngine;

public class HurtBox : MonoBehaviour
{
    [SerializeField] EnemyHealth enemy;
    [SerializeField] private float damageMultiplier = 3f;

    public void TakeDamage(float baseDamage)
    {
        float finalDamage = baseDamage * damageMultiplier;
        enemy.TakeDamage(finalDamage);
    }
}
