using System;
using UnityEngine;

public class ActiveEnemyWave : MonoBehaviour
{
    [SerializeField] private GameObject trigger;

    private void Awake()
    {
        trigger.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        trigger.SetActive(true);
    }
}
