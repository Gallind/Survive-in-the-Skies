using UnityEngine;
using UnityEngine.EventSystems;

public class UnitPlacer : MonoBehaviour
{
    public GameObject knightPlayerPrefab;
    public LayerMask groundLayer; // Set this in the inspector to your ground layer

    private bool isInPlacementMode = false;

    void Update()
    {
        if (isInPlacementMode)
        {
            // Check for player input to place the unit
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
                {
                    PlaceUnit(hit.point);
                    isInPlacementMode = false;
                }
            }

            // Cancel placement with right-click
            if (Input.GetMouseButtonDown(1))
            {
                isInPlacementMode = false;
            }
        }
    }

    public void StartPlacement()
    {
        if (knightPlayerPrefab != null)
        {
            isInPlacementMode = true;
        }
        else
        {
            Debug.LogError("KnightPlayer prefab is not assigned in the UnitPlacer.");
        }
    }

    private void PlaceUnit(Vector3 position)
    {
        Instantiate(knightPlayerPrefab, position, Quaternion.identity);
    }
}
