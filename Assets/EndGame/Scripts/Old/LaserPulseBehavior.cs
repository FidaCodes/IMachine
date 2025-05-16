using UnityEngine;
using Unity.FPS.Game;

public class LaserPulseBehavior : MonoBehaviour
{
    [Header("Expansion Settings")]
    public float MaxRadius = 15f;
    public float ExpansionTime = 2f;
    public float Damage = 30f;

    [Header("Damage Settings")]
    public LayerMask DamageLayers;
    public GameObject DamageOwner;
    public DamageArea DamageArea;

    private float _elapsedTime = 0f;
    private bool _hasInflictedDamage = false;
    private bool _isFiring = false;
    private Transform _visual;

    void Awake()
    {
        _visual = transform.Find("Visual");
        if (_visual != null)
        {
            _visual.localScale = Vector3.zero;
        }

        if (!DamageArea)
        {
            DamageArea = GetComponent<DamageArea>();
        }
    }

    void Update()
    {
        if (!_isFiring)
            return;

        _elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(_elapsedTime / ExpansionTime);
        float currentRadius = Mathf.Lerp(0f, MaxRadius, t);

        if (_visual != null)
        {
            _visual.localScale = new Vector3(currentRadius * 2f, 0.05f, currentRadius * 2f);
        }

        if (DamageArea != null)
        {
            DamageArea.AreaOfEffectDistance = currentRadius;
        }

        if (!_hasInflictedDamage && t >= 0.95f)
        {
            DamageArea.InflictDamageInArea(
                Damage,
                transform.position,
                DamageLayers,
                QueryTriggerInteraction.Ignore,
                DamageOwner
            );
            _hasInflictedDamage = true;
        }

        if (_elapsedTime >= ExpansionTime + 1f)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            FirePulse();
        }
    }

    /// <summary>
    /// Triggers the laser pulse behavior
    /// </summary>
    public void FirePulse()
    {
        _elapsedTime = 0f;
        _hasInflictedDamage = false;
        _isFiring = true;

        if (_visual != null)
        {
            _visual.localScale = Vector3.zero;
        }
    }
}
