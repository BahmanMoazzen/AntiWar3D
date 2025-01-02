using UnityEngine;
[CreateAssetMenu(fileName ="NewShopItem",menuName ="BAHMAN/Shop Item",order =1)]
public class ShoptItemInfo : ScriptableObject
{
    public string _ItemName, _ItemSKUID, _ItemPrice;
    public int _ItemChargeAmount;
    public SaveableItem _ItemInfo;
    public bool isToman= true;

    
}
