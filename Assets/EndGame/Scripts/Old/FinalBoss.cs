using UnityEngine;
using Unity.FPS.Game;

public class FinalBossController : MonoBehaviour
{
    public enum BossPhase
    {
        Phase1,
        Phase2,
        Phase3
    }

    public BossPhase CurrentPhase = BossPhase.Phase1;

    public GameObject LaserPulsePrefab;
    public Transform PulseSpawnPoint; // Optional, or use boss center

    public float phase2HealthThreshold = 0.66f;
    public float phase3HealthThreshold = 0.33f;

    private Health health;
    private float maxHealth;
    private float lastAttackTime;
    public float attackCooldown = 5f;

    void Start()
    {
        health = GetComponent<Health>();
        if (health)
        {
            maxHealth = health.CurrentHealth;
            health.OnDamaged += HandleDamage;
            health.OnDie += HandleDeath;
        }
    }

    void Update()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            TriggerAttack();
        }
    }

    void HandleDamage(float damage, GameObject damageSource)
    {
        float healthPercent = health.CurrentHealth / maxHealth;

        if (healthPercent <= phase3HealthThreshold && CurrentPhase != BossPhase.Phase3)
        {
            CurrentPhase = BossPhase.Phase3;
            Debug.Log("Boss transitioned to Phase 3!");
            // Play VFX, change appearance, increase aggression
        }
        else if (healthPercent <= phase2HealthThreshold && CurrentPhase != BossPhase.Phase2)
        {
            CurrentPhase = BossPhase.Phase2;
            Debug.Log("Boss transitioned to Phase 2!");
        }
    }

    void TriggerAttack()
    {
        switch (CurrentPhase)
        {
            case BossPhase.Phase1:
                FireLaserPulse();
                break;
            case BossPhase.Phase2:
                FireLaserPulse();
                SummonTurrets(); // can be a new function
                break;
            case BossPhase.Phase3:
                FireLaserPulse();
                FireProjectiles();
                SummonMinions(); // most chaotic phase
                break;
        }
    }

    void FireLaserPulse()
    {
        GameObject pulse = Instantiate(LaserPulsePrefab, PulseSpawnPoint.position, Quaternion.identity);

        LaserPulseBehavior behavior = pulse.GetComponent<LaserPulseBehavior>();
        behavior.DamageOwner = gameObject;
    }

    void FireProjectiles()
    {
        Debug.Log("Boss fires a barrage!");
        // Later: Instantiate multiple projectiles toward player
    }

    void SummonTurrets()
    {
        Debug.Log("Boss summons auto-turrets!");
        // Later: Instantiate turret prefabs around the arena
    }

    void SummonMinions()
    {
        Debug.Log("Boss summons minions!");
        // Later: Instantiate enemy units
    }

    void HandleDeath()
    {
        Debug.Log("Boss defeated!");
        // Play death animation, disable attacks, end game, etc.
    }
}
