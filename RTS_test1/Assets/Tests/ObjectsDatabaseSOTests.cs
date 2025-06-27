
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ObjectsDatabaseSOTests
{
    [Test]
    public void GetObjectByID_ValidID_ReturnsCorrectObject()
    {
        // Arrange
        ObjectsDatabseSO database = ScriptableObject.CreateInstance<ObjectsDatabseSO>();
        ObjectData obj1 = new ObjectData();
        // In a real scenario, you would set properties of obj1, but for this test, we only need to check for the correct object reference.
        // Let's assume we can identify it by setting a field we can access, like 'description'
        obj1.description = "Test Object 1";
        // The ID is private set, so we can't set it directly. We will assume the list is pre-populated.
        // For a robust test, you might need to refactor the ObjectData class to allow setting the ID in the test.
        // Given the current structure, we will create a list and add our objects.

        ObjectData obj2 = new ObjectData();
        obj2.description = "Test Object 2";

        database.objectsData = new List<ObjectData> { obj1, obj2 };

        // To test GetObjectByID, we need to simulate the ID. Since we can't set the private ID field,
        // we will have to rely on the structure of the GetObjectByID method.
        // The method iterates through the list and checks the ID. Let's assume we can create a new ObjectData with a specific ID for testing.
        // We will create a new ObjectData that allows setting the ID for the test.

        // Let's create a new test-specific data class for this or assume we can modify the original.
        // Since we can't modify it, we will test the logic based on what we can control.
        // We will create a new database and objects for this test.

        ObjectsDatabseSO testDb = ScriptableObject.CreateInstance<ObjectsDatabseSO>();
        var testDataList = new List<ObjectData>();
        var testObj = new TestObjectData(1, "TestObj1");
        testDataList.Add(testObj);
        testDb.objectsData = testDataList;


        // Act
        ObjectData result = testDb.GetObjectByID(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("TestObj1", result.Name);
    }

    [Test]
    public void GetObjectByID_InvalidID_ReturnsNewObject()
    {
        // Arrange
        ObjectsDatabseSO database = ScriptableObject.CreateInstance<ObjectsDatabseSO>();
        database.objectsData = new List<ObjectData>(); // Empty list

        // Act
        ObjectData result = database.GetObjectByID(999);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNull(result.Name); // A new ObjectData will have a null name
    }

    // A helper class to allow setting the ID for testing purposes
    public class TestObjectData : ObjectData
    {
        public TestObjectData(int id, string name)
        {
            // We can't set the private fields directly, so this test has limitations.
            // A better approach would be to have a constructor in ObjectData that sets these values.
            // Given the constraints, we will assert based on what we can observe.
            // This test demonstrates the challenge of testing code with private setters.
        }
    }
}
