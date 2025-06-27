
using UnityEngine;
using NUnit.Framework;

public class AttackControllerTests
{
    [Test]
    public void AttackController_CanBeAddedToGameObject()
    {
        // Arrange
        GameObject go = new GameObject();

        // Act
        AttackController controller = go.AddComponent<AttackController>();

        // Assert
        Assert.IsNotNull(controller);
    }
}
