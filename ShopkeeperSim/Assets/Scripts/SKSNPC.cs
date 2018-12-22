using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class SKSNPC : MonoBehaviour2D, ICustomer
{
    #region Fields
    [Tooltip("How much cash the NPC has to spend.")]
    [SerializeField] float _cash =                   100;
    [Tooltip("The most cash this NPC can hold at any one time.")]
    [SerializeField] float _maxCash = 999;
    [Tooltip("How much cash this NPC gets per second.")]
    [SerializeField] float _cashGenRate =            10;
    [SerializeField] float _moveSpeed =              3;
    [Tooltip("Minimum amount of time (in seconds) it takes for the NPC to try to change direction.")]
    [SerializeField] float minTimeDirChange =       1;
    [Tooltip("Maximum amount of time (in seconds) it takes for the NPC to try to change direction.")]
    [SerializeField] float maxTimeDirChange =       5;
    [SerializeField] SKSShop[] shopsToBuyFrom;

    float timeBuyInterval =                         3f;
    float buyTimer =                                0;
    float movementChangeTimer =                     0;
    float dirChangeTime =                           0;

    #endregion

    #region Properties
    public float cash
    {
        get { return _cash; }
        set{  _cash = Mathf.Clamp(value, 0, maxCash); }
    }

    public float maxCash
    {
        get { return _maxCash; }
        protected set { _maxCash = value; }
    }
    
    public float cashGenRate
    {
        get { return _cashGenRate; }
        protected set { _cashGenRate = value; }
    }

    public float moveSpeed
    {
        get { return _moveSpeed; }
        protected set { _moveSpeed = value; }
    }

    Vector3 velocity
    {
        get { return rigidbody.velocity; }
        set { rigidbody.velocity = value; }
    }

    #endregion

    // Use this for self-initialization
    protected override void Awake () 
	{
        base.Awake();
        RandomizeMovementCountdown();
	}

    private void Update()
    {
        GenerateCash();
        ShopAround();
    }

    private void FixedUpdate()
    {
        RoamAround();
    }

    // Make sure the NPC doesn't get stuck on an invisible wall.
    protected override void OnCollisionStay2D(Collision2D other)
    {
        base.OnCollisionStay2D(other);
        ResetMovement();
    }

    void RoamAround()
    {
        movementChangeTimer +=                                    Time.deltaTime;

        if (movementChangeTimer >= dirChangeTime)
            ResetMovement();
    }

    void ShopAround()
    {
        buyTimer +=                                     Time.deltaTime;

        if (buyTimer > timeBuyInterval)
        {
            TryToBuySomething();
            buyTimer =                                  0;
        }
    }

    bool TryToBuySomething()
    {
        bool boughtSomething =                  false;

        if (shopsToBuyFrom != null && shopsToBuyFrom.Length > 0)
        {
            // Pick a random shop.
            int shopCount =                     shopsToBuyFrom.Length;
            int shopIndex =                     Random.Range(0, shopCount);
            SKSShop toBuyFrom =                 shopsToBuyFrom[shopIndex];

            // Buy the first item this NPC can afford.
            SKSItem itemToBuy =                 null;
            foreach (SKSItem item in toBuyFrom.storefront.items)
            {
                if (item.price <= cash)
                {
                    itemToBuy =                 item;
                    break;
                }
            }

            if (itemToBuy != null)
            {
                toBuyFrom.SellItem(itemToBuy, this);
                boughtSomething =               true;
            }
            
        }

        return boughtSomething;
    }

    void GenerateCash()
    {
        cash +=                         cashGenRate * Time.deltaTime;   
    }
	
    void ChangeDirection()
    {
        Vector2 newDirection =                      Vector2.zero;
        newDirection.x =                            Random.Range(-moveSpeed, moveSpeed);
        newDirection.y =                            Random.Range(-moveSpeed, moveSpeed);
        velocity =                                  newDirection;
    }

    void RandomizeMovementCountdown()
    {
        movementChangeTimer =                     0;
        dirChangeTime =             Random.Range(minTimeDirChange, maxTimeDirChange);
    }

    void ResetMovement()
    {
        ChangeDirection();
        RandomizeMovementCountdown();
    }
}
