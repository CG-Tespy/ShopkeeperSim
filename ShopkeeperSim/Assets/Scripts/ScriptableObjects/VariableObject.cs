using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class VariableObject<T> : ScriptableObject
{
    public class VariableValueEvent : UnityEvent<T> { }

    VariableValueEvent _ValueChanged =                  new VariableValueEvent();
    public VariableValueEvent ValueChanged
    {
        get { return _ValueChanged; }
    }

    [SerializeField] T _value;

    public virtual T value
    {
        get { return _value; }
        set
        {
            _value = value;
        }
    }

    public override string ToString()
    {
        return value.ToString();
    }

}
