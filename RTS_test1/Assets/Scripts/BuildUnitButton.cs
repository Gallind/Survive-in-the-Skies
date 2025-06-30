using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildUnitButton : MonoBehaviour
{
    public int foodCost = 10; // Cost to spawn a KnightPlayer

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        UpdateButtonInteractability();
    }

    void Update()
    {
        // Update button interactability every frame (or more efficiently with events)
        UpdateButtonInteractability();
    }

    private void OnButtonClick()
    {
        if (FoodQuotaTimer.Instance != null && UnitSpawner.Instance != null)
        {
            if (FoodQuotaTimer.Instance.RemoveFood(foodCost))
            {
                UnitSpawner.Instance.EnterPlacementMode();
            }
            else
            {
                Debug.Log("Not enough food to spawn KnightPlayer! Need " + foodCost + " food.");
            }
        }
        else
        {
            Debug.LogError("FoodQuotaTimer or UnitSpawner instance not found.");
        }
    }

    private void UpdateButtonInteractability()
    {
        if (FoodQuotaTimer.Instance != null)
        {
            button.interactable = FoodQuotaTimer.Instance.currentFood >= foodCost;
        }
        else
        {
            button.interactable = false; // Disable if FoodQuotaTimer is not available
        }
    }
}
