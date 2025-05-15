using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WeaponBuff
{
    public float Duration;
    public float DamageMultiplier;
    public float FireRateMultiplier; // <1 = faster, >1 = slower
    public bool InfiniteAmmo;

    public WeaponBuff(float duration, float damageMultiplier, float fireRateMultiplier, bool infiniteAmmo)
    {
        Duration = duration;
        DamageMultiplier = damageMultiplier;
        FireRateMultiplier = fireRateMultiplier;
        InfiniteAmmo = infiniteAmmo;
    }
}
