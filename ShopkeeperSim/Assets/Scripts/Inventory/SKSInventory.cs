using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class SKSItemEvent : UnityEvent<SKSItem> {}

public class SKSInventory : GameInventory<SKSItem>
{
	// For creating this as an asset
	static string createPath = 						"Assets/NewSKSINventory.asset";
	[MenuItem("Inventory/Create SKSInventory")]
	static void CreateThis()
	{
		SKSInventory newInv =		 				ScriptableObject.CreateInstance<SKSInventory>();
		AssetDatabase.CreateAsset(newInv, createPath);
	}
	protected virtual void OnEnable()
	{
		SetupEvents();
	}

	void SetupEvents()
	{
		ContentChanged = 							new UnityEvent();
		ItemAdded = 								new SKSItemEvent();
		ItemRemoved = 								new SKSItemEvent();
	}

	
}
