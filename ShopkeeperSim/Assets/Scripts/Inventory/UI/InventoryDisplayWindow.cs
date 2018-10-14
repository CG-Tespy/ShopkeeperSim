using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayWindow : UIWindowController
{
	[SerializeField] SKSInventory inventoryToDisplay;
	[SerializeField] GameObject slotPrefab;
	[SerializeField] Transform slotHolder;
	
	protected override void Awake()
	{
		base.Awake();

		if (inventoryToDisplay == null)
		{
			string errMessage = "Need an inventory to work with!";
			throw new System.NullReferenceException(errMessage);
		}
	}

	protected virtual void Populate()
	{
		
	}
}
