using UnityEngine;

public class RailgunLaser : MonoBehaviour
{
    public float laserDuration = 1.5f;
    public float damage = 30f;

    public Transform visualLaser; // Assign the actual visual cube here

    private float timer;

    public void Initialize(Vector3 startPosition, Vector3 targetPosition)
    {
        transform.position = startPosition; // Just to be safe
        Vector3 direction = (targetPosition - startPosition).normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        float distance = Vector3.Distance(startPosition, targetPosition);

        if (visualLaser != null)
        {
            visualLaser.localScale = new Vector3(
                visualLaser.localScale.x,
                visualLaser.localScale.y,
                distance
            );

            visualLaser.localPosition = new Vector3(0f, 0f, distance / 2f);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var health = other.GetComponent<Unity.FPS.Game.Health>();
            if (health != null)
            {
                Debug.Log("Railgun laser hit the player!");
                health.TakeDamage(damage, gameObject);
            }
        }
    }
}
