using UnityEngine;

public class ShockwaveController : MonoBehaviour
{
    public float expansionSpeed = 10f;
    public float maxRadius = 20f;
    public float damage = 25f;
    public float duration = 1f;

    private float currentRadius = 0f;
    private bool isExpanding = false;
    private Vector3 initialScale;
    private Vector3 startPosition;

    void Start()
    {
        initialScale = transform.localScale;
        startPosition = transform.position;
        
    }

    void Update()
    {
        if (isExpanding)
        {
            currentRadius += expansionSpeed * Time.deltaTime;

            float scale = currentRadius;
            transform.localScale = new Vector3(scale, initialScale.y, scale);

            if (currentRadius >= maxRadius)
            {
                ResetShockwave();
            }
        }
    }

    public void ActivateShockwave()
    {
        currentRadius = 0f;
        transform.localScale = initialScale;
        isExpanding = true;
        gameObject.SetActive(true);
        Debug.Log("Shockwave Activated at: " + Time.time);
    }

    private void ResetShockwave()
    {
        isExpanding = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Example damage call; replace with your actual damage method
            var health = other.GetComponent<Unity.FPS.Game.Health>();
            if (health != null)
            {
                Debug.Log("Shockwave hit the player!");
                health.TakeDamage(damage, gameObject);
            }
        }
    }

}
