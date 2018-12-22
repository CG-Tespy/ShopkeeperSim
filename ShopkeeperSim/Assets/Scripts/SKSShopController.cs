using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SKSShopController : MonoBehaviour 
{
    [SerializeField] SKSShop shopObj;
    [SerializeField] UnityEvent _SoldSomething;

    // Use this for self-initialization
    void Awake()
    {
        shopObj.SoldSomething.AddListener(() => _SoldSomething.Invoke());
    }

}
