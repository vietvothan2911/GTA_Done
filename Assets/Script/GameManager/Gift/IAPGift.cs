using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPGift : MonoBehaviour
{
    [SerializeField]
    private int money;

    [SerializeField]
    private IAPManager iAPManager;

    [SerializeField]
    private bool isNoAds;

    [SerializeField]
    private float ipaMoney;

    public void ClickIAP()
    {
        iAPManager.GetIAPGift(money, ipaMoney,isNoAds);
    }
    
}
