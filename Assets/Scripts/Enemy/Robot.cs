using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    FirstPersonController player;
    const string PLAYER_STRING = "Player";

    NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    void Start()
    {
        if(!player) return;
        player = FindFirstObjectByType<FirstPersonController>();

    }


    void Update()
    {
        agent.SetDestination(player.transform.position);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            EnemyHealth enemy = GetComponent<EnemyHealth>();
            enemy.SelfDestruct();
        }
    }
}
