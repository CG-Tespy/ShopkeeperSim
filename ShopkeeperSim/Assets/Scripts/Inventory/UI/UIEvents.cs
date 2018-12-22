using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerEvent : UnityEvent<PointerEventData> { }

public class UIEvents
{
	public PointerEvent PointerEntered 						{ get; protected set; }
	public PointerEvent PointerExited 						{ get; protected set; }
	public PointerEvent PointerClicked 						{ get; protected set; }
    public PointerEvent PointerDown                         { get; protected set; }
	public PointerEvent PointerOver 						{ get; protected set; }
	public PointerEvent PointerUp 					        { get; protected set; }
	public PointerEvent PointerDragged 						{ get; protected set; }
    public PointerEvent BeginDrag                           { get; protected set; }
    public PointerEvent Drag                                { get; protected set; }
    public PointerEvent EndDrag                             { get; protected set; }
    public PointerEvent Drop                                { get; protected set; }
    
	public UIEvents()
	{
		PointerEntered = 										new PointerEvent ();
		PointerExited = 										new PointerEvent ();
		PointerClicked = 										new PointerEvent();
        PointerDown =                                           new PointerEvent();
		PointerOver = 										    new PointerEvent();
		PointerUp = 									        new PointerEvent();
		PointerDragged = 										new PointerEvent();
        BeginDrag =                                             new PointerEvent();
        Drag =                                                  new PointerEvent();
        EndDrag =                                               new PointerEvent();
        Drop =                                                  new PointerEvent();
	}
	
}
