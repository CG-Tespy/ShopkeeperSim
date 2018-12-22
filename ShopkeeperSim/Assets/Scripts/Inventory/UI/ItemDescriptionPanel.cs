using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDescriptionPanel : UIElementController
{
    // Fields
    [SerializeField] InventoryDisplayWindow displayWindow;
    [SerializeField] Text nameText;
    [SerializeField] Text descText;
    [SerializeField] Image itemImage;

    SKSItem itemToDisplay;

    UnityAction<PointerEventData> slotClickAction;

	// Use this for self-initialization
	protected override void Awake () 
	{
        base.Awake();
        EnsureNecessaryComponents();
        slotClickAction =                           OnSlotClicked;

        // There's no item to display at this point, so...
        ClearTextFields();
        HideItemImage();
	}
	
	// Use this when you need to access things that are to be initialized in Awake
	void Start ()
	{
        displayWindow.Refreshed.AddListener(ObserveSlots);
	}

    public override void Refresh()
    {
        if (itemToDisplay == null)
        {
            HideOwnContents();
            base.Refresh();
            return;
        }

        this.itemImage.sprite =                 itemToDisplay.sprite;
        RefreshTextFields();
        ShowItemImage();

        base.Refresh();
    }

    void OnSlotClicked(PointerEventData eventData)
    {
        InventorySlotUI slotUI =            eventData.pointerPress.GetComponent<InventorySlotUI>();

        this.itemToDisplay =                slotUI.itemToDisplay;
        Refresh();
    }

    // Helpers

    void EnsureNecessaryComponents()
    {
        List<string> missingComponents =                            new List<string>();

        if (displayWindow == null)
            missingComponents.Add("Inventory display window,\n");

        if (nameText == null)
            missingComponents.Add("Text field for the item's name.\n");

        if (descText == null)
            missingComponents.Add("Text field for the item's description.\n");

        if (missingComponents.Count > 0)
        {
            string errMessage =                                     this.name + " needs references to these components:\n";
            foreach (string component in missingComponents)
                errMessage =                                        string.Concat(errMessage, component);

            throw new System.MissingFieldException(errMessage);
                    
        }
    }
	
    void HideOwnContents()
    {
        ClearTextFields();
        HideItemImage();
    }

    void ClearTextFields()
    {
        nameText.text =                                         "";
        descText.text =                                         "";
    }

    void RefreshTextFields()
    {
        nameText.text =                                         itemToDisplay.name;
        descText.text =                                         itemToDisplay.description;
    }

    void HideItemImage()
    {
        Color transparent =                             itemImage.color;
        transparent.a =                                 0;
        itemImage.color =                               transparent;
    }

    void ShowItemImage()
    {
        Color opaque =                                  itemImage.color;
        opaque.a =                                      1;
        itemImage.color =                               opaque;
    }
    
    void ObserveSlots()
    {
        InventorySlotUI slotUI =                            null;

        // Watch for when any of the inventory slots are clicked, so this displays the item last clicked.
        foreach (Transform slot in displayWindow.slotHolder)
        {
            slotUI =                                        slot.GetComponent<InventorySlotUI>();
            slotUI.events.PointerClicked.RemoveListener(slotClickAction);
            // ^ In case this panel was already watching this slot.

            slotUI.events.PointerClicked.AddListener(slotClickAction);
        }

        itemToDisplay =                                     null;
        HideOwnContents();
    }

}
