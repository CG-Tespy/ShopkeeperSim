using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryDropSpace : UIElementController
{
    //[SerializeField] RectTransform itemHolder;
    [SerializeField] InventoryDisplayWindow displayWindow;
    [SerializeField] MonoBehaviourUI dropArea;

    SKSInventory inventory;

	// Use this for self-initialization
	protected override void Awake () 
	{
        base.Awake();
        inventory =                                             displayWindow.inventoryToDisplay;
	}

    private void Start()
    {
        dropArea.events.Drop.AddListener(OnDropAreaDrop);
    }

    protected virtual void OnDropAreaDrop(PointerEventData eventData)
    {
        // This can work only with InventorySlotUI objects.
        InventorySlotUI slotDropped =           eventData.pointerDrag.GetComponent<InventorySlotUI>();

        if (slotDropped != null)
        {
            // If the slot is of an item from elsewhere, take it into this display window, 
            // provided its underlying inventory has the space for the item.
            SKSItem itemDropped =               slotDropped.itemToDisplay;
            if (!inventory.Contains(itemDropped) && inventory.itemCount < inventory.capacity)
            {
                SKSInventory otherInventory =   itemDropped.belongsTo as SKSInventory;
                if (otherInventory != null)
                    otherInventory.Remove(itemDropped);
                inventory.Add(itemDropped);

                // Destroy the slot; a new one should be made in its place thanks to 
                // the moving around of items.
                Destroy(slotDropped.gameObject);
            }
            else
            {
                // This slot was inside this window, so just put it back where it belongs.
                slotDropped.ResetParent();
            }
        }

        base.OnDrop(eventData);
    }

}
