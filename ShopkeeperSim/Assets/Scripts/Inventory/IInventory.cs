using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInventory
{

}

/// <typeparam name="T">What the inventory can hold.</typeparam>
public interface IInventory<T> : IInventory
{
	bool Add(T toAdd);
	bool Remove(T toRemove);
	void Clear();
}
