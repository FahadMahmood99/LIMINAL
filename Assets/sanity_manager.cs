//using UnityEngine;
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

//public class SanityEffects : MonoBehaviour
//{
//    public float sanity = 100f;
//    public Volume postVolume;

//    public float sanityDecreaseDelay = 10f;
//    public float distortionDuration = 3f;

//    ChromaticAberration chroma;
//    LensDistortion lens;
//    ColorAdjustments color;

//    private float sanityTimer = 0f;
//    private float distortionTimer = 0f;
//    private bool mildDistortionActive = false;
//    private bool intenseDistortionActive = false;

//    void Start()
//    {
//        postVolume.profile.TryGet(out chroma);
//        postVolume.profile.TryGet(out lens);
//        postVolume.profile.TryGet(out color);
//    }

//    void Update()
//    {
//        // Wait before sanity starts dropping
//        sanityTimer += Time.deltaTime;
//        if (sanityTimer >= sanityDecreaseDelay)
//        {
//            sanity -= Time.deltaTime * 2f;
//            sanity = Mathf.Clamp(sanity, 0, 100);
//        }

//        // Trigger mild distortion at 50% sanity
//        if (sanity <= 50f && !mildDistortionActive && sanity > 25f)
//        {
//            mildDistortionActive = true;
//            distortionTimer = distortionDuration;
//        }

//        // Mild distortion effect (short burst)
//        if (mildDistortionActive && sanity > 25f)
//        {
//            distortionTimer -= Time.deltaTime;
//            float t = Mathf.Clamp01(distortionTimer / distortionDuration);

//            chroma.intensity.value = Mathf.Lerp(0f, 0.8f, t);
//            lens.intensity.value = Mathf.Lerp(0f, -0.4f, t);
//            color.saturation.value = Mathf.Lerp(0f, -60f, t);
//            color.hueShift.value = Mathf.Lerp(0f, 120f * Mathf.Sin(Time.time * 2f), t);

//            if (distortionTimer <= 0f)
//            {
//                ResetEffects();
//                mildDistortionActive = false;
//            }
//        }

//        // Intense distortion if sanity drops below 25%
//        if (sanity <= 25f)
//        {
//            intenseDistortionActive = true;
//        }

//        // Intense ongoing effects
//        if (intenseDistortionActive)
//        {
//            float pulse = Mathf.Sin(Time.time * 6f); // fast flicker

//            chroma.intensity.value = 1f;
//            lens.intensity.value = -0.7f + pulse * 0.1f;
//            color.saturation.value = -90f + pulse * 10f;
//            color.hueShift.value = Mathf.PingPong(Time.time * 200f, 360f);
//        }
//    }

//    void ResetEffects()
//    {
//        chroma.intensity.value = 0f;
//        lens.intensity.value = 0f;
//        color.saturation.value = 0f;
//        color.hueShift.value = 0f;
//    }
//}
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;  

public class SanityEffects : MonoBehaviour
{
    public float sanity = 100f;
    public Volume postVolume;
    public Animator animator; 

    public float sanityDecreaseDelay = 10f;
    public float distortionDuration = 3f;

    ChromaticAberration chroma;
    LensDistortion lens;
    ColorAdjustments color;

    private float sanityTimer = 0f;
    private float distortionTimer = 0f;
    private bool mildDistortionActive = false;
    private bool intenseDistortionActive = false;
    public  MonoBehaviour playerController;
    private bool isFrozen = false;
    public float freezeTime = 2f;
  

    private bool animationTriggered = false;

    void Start()
    {
        postVolume.profile.TryGet(out chroma);
        postVolume.profile.TryGet(out lens);
        postVolume.profile.TryGet(out color);
    }

    void Update()
    {
        // Wait before sanity starts dropping
        sanityTimer += Time.deltaTime;
        if (sanityTimer >= sanityDecreaseDelay)
        {
            // Slow down sanity decrease to 10 minutes (0.1 per second)
            sanity -= Time.deltaTime * (0.1f);  // (100 / 600 seconds = 0.1 per second)
            sanity = Mathf.Clamp(sanity, 0, 100);
        }

        // Trigger freeze effect when sanity goes below 90
        if (sanity <= 90f && !isFrozen)
        {
            StartCoroutine(FreezePlayer(freezeTime)); // Freeze for 2 seconds
            isFrozen = true;
        }

        // Trigger animation at 90 sanity
        if (sanity <= 90f && !animationTriggered)
        {
            TriggerSanityAnimation();  // Call function to trigger animation
            animationTriggered = true; // Prevent repeated triggers
        }

        // Trigger mild distortion at 50% sanity
        if (sanity <= 50f && !mildDistortionActive && sanity > 25f)
        {
            mildDistortionActive = true;
            distortionTimer = distortionDuration;
        }

        // Mild distortion effect (short burst)
        if (mildDistortionActive && sanity > 25f)
        {
            distortionTimer -= Time.deltaTime;
            float t = Mathf.Clamp01(distortionTimer / distortionDuration);

            chroma.intensity.value = Mathf.Lerp(0f, 0.8f, t);
            lens.intensity.value = Mathf.Lerp(0f, -0.4f, t);
            color.saturation.value = Mathf.Lerp(0f, -60f, t);
            color.hueShift.value = Mathf.Lerp(0f, 120f * Mathf.Sin(Time.time * 2f), t);

            if (distortionTimer <= 0f)
            {
                ResetEffects();
                mildDistortionActive = false;
            }
        }

        // Intense distortion if sanity drops below 25%
        if (sanity <= 25f)
        {
            intenseDistortionActive = true;
        }

        // Intense ongoing effects
        if (intenseDistortionActive)
        {
            float pulse = Mathf.Sin(Time.time * 6f); // fast flicker

            chroma.intensity.value = 1f;
            lens.intensity.value = -0.7f + pulse * 0.1f;
            color.saturation.value = -90f + pulse * 10f;
            color.hueShift.value = Mathf.PingPong(Time.time * 200f, 360f);
        }
    }

    IEnumerator FreezePlayer(float freezeDuration)
    {
        if (playerController != null)
            playerController.enabled = false;

        // Wait for the specified freeze time
        yield return new WaitForSeconds(freezeDuration);

        if (playerController != null)
            playerController.enabled = true;
    }

    void ResetEffects()
    {
        chroma.intensity.value = 0f;
        lens.intensity.value = 0f;
        color.saturation.value = 0f;
        color.hueShift.value = 0f;
    }

    void TriggerSanityAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("SanityLowTrigger");
            animator.SetBool("isWalking", false);
            StartCoroutine(PlaySanityEffectSequence());
            animationTriggered = true;
        }
        else
        {
            Debug.LogWarning("Animator not assigned!");
        }
    }

    IEnumerator PlaySanityEffectSequence()
    {
        // Wait a brief moment to let the animation visibly trigger
        yield return new WaitForSeconds(0.3f);

        if (playerController != null)
            playerController.enabled = false;

        yield return new WaitForSeconds(freezeTime);

        if (playerController != null)
            playerController.enabled = true;

        animator.SetBool("IsSane", true);
    }
}
