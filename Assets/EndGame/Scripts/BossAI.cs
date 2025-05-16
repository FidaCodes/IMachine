using UnityEngine;
using Unity.FPS.Game;
using System.Collections;

public class BossAI : MonoBehaviour
{
    [Header("General Settings")]
    public GameObject player;
    public float attackInterval = 3f;

    [Header("Shockwave Settings")]
    public GameObject shockwavePrefab;
    public Transform shockwaveSpawnPoint;

    [Header("Railgun Settings")]
    public GameObject railgunLaserPrefab;
    public Transform[] railgunSpawnPoints;

    private bool shieldActive = true;
    private bool isVulnerable = false;
    private Health health;
    public GameEndingManager endingManager;

    void Start()
    {
        Debug.Log("BossAI STARTED");

        health = GetComponent<Health>();
        if (health != null)
        {
            health.OnDamaged += OnDamaged;
            health.OnDie += Die;
        }

        StartCoroutine(AttackCycle());
    }

    IEnumerator AttackCycle()
    {
        while (true)
        {
            if (shieldActive)
            {
                PerformShockwaveAttack();
            }
            else if (isVulnerable)
            {
                StartCoroutine(PerformRailgunAttack());
            }

            yield return new WaitForSeconds(attackInterval);
        }
    }

    void PerformShockwaveAttack()
    {
        Debug.Log("Performing Shockwave Attack");
        GameObject shockwave = Instantiate(shockwavePrefab, shockwaveSpawnPoint.position, Quaternion.identity);

        ShockwaveController controller = shockwave.GetComponent<ShockwaveController>();
        if (controller != null)
        {
            controller.ActivateShockwave();
        }
        else
        {
            Debug.LogWarning("ShockwaveController script not found on shockwave prefab!");
        }
    }

    IEnumerator PerformRailgunAttack()
    {
        if (player == null) yield break;

        for (int i = 0; i < railgunSpawnPoints.Length; i++)
        {
            Transform spawn = railgunSpawnPoints[i];
            CharacterController col = player.GetComponent<CharacterController>();
            Vector3 targetCenter = player.transform.position + Vector3.up * col.height * 0.5f;

            Vector3 direction = (targetCenter - spawn.position).normalized;
            float maxDistance = Vector3.Distance(spawn.position, targetCenter);
            Vector3 finalTarget = targetCenter;

            if (Physics.Raycast(spawn.position, direction, out RaycastHit hit, maxDistance))
            {
                finalTarget = hit.point;
            }

            GameObject laser = Instantiate(railgunLaserPrefab, spawn.position, Quaternion.identity);
            RailgunLaser laserScript = laser.GetComponent<RailgunLaser>();
            if (laserScript != null)
            {
                laserScript.Initialize(spawn.position, finalTarget);
            }

            yield return new WaitForSeconds(3f); // delay between shots
        }
    }

    int currentPhase = 1;

    void EnterPhase2()
    {
        currentPhase = 2;
        attackInterval = 2f; // faster attacks
        Debug.Log("Boss Phase 2");
    }

    void EnterPhase3()
    {
        currentPhase = 3;
        attackInterval = 1f;
        Debug.Log("Boss Final Phase!");
    }




    public void DisableShield()
    {
        shieldActive = false;
        isVulnerable = true;
        health.Invincible = false; // Make sure the boss can now take damage
    }

    void OnDamaged(float amount, GameObject source)
    {
        if (!isVulnerable)
            return;

        float healthPercent = health.CurrentHealth / health.MaxHealth;

        if (healthPercent <= 0.33f && currentPhase < 3)
        {
            EnterPhase3();
        }
        else if (healthPercent <= 0.66f && currentPhase < 2)
        {
            EnterPhase2();
        }
    }



    void Die()
    {
        Debug.Log("Boss died");

        if (endingManager != null)
            endingManager.TriggerGameEnd();

        // Optional: destroy boss object
        Destroy(gameObject);
    }
}
