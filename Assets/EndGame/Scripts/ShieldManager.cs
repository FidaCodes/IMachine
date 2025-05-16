using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public GameObject shieldObject; // Assign the shield sphere
    public BossAI boss; // Reference to BossAI

    private int switchesActivated = 0;
    public int totalSwitchesRequired = 2;

    public void RegisterSwitch()
    {
        switchesActivated++;
        Debug.Log($"Switch Activated: {switchesActivated}/{totalSwitchesRequired}");

        if (switchesActivated >= totalSwitchesRequired)
        {
            DisableShield();
        }
    }

    void DisableShield()
    {
        if (shieldObject != null)
            shieldObject.SetActive(false);

        if (boss != null)
            boss.DisableShield();
    }
}
