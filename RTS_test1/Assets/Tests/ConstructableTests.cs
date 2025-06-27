
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class ConstructableTests
{
    [Test]
    public void ConstructableWasPlaced_ActivatesNavMeshObstacle()
    {
        // Arrange
        GameObject parentObject = new GameObject("ConstructableObject");
        Constructable constructable = parentObject.AddComponent<Constructable>();

        GameObject childObject = new GameObject("ObstacleObject");
        childObject.transform.SetParent(parentObject.transform);

        NavMeshObstacle obstacle = childObject.AddComponent<NavMeshObstacle>();
        obstacle.enabled = false;

        // Act
        constructable.ConstructableWasPlaced();

        // Assert
        Assert.IsTrue(obstacle.enabled);
    }
}
