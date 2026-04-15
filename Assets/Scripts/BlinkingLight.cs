using UnityEngine;

public class BlinkLight : MonoBehaviour
{
    public float blinkRate = 0.5f; // Seconds between blinks
    private Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
        InvokeRepeating("ToggleLight", blinkRate, blinkRate);
    }

    void ToggleLight()
    {
        myLight.enabled = !myLight.enabled;
    }
}