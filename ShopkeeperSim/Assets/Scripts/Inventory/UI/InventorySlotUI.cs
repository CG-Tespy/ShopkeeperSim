using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : DraggableUIElement
{
    static UnityEvent _SlotDragBegin =          new UnityEvent();
    public static UnityEvent SlotDragBegin
    {
        get                                     { return _SlotDragBegin; }
        protected set                           { _SlotDragBegin = value; }
    }

    static UnityEvent _SlotDragEnd =            new UnityEvent();
    public static UnityEvent SlotDragEnd
    {
        get                                     { return _SlotDragEnd; }
        protected set                           { _SlotDragEnd = value; }
    }

    SKSItem _itemToDisplay =                    null;
    SKSInventory inventory;
    GraphicRaycaster gRaycaster;

	public virtual SKSItem itemToDisplay
	{
		get { return _itemToDisplay; }
		set 
		{
			_itemToDisplay = 						value;
			Refresh(); // Always display the current item properly.
		}
	}

	[SerializeField] Image image;
    [SerializeField] Text priceText;

    RectTransform anchorParent;
    int siblingIndex;

	// Use this for self-initialization
	protected override void Awake () 
	{
        base.Awake();
        gRaycaster =                                GameObject.FindObjectOfType<GraphicRaycaster>();
		EnsureComponents();
        SlotDragBegin.AddListener(OnSlotDragBegin);
        SlotDragEnd.AddListener(OnSlotDragEnd);
	}

    protected override void Start()
    {
        base.Start();
        inventory =                                 _itemToDisplay.belongsTo as SKSInventory;

        // This may not be parented to something during Awake. Definitely should during Start.
        anchorParent =                              rectTransform.parent as RectTransform;
        Canvas.ForceUpdateCanvases();
        siblingIndex =                              rectTransform.GetSiblingIndex();
        // ^ Gets the wrong index for some reason
    }
	
	public override void Refresh()
	{
		image.sprite = 								itemToDisplay.sprite;
        priceText.text =                            itemToDisplay.price.ToString();
        base.Refresh();
	}

    public void ResetParent()
    {
        transform.SetParent(anchorParent, false);
        transform.SetSiblingIndex(siblingIndex);
    }

    protected override void OnDraggableBeginDrag(PointerEventData eventData)
    {
        if (!dragWith.Contains(eventData.button))
            return;

        // Make sure to allow free movement all over the screen when dragging this.
        Canvas parentCanvas =                       rectTransform.GetParentCanvas();
        rectTransform.SetParent(parentCanvas.transform);
        SlotDragBegin.Invoke();
        base.OnDraggableBeginDrag(eventData);
        
    }

    protected override void OnDraggableEndDrag(PointerEventData eventData)
    {
        if (!dragWith.Contains(eventData.button))
            return;

        // See if this is was left over a rycast-blocking drop space.

        List<RaycastResult> hits =                  new List<RaycastResult>();
        gRaycaster.Raycast(eventData, hits);

        InventoryDropSpace dropSpace =              null;
        bool hitDropSpace =                         false;

        foreach (RaycastResult hit in hits)
        {
            dropSpace =                             hit.gameObject.GetComponent<InventoryDropSpace>();
            if (dropSpace != null)
                break;
        }

        // If not, put it back where it was. Otherwise, let the drop area handle the rest.
        if (!hitDropSpace) 
            ResetParent();

        SlotDragEnd.Invoke();
        base.OnDraggableDrag(eventData);
    }

    // Helpers
    void EnsureComponents()
	{
		// Make sure this has all the components it needs
		string errMessage = 						this.name +  "is missing references to these components: ";
		List<string> missingComponents = 			new List<string>();

		if (image == null)
			missingComponents.Add("Image for the item's icon.");

		if (missingComponents.Count > 0)
		{
			foreach (string missingComponent in missingComponents)
				errMessage = 						string.Concat(errMessage, '\n', missingComponent);

			throw new System.NullReferenceException(errMessage);
		}
			
	}
	
    void OnSlotDragBegin()
    {
        blocksRaycasts =                                false;
        draggableArea.blocksRaycasts =                  false;
    }

    void OnSlotDragEnd()
    {
        blocksRaycasts =                                true;
        draggableArea.blocksRaycasts =                  true;
    }
}
