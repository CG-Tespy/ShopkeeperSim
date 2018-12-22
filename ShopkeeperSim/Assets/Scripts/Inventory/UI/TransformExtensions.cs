using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public static class TransformExtensions 
{
	/// <summary>
	/// Looks up the transform's family tree to find the nearest parent that has 
	/// a Canvas component.
	/// </summary>
	/// <param name="trans"></param>
	/// <returns></returns>
	public static Canvas GetParentCanvas(this Transform trans)
	{
		if (trans.parent == null)
			return null;

		Canvas parentCanvas = trans.parent.GetComponent<Canvas>();

		// if the parent has no canvas, ask it so look further up the family tree
		if (parentCanvas == null)
			return trans.parent.GetParentCanvas();
		
		// for when this one's parent has a Canvas component
		return parentCanvas;

	}
	
}
