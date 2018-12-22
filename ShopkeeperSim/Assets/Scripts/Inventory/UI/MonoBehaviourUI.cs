using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Base class for all UI MonoBehaviours.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public abstract class MonoBehaviourUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, 
    IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
	public RectTransform rectTransform 							{ get; protected set; }
	public CanvasRenderer canvasRenderer 						{ get; protected set; }
	public UIEvents events 										{ get; protected set; }
	const int leftClick = 										0;
	const int rightClick = 										1;

	// Use this for self-initialization
	protected virtual void Awake () 
	{
		rectTransform = 										GetComponent<RectTransform>();
		canvasRenderer = 										GetComponent<CanvasRenderer>();
		events = 												new UIEvents();
	}
	
	#region Mouse Event handlers
	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		events.PointerEntered.Invoke(eventData);
        //Debug.Log("Mouse entered " + this.name);
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		events.PointerExited.Invoke(eventData);
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
        Debug.Log("Pointer down on " + this.name);
		events.PointerDown.Invoke(eventData);
	}

	public virtual void OnPointerUp(PointerEventData eventData)
	{
		events.PointerUp.Invoke(eventData);
	}

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked " + this.name);
        events.PointerClicked.Invoke(eventData);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(this.name + ": OnBeginDrag");
        events.BeginDrag.Invoke(eventData);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Debug.Log(this.name + ": OnDrag");
        events.Drag.Invoke(eventData);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(this.name + ": OnEndDrag");
        events.EndDrag.Invoke(eventData);
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        events.Drop.Invoke(eventData);
    }
	#endregion
	
}
