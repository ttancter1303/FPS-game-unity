using System;
using UnityEngine;

public class ActiveEnemyWave : MonoBehaviour
{
    [SerializeField] private GameObject spawnWaveObject;

    private void Awake()
    {
        spawnWaveObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnWaveObject.SetActive(true);
    }
}
