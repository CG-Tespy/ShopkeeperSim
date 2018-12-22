using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class SKSItemEvent : UnityEvent<SKSItem> {}

[CreateAssetMenu(menuName = "Inventory/New Inventory", fileName = "NewSKSInventory")]
public class SKSInventory : GameInventory<SKSItem>
{
    List<SKSItem> realItems = new List<SKSItem>();

    public override List<SKSItem> items
    {
        get                                         { return realItems; }
        protected set                               { realItems = value; }
    }
    public override bool Add(SKSItem item)
    {
        bool itemAdded =                            base.Add(item);

        if (itemAdded)
        {
            item.belongsTo =                        this;
            ItemAdded.Invoke(item);
            ContentChanged.Invoke();
        }

        return itemAdded;
    }

    public override bool Remove(SKSItem item)
    {
        bool itemRemoved =                          base.Remove(item);

        if (itemRemoved)
        {
            item.belongsTo =                        null;
            ItemRemoved.Invoke(item);
            ContentChanged.Invoke();
        }

        return itemRemoved;
    }

    public override void Clear()
    {
        while (items.Count > 0)
            Remove(items[0]); // Make sure the callbacks get executed for each item
    }

    protected virtual void OnEnable()
	{
        Debug.Log(this.name + " in OnEnable!");
        items.Clear();
        MakeItemsUnique();
        foreach (GameItem item in items)
            item.belongsTo =                        this;
        SetupEvents();
	}

    void SetupEvents()
	{
		ContentChanged = 							new UnityEvent();
		ItemAdded = 								new SKSItemEvent();
		ItemRemoved = 								new SKSItemEvent();
	}

	void MakeItemsUnique()
    {
        // Make sure that each item is a copy of the original scriptable object, 
        // to avoid issues with deciding if an item is in an inventory.
        foreach (SKSItem item in base.items)
        {
            SKSItem itemCopy =                  SKSItem.Copy(item);
            realItems.Add(itemCopy);
        }
    }
}
