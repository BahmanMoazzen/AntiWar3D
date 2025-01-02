/*
 * 
 * ShopManager Version 1.0
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// create a ShopManagerInfo ScriptableObject and then create ShopItemInfo for it

public class ShopManager : MonoBehaviour
{
    // fires when purchase is successful
    public delegate void purchaseSuccessful(ShoptItemInfo iPurchase);
    public static event purchaseSuccessful OnPurchaseSuccessfull;

    // if shop window closes this will fire
    public delegate void shopClosed();
    public static event shopClosed OnShopClosed;

    // instance to handle class
    public static ShopManager _INSTANCE;

    

    // this visibles shop panel should be fired within other codes to show shop panel
    public void _ShowShop()
    {
        foreach(Image go in GetComponentsInChildren<Image>())
        {
            go.enabled = true;
        }
        foreach(Text t in GetComponentsInChildren<Text>())
        {
            t.enabled = true;
        }
        //_ShopPanel.SetActive(true);
    }
    public void _HideShop()
    {
        foreach (Image go in GetComponentsInChildren<Image>())
        {
            go.enabled = false;
        }
        foreach (Text t in GetComponentsInChildren<Text>())
        {
            t.enabled = false;
        }

        //_ShopPanel.SetActive(false);
    }

    #region private

    // procedure to start purchase routine. it fires from ShopItem dont use on other codes.

    public void StartPurchase(string iSKUID)
    {
        foreach (var item in _ShopSetting._AllShopItems)
        {
            if (item._ItemSKUID.Equals(iSKUID))
            {
                item._ItemInfo._ChangeAmount(item._ItemChargeAmount, false);
                _ShopPanel.SetActive(false);
                OnShopClosed?.Invoke();
                OnPurchaseSuccessfull?.Invoke(item);
                return;
            }
        }
    }

    // the setting of shop manager including shop items
    [SerializeField] ShopManagerInfo _ShopSetting;

    // the template of shop item ui
    [SerializeField] GameObject _ShopItemTemplate;

    // the panel of shop to show or hide
    [SerializeField] GameObject _ShopPanel;

    // the parent of _ShopItemTemplate
    [SerializeField] Transform _ShopItemParent;
    private void Awake()
    {
        if (_INSTANCE == null)
            _INSTANCE = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        StartCoroutine(_startupRoutine());

    }
    IEnumerator _startupRoutine()
    {
        
        
        foreach (var item in _ShopSetting._AllShopItems)
        {
            GameObject go = Instantiate(_ShopItemTemplate, _ShopItemParent);
            go.GetComponent<ShopItem>().ItemInfo = item;
        }
        _HideShop();
        yield return 0;
        
    }
    void OnEnable()
    {
        ShopItem.OnMouseClicked += StartPurchase;
    }
    void OnDisable()
    {
        ShopItem.OnMouseClicked -= StartPurchase;
    }

    #endregion

}
