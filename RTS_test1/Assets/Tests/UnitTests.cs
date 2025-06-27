
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class UnitTests
{
    [Test]
    public void Unit_TakeDamage_ReducesHP()
    {
        // Arrange
        GameObject unitObject = new GameObject();
        Unit unit = unitObject.AddComponent<Unit>();

        // Mock HealthTracker
        GameObject healthTrackerObject = new GameObject();
        HealthTracker healthTracker = healthTrackerObject.AddComponent<HealthTracker>();
        Slider slider = healthTrackerObject.AddComponent<Slider>();
        healthTracker.HealthBarSlider = slider;
        Image image = new GameObject().AddComponent<Image>();
        healthTracker.sliderFill = image;
        healthTracker.greenEmission = new Material(Shader.Find("Standard"));
        healthTracker.yellowEmission = new Material(Shader.Find("Standard"));
        healthTracker.redEmission = new Material(Shader.Find("Standard"));
        unit.healthTracker = healthTracker;

        // Mock UnitSelectionManager
        GameObject usmObject = new GameObject();
        usmObject.AddComponent<UnitSelectionManager>();

        unit.unitMaxHP = 100;
        int damage = 30;

        // We need to manually call Start() to initialize the unit's HP
        // In a real game, Unity calls this. In a test, we do it ourselves.
        // However, the Start() method in Unit.cs adds the unit to a list in a singleton, which can be problematic in tests.
        // For this test, we will manually set the HP and then call TakeDamage.
        // In a more complex scenario, you might refactor the Start method to be more test-friendly.

        // Let's simulate the state after Start() would have run
        // unit.unitHP = unit.unitMaxHP; // This is private, so we can't set it directly. We will test the public method.

        // Act
        unit.TakeDamage(damage);

        // Assert
        // We can't directly access unitHP, so we will infer the result from what we can see.
        // In this case, we can check the value passed to the health tracker.
        Assert.AreEqual(0.7f, healthTracker.HealthBarSlider.value, 0.01f);
    }
}
