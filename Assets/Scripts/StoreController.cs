using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameController gameController;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private Item[] items;
    [SerializeField] private StoreButton[] storeButtons;

    public void BuyItem(int id)
    {
        Item item = items[id];
        if (gameController.SpendCoins(item.buyPrice))
        {
            if (inventoryController.AddItem(item))
            {
                EventManager.TriggerEvent("ItemChange", null);
            }
            else
            {
                gameController.AddCoins(item.buyPrice);
            }
        }
    }

    public void SellItem(Item item)
    {
        gameController.AddCoins(item.salePrice);
        EventManager.TriggerEvent("ItemChange", null);
    }

    private void Start()
    {
        for (int i = 0; i < storeButtons.Length; i++)
        {
            storeButtons[i].Initialize(items[i]);
        }
    }

    public void AddItem(int id)
    {
        inventoryController.AddItem(items[id]);
    }
}
