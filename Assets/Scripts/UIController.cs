using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("References")]
    public Canvas inventoryCanvas;
    public Canvas storeCanvas;
    public PlayerController player;

    [Header("Store Sign")]
    public SpriteRenderer signRenderer1;
    public SpriteRenderer signRenderer2;
    public Sprite exclamationImage;
    public Sprite eImage;
    public Sprite iImage;

    [Header("UI Variables")]
    public TextMeshProUGUI coinTextStore;
    public TextMeshProUGUI coinTextInventory;

    private void OnEnable()
    {
        EventManager.StartListening("EntryCloset", EntryCloset);
        EventManager.StartListening("ExitCloset", ExitCloset);
        EventManager.StartListening("InventoryOpen", ShowInventory);
        EventManager.StartListening("InventoryClose", HideInventory);
        EventManager.StartListening("StoreOpen", ShowStore);
        EventManager.StartListening("StoreClose", HideStore);
        EventManager.StartListening("EntryStore", EntryStore);
        EventManager.StartListening("ExitStore", ExitStore);
        EventManager.StartListening("UpdateCoins", UpdateCoinUI);
    }
    private void OnDisable()
    {
        EventManager.StopListening("EntryCloset", EntryCloset);
        EventManager.StopListening("ExitCloset", ExitCloset);
        EventManager.StopListening("InventoryOpen", ShowInventory);
        EventManager.StopListening("InventoryClose", HideInventory);
        EventManager.StopListening("StoreOpen", ShowStore);
        EventManager.StopListening("StoreClose", HideStore);
        EventManager.StopListening("EntryStore", EntryStore);
        EventManager.StopListening("ExitStore", ExitStore);
        EventManager.StopListening("UpdateCoins", UpdateCoinUI);
    }

    private void Start()
    {
        coinTextInventory.text = player.coins.ToString();
        coinTextStore.text = player.coins.ToString();
    }

    public void UpdateCoinUI(object param)
    {
        int coins = (int)param;
        coinTextStore.text = coins.ToString();
        coinTextInventory.text = coins.ToString();
    }

    void EntryCloset(object param)
    {
        signRenderer1.sprite = iImage;
    }

    void ExitCloset(object param)
    {
        signRenderer1.sprite = exclamationImage;
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
        signRenderer2.sprite = eImage;
    }
    void ExitStore(object param)
    {
        signRenderer2.sprite = exclamationImage;
    }

}
