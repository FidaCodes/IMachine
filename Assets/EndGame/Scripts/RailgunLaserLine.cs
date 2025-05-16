using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RailgunLaserLine : MonoBehaviour
{
    public float laserDuration = 1.5f;
    public float laserDamage = 30f;
    public LayerMask playerLayer;

    private LineRenderer lineRenderer;
    private float timer = 0f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Initialize(Vector3 start, Vector3 target)
    {
        Vector3 direction = (target - start).normalized;
        float distance = Vector3.Distance(start, target);

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, target);

        // 🔥 Shoot raycast at player
        if (Physics.Raycast(start, direction, out RaycastHit hit, distance, playerLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                var health = hit.collider.GetComponent<Unity.FPS.Game.Health>();
                if (health != null)
                {
                    Debug.Log("Railgun laser hit the player via Raycast!");
                    health.TakeDamage(laserDamage, gameObject);
                }
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= laserDuration)
        {
            Destroy(gameObject);
        }
    }
}
