using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI; // If using UI Text
// using TMPro; // Use this if you’re using TextMeshPro

public class SwitchTrigger : MonoBehaviour
{
    public Transform lever;
    public Renderer[] visualRenderers;
    public Material switchOnMaterial;
    public DoorManager doorManager;

    public GameObject promptUI; // Assign your UI prompt object here

    private bool isPlayerNearby = false;
    private bool isActivated = false;

    void Update()
    {
        if (isPlayerNearby && !isActivated && Input.GetKeyDown(KeyCode.E))
        {
            ActivateSwitch();
            promptUI.SetActive(false);
        }
    }

    private void ActivateSwitch()
    {
        isActivated = true;
        lever.localRotation = Quaternion.Euler(0f, -90f, -45f);

        foreach (Renderer rend in visualRenderers)
        {
            if (rend != null)
                rend.material = switchOnMaterial;
        }

        if (doorManager != null)
        {
            doorManager.RegisterSwitchActivated();
            EventManager.Broadcast(new SwitchActivatedEvent(doorManager.GetRemainingSwitches()));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated && other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            promptUI.SetActive(false);
        }
    }
}
