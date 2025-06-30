using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    public GameObject knightPlayerPrefab;
    public LayerMask groundLayer; // Set this in the inspector to your ground layer

    private bool isInPlacementMode = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (isInPlacementMode)
        {
            // Check for player input to place the building
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
                {
                    PlaceBuilding(hit.point);
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

    public void StartPlacementMode()
    {
        if (knightPlayerPrefab != null)
        {
            isInPlacementMode = true;
            // Here you could also activate a placement ghost/indicator
        }
        else
        {
            Debug.LogError("KnightPlayer prefab is not assigned in the BuildingManager.");
        }
    }

    private void PlaceBuilding(Vector3 position)
    {
        Instantiate(knightPlayerPrefab, position, Quaternion.identity);
    }
}
