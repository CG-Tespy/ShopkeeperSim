using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class IntDisplayer : UIElementController
{
    [SerializeField] IntReference toDisplay;
    [SerializeField] Text textField;

    private void Start()
    {
        toDisplay.ValueChanged.AddListener(OnValueChanged);
        Refresh();
    }

    public override void Refresh()
    {
        textField.text =                                toDisplay.value.ToString();
        base.Refresh();
    }

    void OnValueChanged(int newValue)
    {
        Refresh();
    }

}
