using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] Transform _Target;
    [SerializeField] float _Range;
    [SerializeField] LayerMask _EnemyMask;
    [SerializeField] Transform _WeaponTip;
    [SerializeField] GameObject _Ammo;
    [SerializeField] GunAmmoInfo _AmmoInfo;
    private void Update()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            Instantiate(_Ammo, _WeaponTip.position, Quaternion.identity).transform.forward = _WeaponTip.forward;

        }

        Collider[] enemies = Physics.OverlapSphere(transform.position, _Range, _EnemyMask);
        if (enemies.Length > 0)
        {

            transform.LookAt(enemies[0].transform.position);
        }
        else
        {

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _Range);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_WeaponTip.position, .1f);
    }
}
