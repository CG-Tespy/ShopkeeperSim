using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class UIElementController : MonoBehaviourUI
{
    public UnityEvent Refreshed                             { get; protected set; }
	public virtual bool mouseOverThis 						{ get; protected set; }
	public virtual bool isFocused 							{ get; protected set; }

    protected CanvasGroup canvasGroup;

    public float opacity
    {
        get { return canvasGroup.alpha;  }
        set { canvasGroup.alpha = value; }
    }

    public bool interactable
    {
        get { return canvasGroup.interactable;  }
        set { canvasGroup.interactable = value; }
    }

    public bool blocksRaycasts
    {
        get { return canvasGroup.blocksRaycasts; }
        set { canvasGroup.blocksRaycasts = value; }
    }

	protected override void Awake()
	{
		base.Awake();
        Refreshed =                                         new UnityEvent();
		mouseOverThis = 									false;
		isFocused = 										false;
        canvasGroup =                                       GetComponent<CanvasGroup>();
	}

    public virtual void Refresh()
    {
        Refreshed.Invoke();
    }

    protected virtual void Update()
    {
        UpdateFocus();
    }

    protected virtual void UpdateFocus()
	{
		bool leftClicked = 									Input.GetMouseButtonDown(0);

		if (leftClicked && !mouseOverThis)
			isFocused = 									false;
	}

	public override void OnPointerClick(PointerEventData eventData)
	{
        base.OnPointerClick(eventData);
		if (this.gameObject.activeSelf)
			isFocused = 									true;
	}

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        mouseOverThis =                                     true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        mouseOverThis =                                     false;
    }

    public virtual void Hide(bool beInteractable = false, bool blockRaycasts = false)
    {
        opacity =                                           0;
        interactable =                                      beInteractable;
        blocksRaycasts =                                    blockRaycasts;

    }

    public virtual void Show(bool beInteractable = true, bool blockRaycasts = true)
    {
        // Make sure the object is active
        gameObject.SetActive(true);
        opacity =                                           1;
        interactable =                                      beInteractable;
        blocksRaycasts =                                    blockRaycasts;
    }

    /// <summary>
    /// Hides the element if it is being shown, shows it if it is being hidden.
    /// </summary>
    public virtual void ReverseView()
    {
        if (opacity == 0)
            Show();
        else
            Hide();
    }

}
