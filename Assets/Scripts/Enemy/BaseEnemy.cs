using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 30;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float alertRange = 6f;
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float wanderRadius = 15f;
    [SerializeField] float wanderDelay = 5f;
    [SerializeField] float attackDelay = 1f;

    FirstPersonController player;
    const string PLAYER_STRING = "Player";
    Animator animator;
    public NavMeshAgent agent;
    const string RUN_STRING = "Run";
    const string WALK_STRING = "WALK";
    const string ATTACK_STRING = "Attack";
    private string currentState = "";
    private Vector3 wanderTarget;
    private float wanderTimer;
    private bool isAttacking = false;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindFirstObjectByType<FirstPersonController>();
        wanderTimer = wanderDelay;

    }


    void Update()
    {
        if (!player) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= chaseRange)
        {
            // Đuổi theo player
            agent.SetDestination(player.transform.position);
            SetAnimationState(RUN_STRING);
        }
        else
        {
            // Đi lang thang
            Wander();
        }
    }
    void Wander()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderDelay || agent.remainingDistance < 1f)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            wanderTimer = 0;
        }

        SetAnimationState(WALK_STRING);
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        if (NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask))
        {
            return navHit.position;
        }

        return origin; // fallback nếu không tìm được vị trí hợp lệ
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag(PLAYER_STRING))
        //{
        //    EnemyHealth enemy = GetComponent<EnemyHealth>();
        //    enemy.SelfDestruct();
        //}
        Attack();
    }
    void Attack()
    {
        if (player == null || animator == null || isAttacking) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                isAttacking = true; // Đánh dấu đang tấn công
                animator.SetTrigger(ATTACK_STRING);
                StartCoroutine(DealDamageWithDelay(playerHealth));
                break;
            }
        }
    }
    IEnumerator DealDamageWithDelay(PlayerHealth target)
    {
        yield return new WaitForSeconds(attackDelay); // Delay khớp với thời điểm đòn đánh chạm trong animation
        if (target == null) yield break;
        if (Vector3.Distance(transform.position, target.transform.position) <= radius)
        {
            target.TakeDamage(damage);
        }

        yield return new WaitForSeconds(1f); // Thời gian giữa 2 lần đánh
        isAttacking = false;
    }
    void SetAnimationState(string state)
    {
        if (state == currentState) return;

        // Reset các trigger cũ
        animator.ResetTrigger(WALK_STRING);
        animator.ResetTrigger(RUN_STRING);
        animator.ResetTrigger(ATTACK_STRING);

        animator.SetTrigger(state);
        currentState = state;
    }

}
