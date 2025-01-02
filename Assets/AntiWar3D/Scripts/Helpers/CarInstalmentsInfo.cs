using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewCarInstallment",menuName ="BAHMAN/New Car Instalment",order =3)]
public class CarInstalmentsInfo : ScriptableObject
{
    public GameObject[] _Weapons;
    public Transform[] _WeaponPlaces;
    public int[] _WeaponInstallment;
    
}
