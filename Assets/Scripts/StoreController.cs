using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private Item[] items;
    [SerializeField] private StoreButton[] storeButtons;

    private void Start()
    {
        for (int i = 1; i < storeButtons.Length; i++)
        {
            storeButtons[i].Initialize(items[i]);
        }
    }

    public void AddItem(int id)
    {
        inventoryController.AddItem(items[id]);
    }
}
