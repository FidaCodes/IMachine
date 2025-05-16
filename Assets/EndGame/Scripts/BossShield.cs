using UnityEngine;

public class BossShield : MonoBehaviour
{
    public GameObject shieldVisual;
    public int totalPillars = 4;
    private int remainingPillars;

    public Material shieldNormalMat;
    public Material shieldCrackedMat;
    public Renderer shieldRenderer;

    public BossAI boss; // Reference to BossAI script

    void Start()
    {
        remainingPillars = totalPillars;
        if (shieldRenderer != null)
            shieldRenderer.material = shieldNormalMat;

        shieldVisual.SetActive(true);
    }

    public void NotifyPillarDestroyed()
    {
        remainingPillars--;

        if (remainingPillars == 2 && shieldRenderer != null)
        {
            shieldRenderer.material = shieldCrackedMat;
        }

        if (remainingPillars <= 0)
        {
            DisableShield();
        }
    }

    void DisableShield()
    {
        shieldVisual.SetActive(false);
        boss?.DisableShield(); // Make boss vulnerable
        Debug.Log("Shield down! Boss is now vulnerable.");
    }
}
