using System;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public Renderer targetRenderer;
    public Color baseEmissionColor = Color.white;
    public float minIntensity = 0.1f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f;

    private Material flickerMat;
    private float timer;

    void Start()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        // Duplicate the material so changes don't affect other objects
        flickerMat = targetRenderer.material;
        flickerMat.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            float intensity = UnityEngine.Random.Range(minIntensity, maxIntensity);
            Color finalColor = baseEmissionColor * intensity;

            flickerMat.SetColor("_EmissionColor", finalColor);

            // Optional: Sync with lighting if using baked GI
            DynamicGI.SetEmissive(targetRenderer, finalColor);

            timer = flickerSpeed;
        }
    }
}
