using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(menuName = "Inventory/Items/New Item", fileName = "NewItem")]
public class SKSItem : GameItem
{

	// Fields
	[SerializeField] protected float _price = 			10;
	[TextArea(2, 5)]
	[SerializeField] protected string _description;
	

	// Properties
	public string description
	{
		get { return _description; }
		set { _description = value; }
	}

	public float price
	{
		get 											{ return _price; }
		protected set 									{ _price = value; }
	}
    
	public static SKSItem Copy(SKSItem original)
    {
        SKSItem itemCopy =                  ScriptableObject.CreateInstance<SKSItem>();
        CopyBaseAttributes(original, itemCopy);
        itemCopy.price =                    original.price;
        itemCopy.description =              original.description;

        return itemCopy;
    }
    
}
