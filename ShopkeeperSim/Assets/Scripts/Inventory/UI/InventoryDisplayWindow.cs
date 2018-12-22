using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayWindow : UIWindowController
{
	[SerializeField] SKSInventory _inventoryToDisplay;
	[SerializeField] GameObject slotPrefab;
	[SerializeField] Transform _slotHolder;

	public SKSInventory inventoryToDisplay
	{
		get { return _inventoryToDisplay; }
	}

	public Transform slotHolder 
	{
		get { return _slotHolder; }
	}
	
	protected override void Awake()
	{
		base.Awake();

		if (inventoryToDisplay == null)
		{
			string errMessage = 						"Need an inventory to work with!";
			throw new System.NullReferenceException(errMessage);
		}

		inventoryToDisplay.ContentChanged.AddListener(Refresh);
		Refresh();

        Invoke("Refresh", 1); // For Item-Description Panel
	}

	public override void Refresh()
	{
		Clear();
		Populate();
        base.Refresh();
	}

	protected virtual void Populate()
	{
		// Add an inventory slot display for each item in the inventory, making sure 
		// they display the right things.
		foreach (SKSItem item in inventoryToDisplay.items)
		{
			GameObject newSlot = 						Instantiate<GameObject>(slotPrefab);
			newSlot.name = 								item.name;
			newSlot.transform.SetParent(slotHolder, false);
			InventorySlotUI slotDisplay = 				newSlot.GetComponent<InventorySlotUI>();
			slotDisplay.itemToDisplay = 				item;
		}
	}

	protected virtual void Clear()
	{
		foreach (Transform slot in slotHolder)
			Destroy(slot.gameObject);
	}

}
