using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour
{
    [SerializeField]
    private Text moneytext;

    [SerializeField]
    private GameObject iap;

    [SerializeField]
    private UiManager uiManager;

    [SerializeField]
    private GameObject noAds;

    [SerializeField]
    private int isads;

    public void Awake()
    {
        if (PlayerPrefs.HasKey("NoAds"))
        {
            isads = PlayerPrefs.GetInt("NoAds");
        }
        else
        {
            isads = 0;
            PlayerPrefs.SetInt("NoAds", 0);
        }

        Init();
    }

    public void Init()
    {
        isads = PlayerPrefs.GetInt("NoAds");
        if (isads != 0)
        {
            noAds.SetActive(false);
        }
    }
    public void GetIAPGift(int _money,float _iap, bool _noads)
    {
        if(_iap == 0)
        {
            ItemManager.ins.AddMoney(_money);
        }
        else
        {
            ItemManager.ins.AddMoney(_money);
        }

        if(_noads)
        {

        }
    }

    public void Update()
    {
        moneytext.text = "" + ItemManager.ins.itemData.money;
    }

    public void OnIAP()
    {
        iap.SetActive(true);
        uiManager.PauseGame();
    }

    public void EndIAP()
    {
        iap.SetActive(false);
        uiManager.InGame();
    }
}
