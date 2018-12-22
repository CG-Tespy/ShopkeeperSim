using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Shopkeeping/ShopKeeper", fileName = "NewShopkeeper")]
public class SKSShopkeeper : ScriptableObject
{
    [SerializeField] FloatReference _cash;
    [SerializeField] SKSShop                        shopOwned;
    
    public float cash
    {
        get                                         { return _cash.value; }
        set                                         { _cash.value = value; }
    }

    public void GetPaid(float amount)
    {
        cash +=                                     amount;
    }

    public virtual void OnEnable()
    {
        Debug.Log(this.name + " On Enable!");
        if (shopOwned.owner != this)
            shopOwned.owner =                       this;
    }
	
}
