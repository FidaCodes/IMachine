using UnityEngine;

public class ShockwaveTester : MonoBehaviour
{
    public ShockwaveController shockwave;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            shockwave.ActivateShockwave();
        }
    }
}
