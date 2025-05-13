using System.Collections;
using UnityEngine;

public class SpawnWave : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefabs;
    [SerializeField] float spawnTime = 5f;
    [SerializeField] Transform spawnPoint;


    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            int random = Random.Range(5, 20);
            for(int i = 0; i < random; i++)
            {
                Instantiate(enemyPrefabs, spawnPoint.position, transform.rotation);

            }
            yield return new WaitForSeconds(spawnTime);
        }

    }
}
