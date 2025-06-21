using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform targetToAttack;
    public Material idleStateMaterial, followStateMaterial, AttackStateMaterial;
    public int unitDamage, unitHP;

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
        GetComponent<Renderer>().material = idleStateMaterial;
    }
    public void SetFollowMaterial() {
        GetComponent<Renderer>().material = followStateMaterial;
    }
    public void SetAttackMaterial()
    {
        GetComponent<Renderer>().material = AttackStateMaterial;
    }
}
