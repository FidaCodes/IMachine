using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    public Transform door; // Assign the door object here
    public Vector3 openPositionOffset = new Vector3(0, 0, 5); // How far it moves
    public float openSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false;

    void Start()
    {
        closedPosition = door.position;
        openPosition = closedPosition + openPositionOffset;
    }

    void Update()
    {
        if (isOpening)
        {
            door.position = Vector3.Lerp(door.position, openPosition, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpening = true;
        }
    }
}
