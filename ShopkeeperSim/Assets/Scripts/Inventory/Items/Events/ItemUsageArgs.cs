using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Type of event args surrounding the usage of a GameItem.
/// </summary>
/// <typeparam name="T">Specific type of CSSItem involved.</typeparam>
public class UsageArgs<T> where T: GameItem
{
	public T itemUsed;
	public MonoBehaviour usedOn;
	//public CustomAttribute[] custAttributes 	{ get { return itemUsed.custAttributes; } }

	public UsageArgs(T itemUsed, MonoBehaviour usedOn)
	{
		this.itemUsed = 						itemUsed;
		this.usedOn = 							usedOn;
	}

}
