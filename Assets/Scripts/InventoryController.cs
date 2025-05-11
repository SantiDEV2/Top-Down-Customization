using UnityEngine;
using UnityEngine.U2D.Animation;

public class InventoryController : MonoBehaviour
{
    [Header("References")]
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    [Header("Sprite Library")]
    [SerializeField] private SpriteLibrary hatLibrary;
    [SerializeField] private SpriteLibrary torsoLibrary;
    [SerializeField] private SpriteLibrary legsLibrary;

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    public void SellItem(Item item)
    {
        StoreController storeController = FindFirstObjectByType<StoreController>();
        storeController.SellItem(item);
    }

    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public void UpdateSprite(InventorySlot.SlotType slotType, SpriteLibraryAsset spriteLibraryAsset)
    {
        SpriteRenderer spriteRenderer = null;

        switch (slotType)
        {
            case InventorySlot.SlotType.Hat:
                hatLibrary.spriteLibraryAsset = spriteLibraryAsset;
                hatLibrary.RefreshSpriteResolvers();
                spriteRenderer = hatLibrary.GetComponent<SpriteRenderer>();
                break;
            case InventorySlot.SlotType.Torso:
                torsoLibrary.spriteLibraryAsset = spriteLibraryAsset;
                torsoLibrary.RefreshSpriteResolvers();
                spriteRenderer = torsoLibrary.GetComponent<SpriteRenderer>();
                break;
            case InventorySlot.SlotType.Legs:
                legsLibrary.spriteLibraryAsset = spriteLibraryAsset;
                legsLibrary.RefreshSpriteResolvers();
                spriteRenderer = legsLibrary.GetComponent<SpriteRenderer>();
                break;
        }
        if (spriteRenderer != null && spriteLibraryAsset == null)
        {
            spriteRenderer.sprite = null;
        }
    }
}
