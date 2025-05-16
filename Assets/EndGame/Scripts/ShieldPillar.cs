using UnityEngine;
using Unity.FPS.Game;

[RequireComponent(typeof(Health))]
public class ShieldPillar : MonoBehaviour
{
    public Renderer[] glowIndicators;
    public Material activeMaterial;
    public Material destroyedMaterial;
    public BossShield bossShield;

    private bool isDestroyed = false;
    private Health health;

    void Start()
    {
        health = GetComponent<Health>();

        // Initialize visuals
        foreach (var rend in glowIndicators)
        {
            rend.material = activeMaterial;
        }

        // Subscribe to damage events
        health.OnDie += OnDestroyed;
    }

    void OnDestroyed()
    {
        if (isDestroyed) return;

        isDestroyed = true;

        foreach (var rend in glowIndicators)
        {
            rend.material = destroyedMaterial;
        }

        bossShield?.NotifyPillarDestroyed();

        // Optionally disable collider or other components
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;
    }
}
