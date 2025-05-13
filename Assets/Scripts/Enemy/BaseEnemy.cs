using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    FirstPersonController player;
    const string PLAYER_STRING = "Player";
    Animator animator;
    NavMeshAgent agent;
    const string RUN_STRING = "Run";
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindFirstObjectByType<FirstPersonController>();

    }


    void Update()
    {
        if (!player) return;
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
