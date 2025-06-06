﻿using StarterAssets;
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
    public bool isProvoked = false;

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

        if (isProvoked || distanceToPlayer <= chaseRange)
        {
            Chase();
        }
        else
        {
            // Đi lang thang
            Wander();
        }
    }

    public void Chase()
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.speed = 2f;
            agent.SetDestination(player.transform.position);
            SetAnimationState(RUN_STRING);
        }
        else
        {
            Debug.LogWarning("NavMeshAgent chưa sẵn sàng để chạy Chase()");
        }
    }
    public void OnAttackFinished()
    {
        
        isAttacking = false;
        Debug.Log(currentState);
        Debug.Log(isAttacking);
        
        SetAnimationState(RUN_STRING); // chuyển lại animation chạy
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
        yield return new WaitForSeconds(attackDelay);
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= radius)
        {
            target.TakeDamage(damage);
        }

        yield return new WaitForSeconds(1f);
        isAttacking = false;

        // Chuyển về chạy nếu đang trong chế độ chase
        if (Vector3.Distance(transform.position, player.transform.position) <= chaseRange)
        {
            SetAnimationState(RUN_STRING);
        }
    }

    void SetAnimationState(string state)
    {
        if (state == currentState) return;
        
        animator.ResetTrigger(ATTACK_STRING);

        switch (state)
        {
            case RUN_STRING:
                animator.SetBool(RUN_STRING, true);
                animator.SetBool(WALK_STRING, false);
                break;
            case WALK_STRING:
                animator.SetBool(RUN_STRING, false);
                animator.SetBool(WALK_STRING, true);
                break;
            case ATTACK_STRING:
                animator.SetTrigger(ATTACK_STRING);
                break;
        }

        currentState = state;
    }


}
