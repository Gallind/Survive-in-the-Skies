
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class GridDataAndHealthTrackerTests
{
    // Tests for GridData
    [Test]
    public void GridData_AddObjectAt_AddsObject()
    {
        // Arrange
        GridData gridData = new GridData();
        Vector3Int gridPosition = new Vector3Int(0, 0, 0);
        Vector2Int objectSize = new Vector2Int(2, 2);
        int id = 1;
        int placedObjectIndex = 0;

        // Act
        gridData.AddObjectAt(gridPosition, objectSize, id, placedObjectIndex);

        // Assert
        Assert.IsFalse(gridData.CanPlaceObjectAt(gridPosition, new Vector2Int(1, 1)));
    }

    [Test]
    public void GridData_AddObjectAt_ThrowsExceptionWhenOccupied()
    {
        // Arrange
        GridData gridData = new GridData();
        Vector3Int gridPosition = new Vector3Int(0, 0, 0);
        Vector2Int objectSize = new Vector2Int(2, 2);
        int id = 1;
        int placedObjectIndex = 0;
        gridData.AddObjectAt(gridPosition, objectSize, id, placedObjectIndex);

        // Act & Assert
        Assert.Throws<System.Exception>(() => gridData.AddObjectAt(gridPosition, objectSize, id, placedObjectIndex));
    }

    [Test]
    public void GridData_CanPlaceObjectAt_ReturnsTrueForEmpty()
    {
        // Arrange
        GridData gridData = new GridData();
        Vector3Int gridPosition = new Vector3Int(0, 0, 0);
        Vector2Int objectSize = new Vector2Int(2, 2);

        // Act & Assert
        Assert.IsTrue(gridData.CanPlaceObjectAt(gridPosition, objectSize));
    }

    [Test]
    public void GridData_CanPlaceObjectAt_ReturnsFalseForOccupied()
    {
        // Arrange
        GridData gridData = new GridData();
        Vector3Int gridPosition = new Vector3Int(0, 0, 0);
        Vector2Int objectSize = new Vector2Int(2, 2);
        int id = 1;
        int placedObjectIndex = 0;
        gridData.AddObjectAt(gridPosition, objectSize, id, placedObjectIndex);

        // Act & Assert
        Assert.IsFalse(gridData.CanPlaceObjectAt(gridPosition, objectSize));
    }

    [Test]
    public void GridData_RemoveObjectAt_RemovesObject()
    {
        // Arrange
        GridData gridData = new GridData();
        Vector3Int gridPosition = new Vector3Int(0, 0, 0);
        Vector2Int objectSize = new Vector2Int(2, 2);
        int id = 1;
        int placedObjectIndex = 0;
        gridData.AddObjectAt(gridPosition, objectSize, id, placedObjectIndex);

        // Act
        gridData.RemoveObjectAt(gridPosition);

        // Assert
        Assert.IsTrue(gridData.CanPlaceObjectAt(gridPosition, objectSize));
    }

    [Test]
    public void GridData_GetRepresentationIndex_ReturnsCorrectIndex()
    {
        // Arrange
        GridData gridData = new GridData();
        Vector3Int gridPosition = new Vector3Int(0, 0, 0);
        Vector2Int objectSize = new Vector2Int(1, 1);
        int id = 1;
        int placedObjectIndex = 5;
        gridData.AddObjectAt(gridPosition, objectSize, id, placedObjectIndex);

        // Act
        int index = gridData.GetRepresentationIndex(gridPosition);

        // Assert
        Assert.AreEqual(placedObjectIndex, index);
    }

    [Test]
    public void GridData_GetRepresentationIndex_ReturnsMinusOneForEmpty()
    {
        // Arrange
        GridData gridData = new GridData();
        Vector3Int gridPosition = new Vector3Int(0, 0, 0);

        // Act
        int index = gridData.GetRepresentationIndex(gridPosition);

        // Assert
        Assert.AreEqual(-1, index);
    }

    // Tests for HealthTracker
    [Test]
    public void HealthTracker_UpdateSliderValue_CalculatesCorrectPercentage()
    {
        // Arrange
        GameObject testObject = new GameObject();
        HealthTracker healthTracker = testObject.AddComponent<HealthTracker>();
        Slider slider = testObject.AddComponent<Slider>();
        healthTracker.HealthBarSlider = slider;
        Image image = new GameObject().AddComponent<Image>();
        healthTracker.sliderFill = image;
        healthTracker.greenEmission = new Material(Shader.Find("Standard"));
        healthTracker.yellowEmission = new Material(Shader.Find("Standard"));
        healthTracker.redEmission = new Material(Shader.Find("Standard"));


        // Act
        healthTracker.UpdateSliderValue(50, 100);

        // Assert
        Assert.AreEqual(0.5f, slider.value, 0.01f);
    }

    [Test]
    public void HealthTracker_UpdateColor_SetsGreen()
    {
        // Arrange
        GameObject testObject = new GameObject();
        HealthTracker healthTracker = testObject.AddComponent<HealthTracker>();
        Slider slider = testObject.AddComponent<Slider>();
        healthTracker.HealthBarSlider = slider;
        Image image = new GameObject().AddComponent<Image>();
        healthTracker.sliderFill = image;
        Material green = new Material(Shader.Find("Standard"));
        healthTracker.greenEmission = green;
        healthTracker.yellowEmission = new Material(Shader.Find("Standard"));
        healthTracker.redEmission = new Material(Shader.Find("Standard"));

        // Act
        healthTracker.UpdateSliderValue(70, 100);

        // Assert
        Assert.AreEqual(green, image.material);
    }

    [Test]
    public void HealthTracker_UpdateColor_SetsYellow()
    {
        // Arrange
        GameObject testObject = new GameObject();
        HealthTracker healthTracker = testObject.AddComponent<HealthTracker>();
        Slider slider = testObject.AddComponent<Slider>();
        healthTracker.HealthBarSlider = slider;
        Image image = new GameObject().AddComponent<Image>();
        healthTracker.sliderFill = image;
        Material yellow = new Material(Shader.Find("Standard"));
        healthTracker.greenEmission = new Material(Shader.Find("Standard"));
        healthTracker.yellowEmission = yellow;
        healthTracker.redEmission = new Material(Shader.Find("Standard"));

        // Act
        healthTracker.UpdateSliderValue(45, 100);

        // Assert
        Assert.AreEqual(yellow, image.material);
    }

    [Test]
    public void HealthTracker_UpdateColor_SetsRed()
    {
        // Arrange
        GameObject testObject = new GameObject();
        HealthTracker healthTracker = testObject.AddComponent<HealthTracker>();
        Slider slider = testObject.AddComponent<Slider>();
        healthTracker.HealthBarSlider = slider;
        Image image = new GameObject().AddComponent<Image>();
        healthTracker.sliderFill = image;
        Material red = new Material(Shader.Find("Standard"));
        healthTracker.greenEmission = new Material(Shader.Find("Standard"));
        healthTracker.yellowEmission = new Material(Shader.Find("Standard"));
        healthTracker.redEmission = red;

        // Act
        healthTracker.UpdateSliderValue(20, 100);

        // Assert
        Assert.AreEqual(red, image.material);
    }
}
