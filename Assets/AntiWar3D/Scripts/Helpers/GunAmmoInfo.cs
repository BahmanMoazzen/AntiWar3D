using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAmmoInfo : ScriptableObject
{
    [SerializeField] SaveableItem _AmmoCount;
    const string _AmmoPrefixTag = "Ammo_";
    [SerializeField] string _AmmoName;
    public int _Count
    {
        set
        {
            _AmmoCount._Stock = value;
        }
        get
        {
            return _AmmoCount._Stock;
        }
    }
}
