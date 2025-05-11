using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public enum SlotType { Hat, Torso, Legs, Inventory, TrashCan }
    public SlotType slotType;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryController inventoryController = FindFirstObjectByType<InventoryController>();

        if (transform.childCount > 0)
        {
            InventoryItem existingItem = transform.GetChild(0).GetComponent<InventoryItem>();
            InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();

            if (existingItem != null && newItem != null && existingItem.item.slotType == newItem.item.slotType)
            {
                return; 
            }
        }

        if (transform.childCount == 0)
        {
            inventoryController.UpdateSprite(slotType, null);
        }

        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (inventoryItem.parentAfterDrag != null && inventoryItem.parentAfterDrag != transform)
        {
            InventorySlot previousSlot = inventoryItem.parentAfterDrag.GetComponent<InventorySlot>();
            if (previousSlot != null)
            {
                inventoryController.UpdateSprite(previousSlot.slotType, null);
            }
        }

        if (inventoryItem.item.slotType == (Item.SlotType)slotType || slotType == SlotType.Inventory)
        {
            inventoryItem.parentAfterDrag = transform;
            inventoryController.UpdateSprite(slotType, inventoryItem.item.spriteLibrary);
        }

        if (slotType == SlotType.TrashCan)
        {
            inventoryController.SellItem(inventoryItem.item);
            Destroy(inventoryItem.gameObject);
        }
    }
}
