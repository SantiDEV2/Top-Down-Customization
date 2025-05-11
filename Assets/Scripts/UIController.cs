using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private Canvas storeCanvas;

    [Header("Store Sign")]
    [SerializeField] private SpriteRenderer signRenderer;
    [SerializeField] private Sprite exclamationImage;
    [SerializeField] private Sprite eImage;

    private void OnEnable()
    {
        EventManager.StartListening("InventoryOpen", ShowInventory);
        EventManager.StartListening("InventoryClose", HideInventory);
        EventManager.StartListening("StoreOpen", ShowStore);
        EventManager.StartListening("StoreClose", HideStore);
        EventManager.StartListening("EntryStore", EntryStore);
        EventManager.StartListening("ExitStore", ExitStore);
    }
    private void OnDisable()
    {
        EventManager.StopListening("InventoryOpen", ShowInventory);
        EventManager.StopListening("InventoryClose", HideInventory);
        EventManager.StopListening("StoreOpen", ShowStore);
        EventManager.StopListening("StoreClose", HideStore);
        EventManager.StopListening("EntryStore", EntryStore);
        EventManager.StopListening("ExitStore", ExitStore);
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
    void ShowStore(object param)
    {
        GameController.isGamePaused = true;
        storeCanvas.enabled = true;
    }
    void HideStore(object param)
    {
        GameController.isGamePaused = false;
        storeCanvas.enabled = false;
    }

    void EntryStore(object param)
    {
        signRenderer.sprite = eImage;
    }
    void ExitStore(object param)
    {
        signRenderer.sprite = exclamationImage;
    }

}
