using UnityEngine;

public class BuildUnitButton : MonoBehaviour
{
    // This method should be linked to the OnClick event of your button in the Unity Inspector.
    public void TriggerBuildMode()
    {
        if (BuildingManager.Instance != null)
        {
            BuildingManager.Instance.StartPlacementMode();
        }
        else
        {
            Debug.LogError("BuildingManager instance not found. Make sure a BuildingManager is active in the scene.");
        }
    }
}
