using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Transform door;
    public int totalSwitches = 3;
    private int activatedSwitches = 0;
    public MeshRenderer[] indicatorRenderers; // Assign cubes here
    public Material greenMaterial; // Assign EmissiveGreen


    public Vector3 openOffset = new Vector3(0, 3, 0);
    public float openSpeed = 2f;
    private bool doorOpening = false;
    private Vector3 closedPos;
    private Vector3 openPos;

    void Start()
    {
        closedPos = door.position;
        openPos = closedPos + openOffset;
    }

    public void RegisterSwitchActivated()
    {
        if (activatedSwitches < indicatorRenderers.Length)
        {
            indicatorRenderers[activatedSwitches].material = greenMaterial;
        }

        activatedSwitches++;

        if (activatedSwitches >= totalSwitches)
        {
            doorOpening = true;
        }
    }

    public int GetRemainingSwitches()
    {
        return totalSwitches - activatedSwitches;
    }

    void Update()
    {
        if (doorOpening)
        {
            door.position = Vector3.Lerp(door.position, openPos, Time.deltaTime * openSpeed);
        }
    }
}
