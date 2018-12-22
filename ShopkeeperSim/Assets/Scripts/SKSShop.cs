using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Shopkeeping/Shop", fileName = "NewShop")]
public class SKSShop : ScriptableObject
{
    UnityEvent _SoldSomething =                 new UnityEvent();

    public UnityEvent SoldSomething
    {
        get                                     { return _SoldSomething; }
        protected set                           { _SoldSomething = value; }
    }
    [SerializeField] SKSInventory _storefront, _storage;
    [SerializeField] SKSShopkeeper _owner;

    public SKSShopkeeper owner
    {
        get { return _owner; }
        set { _owner = value; }
    }

    public SKSInventory storefront
    {
        get { return _storefront; }
    }

    public SKSInventory storage
    {
        get { return _storage; }
    }

    /// <summary>
    /// Return true if selling was successful, false otherwise.
    /// </summary>
    public bool SellItem(SKSItem itemToSell, ICustomer toSellTo)
    {
        if (toSellTo.cash >= itemToSell.price)
        {
            toSellTo.cash -=                        itemToSell.price;
            storefront.Remove(itemToSell);
            owner.cash +=                           itemToSell.price;
            SoldSomething.Invoke();
            return true;
        }

        Debug.Log(toSellTo.name + " doesn't have enough cash to buy " + itemToSell.name);
        return false;
    }
	
}
