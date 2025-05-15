using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;

public class FullPowerBuffPickup : MonoBehaviour
{
    public float Duration = 8f;
    public float DamageMultiplier = 2f;
    public float FireRateMultiplier = 0.5f; // 0.5 = 2x faster
    public bool InfiniteAmmo = true;

    private void OnTriggerEnter(Collider other)
    {
        PlayerWeaponsManager manager = other.GetComponent<PlayerWeaponsManager>();
        if (manager != null)
        {
            WeaponBuff buff = new WeaponBuff(
                Duration,
                DamageMultiplier,
                FireRateMultiplier,
                InfiniteAmmo
            );
            manager.ApplyWeaponBuff(buff);
            Destroy(gameObject);
        }
    }
}
