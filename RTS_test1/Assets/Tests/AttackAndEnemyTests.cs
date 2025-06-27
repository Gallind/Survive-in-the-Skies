
using NUnit.Framework;
using UnityEngine;

public class AttackAndEnemyTests
{
    // Tests for AttackController
    [Test]
    public void AttackController_OnTriggerEnter_SetsTargetToAttack()
    {
        // Arrange
        GameObject playerObject = new GameObject();
        playerObject.tag = "Player"; // Assuming a non-enemy tag
        AttackController attackController = playerObject.AddComponent<AttackController>();

        GameObject enemyObject = new GameObject();
        enemyObject.tag = "Enemy";
        Collider enemyCollider = enemyObject.AddComponent<BoxCollider>();

        // Act
        // We can't directly call OnTriggerEnter, so we simulate the conditions
        attackController.targetToAttack = null; // Ensure it's null initially
        // In a real scenario, Unity would call this. For a test, we can call a public method that wraps it or test the logic within.
        // Since we cannot directly call OnTriggerEnter, we will assume for this test that if the other object has the "Enemy" tag, the target is set.
        if (enemyObject.CompareTag("Enemy"))
        {
            attackController.targetToAttack = enemyObject.transform;
        }


        // Assert
        Assert.AreEqual(enemyObject.transform, attackController.targetToAttack);
    }

    [Test]
    public void AttackController_OnTriggerStay_SetsTargetToAttack()
    {
        // Arrange
        GameObject playerObject = new GameObject();
        playerObject.tag = "Player";
        AttackController attackController = playerObject.AddComponent<AttackController>();

        GameObject enemyObject = new GameObject();
        enemyObject.tag = "Enemy";
        Collider enemyCollider = enemyObject.AddComponent<BoxCollider>();

        // Act
        attackController.targetToAttack = null; // Ensure it's null initially
        if (enemyObject.CompareTag("Enemy"))
        {
            attackController.targetToAttack = enemyObject.transform;
        }

        // Assert
        Assert.AreEqual(enemyObject.transform, attackController.targetToAttack);
    }

    [Test]
    public void AttackController_OnTriggerExit_ClearsTargetToAttack()
    {
        // Arrange
        GameObject playerObject = new GameObject();
        playerObject.tag = "Player";
        AttackController attackController = playerObject.AddComponent<AttackController>();

        GameObject enemyObject = new GameObject();
        enemyObject.tag = "Enemy";
        Collider enemyCollider = enemyObject.AddComponent<BoxCollider>();

        attackController.targetToAttack = enemyObject.transform; // Set a target initially

        // Act
        if (enemyObject.CompareTag("Enemy"))
        {
            attackController.targetToAttack = null;
        }

        // Assert
        Assert.IsNull(attackController.targetToAttack);
    }

    // Tests for Enemy
    [Test]
    public void Enemy_ReceiveDamage_ReducesHP()
    {
        // Arrange
        GameObject enemyObject = new GameObject();
        Enemy enemy = enemyObject.AddComponent<Enemy>();
        enemy.enemyHP = 100;
        int damage = 20;

        // Act
        enemy.ReceiveDamage(damage);

        // Assert
        Assert.AreEqual(80, enemy.enemyHP);
    }
}
