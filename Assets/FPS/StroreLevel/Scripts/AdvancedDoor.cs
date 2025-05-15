using UnityEngine;

public class AdvancedDoor : MonoBehaviour
{
    public Transform door;
    public Vector3 openPositionOffset = new Vector3(0, 0, 5);
    public float speed = 2f;
    public float closeDelay = 3f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private float timer = 0f;

    void Start()
    {
        closedPosition = door.position;
        openPosition = closedPosition + openPositionOffset;
    }

    void Update()
    {
        if (isOpen)
        {
            door.position = Vector3.Lerp(door.position, openPosition, Time.deltaTime * speed);
            timer += Time.deltaTime;
            if (timer > closeDelay)
            {
                isOpen = false;
                timer = 0f;
            }
        }
        else
        {
            door.position = Vector3.Lerp(door.position, closedPosition, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = true;
            timer = 0f;
        }
    }
}
