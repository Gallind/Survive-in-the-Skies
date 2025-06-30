using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class UnitSpawner : MonoBehaviour
{
    public static UnitSpawner Instance { get; private set; }

    public GameObject knightPlayerPrefab;
    public LayerMask groundLayer;

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

            if (Input.GetMouseButtonDown(1)) // Right-click to cancel
            {
                isInPlacementMode = false;
            }
        }
    }

    public void EnterPlacementMode()
    {
        if (knightPlayerPrefab != null)
        {
            isInPlacementMode = true;
        }
        else
        {
            Debug.LogError("KnightPlayer prefab is not assigned in the UnitSpawner.");
        }
    }

    private void PlaceUnit(Vector3 position)
    {
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(position, out navHit, 1.0f, NavMesh.AllAreas))
        {
            Instantiate(knightPlayerPrefab, navHit.position, Quaternion.identity);
        }
        else
        {
             Debug.LogWarning($"Could not find a valid position on the NavMesh near {position}. Spawning at original click position.");
             Instantiate(knightPlayerPrefab, position, Quaternion.identity);
        }
    }
}
