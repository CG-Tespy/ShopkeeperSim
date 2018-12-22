using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableUIElement : UIElementController
{
    Vector2 lastMousePosition =                         Vector2.zero;

    [Tooltip("This UI element can be dragged by the click of any of these mouse buttons.")]
    [SerializeField] protected List<PointerEventData.InputButton> dragWith;
    [Tooltip("What to click to drag this.")]
    [SerializeField] protected UIElementController draggableArea;

    int uiBlockLayer;

    protected override void Awake()
    {
        base.Awake();
        uiBlockLayer =                                  LayerMask.NameToLayer("UIBlock");
    }


    protected virtual void Start()
    {
        // Make sure to act based on what happens to the draggable area.
        draggableArea.events.BeginDrag.AddListener(OnDraggableBeginDrag);
        draggableArea.events.Drag.AddListener(OnDraggableDrag);
        draggableArea.events.EndDrag.AddListener(OnDraggableEndDrag);

        // And let the user know when this is effectively undraggable.
        if (dragWith.Count == 0)
        {
            string warnMessage =        this.name + " doesn't have any mouse buttons set up to drag it.";
            Debug.LogWarning(warnMessage);
        }
    }

    protected virtual void OnDraggableBeginDrag(PointerEventData eventData)
    {
        // Only drag with the mouse buttons set for this instance.
        if (!dragWith.Contains(eventData.button))
            return;

        // Set the initial mouse position that the movement will be made relative towards.
        lastMousePosition =                         Input.mousePosition;
        Vector2 draggablePos =                      draggableArea.transform.position;

        if (eventData.pointerDrag != this.gameObject)
            events.BeginDrag.Invoke(eventData);
        // Best invoke the event here when this is being indirectly dragged, not only because it 
        // makes sense, but because we want to avoid a stack overflow.

        draggableArea.blocksRaycasts =              false; // So that this can be dropped on things rendering below it.
    }

    protected virtual void OnDraggableDrag(PointerEventData eventData)
    {
        // Only drag with the mouse buttons set for this instance, and when the mouse isn't 
        // over something to block dragging.
        if (!dragWith.Contains(eventData.button))
            return;

        // Apply the drag movement based on how far the current mouse position is 
        // from the last one. And remember, in vectors... A - B = C, with C pointing 
        // from B to A.
        Vector2 mousePos =                      Input.mousePosition;

        Vector3 movement =                      mousePos - lastMousePosition;
        transform.localPosition +=              movement;

        lastMousePosition =                     mousePos;

        if (eventData.pointerDrag != this.gameObject)
            events.Drag.Invoke(eventData);
        
    }

    protected virtual void OnDraggableEndDrag(PointerEventData eventData)
    {
        draggableArea.blocksRaycasts =          true;
    }
    
    bool OverDragBlocker(PointerEventData eventData)
    {
        return true;
    }

}
