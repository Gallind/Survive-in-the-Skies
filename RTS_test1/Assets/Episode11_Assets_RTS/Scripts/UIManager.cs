using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Building System")]
    public Button buildButton; // The original button for the grid-based system
    public PlacementSystem placement;

    [Header("Unit Spawning")]
    public Button spawnKnightButton; // Your new button for spawning knights
    public UnitPlacer unitPlacer; // The new script to handle unit placement

    private void Start()
    {
        // Existing listener for the building system
        if (buildButton != null && placement != null)
        {
            buildButton.onClick.AddListener(() => Construct(0));
        }

        // New listener for your unit spawning button
        if (spawnKnightButton != null && unitPlacer != null)
        {
            spawnKnightButton.onClick.AddListener(() => unitPlacer.StartPlacement());
        }
    }

    private void Construct(int id)
    {
        Debug.Log("clicked");
        if (placement != null)
        {
            placement.StartPlacement(id);
        }
    }
}
