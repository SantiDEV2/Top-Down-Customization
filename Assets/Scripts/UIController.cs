using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Canvas inventoryCanvas;

    private void OnEnable()
    {
        EventManager.StartListening("InventoryOpen", ShowInventory);
        EventManager.StartListening("InventoryClose", HideInventory);
    }
    private void OnDisable()
    {
        EventManager.StopListening("InventoryOpen", ShowInventory);
        EventManager.StopListening("InventoryClose", HideInventory);
    }

    void ShowInventory(object param)
    {
        GameController.isGamePaused = true;
        inventoryCanvas.enabled = true;
    }

    void HideInventory(object param)
    {
        GameController.isGamePaused = false;
        inventoryCanvas.enabled = false;
    }
}
