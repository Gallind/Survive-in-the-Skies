using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour
{
    public Sprite availableSprite, unAvailableSprite;
    public bool isAvailable;

    void Start()
    {
        UpdateAvailabilityUI();
    }

    private void UpdateAvailabilityUI()
    {
        if (isAvailable)
        {
            GetComponent<Image>().sprite = availableSprite;
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Image>().sprite = unAvailableSprite;
            GetComponent<Button>().interactable = false;
        }
    }

    // This method will be called by the OnClick event of the button
    public void OnBuildButtonClick()
    {
        if (isAvailable && BuildingManager.Instance != null)
        {
            BuildingManager.Instance.StartPlacementMode();
        }
    }
}
