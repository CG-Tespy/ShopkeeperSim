using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FloatDisplayer : UIElementController
{
    [SerializeField] FloatReference toDisplay;
    [SerializeField] Text textField;

    private void Start()
    {
        toDisplay.ValueChanged.AddListener(OnValueChanged);
        Refresh();
    }

    protected override void Update()
    {
        base.Update();
        Refresh();
    }

    public override void Refresh()
    {
        textField.text =                    toDisplay.value.ToString();
        base.Refresh();
    }

    void OnValueChanged(float newValue)
    {
        Refresh();
    }

}
