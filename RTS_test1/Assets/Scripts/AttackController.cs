using UnityEngine;

public class AttackController : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;

    [Header("Player-specific")]
    public Transform targetToAttack; // Used by player trigger logic
    public Material idleStateMaterial, followStateMaterial, AttackStateMaterial;

    private float lastAttackTime;

    // This method can be called by other scripts (like AI) to perform an attack
    public void PerformAttack(Transform target)
    {
        if (Time.time - lastAttackTime < attackCooldown || target == null)
        {
            return; // Exit if on cooldown or no target
        }

        // Check if target is in range
        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            lastAttackTime = Time.time;

            // Try to damage a player Unit
            Unit targetUnit = target.GetComponent<Unit>();
            if (targetUnit != null)
            {
                targetUnit.TakeDamage(attackDamage);
                return;
            }

            // Try to damage an Enemy
            Enemy targetEnemy = target.GetComponent<Enemy>();
            if (targetEnemy != null)
            {
                targetEnemy.ReceiveDamage(attackDamage);
            }
        }
    }


    // --- Existing Player-specific Trigger Logic ---
    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Enemy") && other.CompareTag("Enemy") && targetToAttack == null)
        {
            targetToAttack = other.transform;
        }
    }
    private void OnTriggerStay(Collider other) {
        if (!gameObject.CompareTag("Enemy") && other.CompareTag("Enemy") && targetToAttack == null)
        {
            targetToAttack = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!gameObject.CompareTag("Enemy") && other.CompareTag("Enemy") && targetToAttack != null)
        {
            targetToAttack = null;
        }
    }

    public void SetIdleMaterial()
    {
        //GetComponent<Renderer>().material = idleStateMaterial;
    }
    public void SetFollowMaterial() {
        //GetComponent<Renderer>().material = followStateMaterial;
    }
    public void SetAttackMaterial()
    {
        //GetComponent<Renderer>().material = AttackStateMaterial;
    }
}
