using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AttackController))]
public class Enemy : MonoBehaviour
{
    // Health and UI
    public float maxHP = 100f;
    private float currentHP;
    public HealthTracker healthTracker;

    // AI
    public float targetSearchInterval = 0.2f;

    // Components
    private NavMeshAgent agent;
    private Animator animator;
    private AttackController attackController;
    private Transform targetPlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attackController = GetComponent<AttackController>();

        currentHP = maxHP;
        if (healthTracker != null)
        {
            healthTracker.UpdateSliderValue(currentHP, maxHP);
        }

        StartCoroutine(FindClosestPlayerRoutine());
    }

    void Update()
    {
        // Animation
        if (animator != null)
        {
            animator.SetBool("isMove", agent.velocity.magnitude > 0.1f);
        }

        if (targetPlayer != null)
        {
            float distance = Vector3.Distance(transform.position, targetPlayer.position);

            if (distance <= agent.stoppingDistance)
            {
                // Arrived at target, look at it
                Vector3 direction = (targetPlayer.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            // Let the AttackController handle the attack
            attackController.PerformAttack(targetPlayer);

            // Move towards the target if not in attack range
            if (distance > attackController.attackRange)
            {
                agent.SetDestination(targetPlayer.position);
            }
        }
    }

    IEnumerator FindClosestPlayerRoutine()
    {
        while (true)
        {
            FindClosestPlayer();
            yield return new WaitForSeconds(targetSearchInterval);
        }
    }

    void FindClosestPlayer()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestPlayer = null;

        if (UnitSelectionManager.Instance != null && UnitSelectionManager.Instance.allUnitsList.Count > 0)
        {
            foreach (GameObject playerUnit in UnitSelectionManager.Instance.allUnitsList)
            {
                if (playerUnit != null)
                {
                    float distance = Vector3.Distance(transform.position, playerUnit.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPlayer = playerUnit;
                    }
                }
            }
        }

        if (closestPlayer != null)
        {
            targetPlayer = closestPlayer.transform;
            agent.SetDestination(targetPlayer.position);
        }
        else
        {
            targetPlayer = null;
            agent.ResetPath();
        }
    }

    public void ReceiveDamage(int damage)
    {
        currentHP -= damage;
        if (healthTracker != null)
        {
            healthTracker.UpdateSliderValue(currentHP, maxHP);
        }

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
