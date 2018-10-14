using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class SKSItem : GameItem
{
	// For creating as an asset in Unity 5.0
	static string createPath = 							"Assets/NewSKSItem.asset";
	[MenuItem("Inventory/Items/Create SKSItem")]
	static void CreateThis()
	{
		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<SKSItem>(), createPath);
	}

	// Fields
	[SerializeField] protected int _price = 			10;
	[TextArea(2, 5)]
	[SerializeField] protected string _description;
	

	// Properties
	public string description
	{
		get { return _description; }
		set { _description = value; }
	}

	public int price
	{
		get 											{ return _price; }
		protected set 									{ _price = value; }
	}
	
}
