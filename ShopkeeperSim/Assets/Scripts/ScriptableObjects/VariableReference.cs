using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


/// <summary>
/// Based off the FloatReference script as shown in 
//  Unite Austin 2017 : Game Architecture with Scriptable Objects.
//  T is the type of the var to reference, T2 is the variable object for that type.
/// </summary>
public abstract class VariableReference<T, T2> where T2: VariableObject<T>
{
    VariableObject<T>.VariableValueEvent _ValueChanged =    new VariableObject<T>.VariableValueEvent();

    public VariableObject<T>.VariableValueEvent ValueChanged
    {
        get { return _ValueChanged; }
    }

	[SerializeField] bool useRaw = 			true;
	[SerializeField] T rawValue;
	[SerializeField] T2 variable;

	public virtual T value
    {
        get { return useRaw ? rawValue : variable.value; }
        set
        {
            if (useRaw)
                rawValue =                  value;
            else
                variable.value =            value;

            ValueChanged.Invoke(value);
        }
	}

    public override string ToString()
    {
        if (useRaw)
            return rawValue.ToString();
        else
            return variable.ToString();
    }

}
