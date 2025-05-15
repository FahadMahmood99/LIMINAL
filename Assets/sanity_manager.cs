using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public MonoBehaviour playerController;
    private bool isFrozen = false;
    private bool animationTriggered = false;

    public float freezeTime = 2f;

    public GameObject gameOverCanvas;         // Assign in Inspector
    public Button restartButton;              // Assign in Inspector
    public Button mainMenuButton;             // Assign in Inspector

    private bool isGameOver = false;

    void Start()
    {
        postVolume.profile.TryGet(out chroma);
        postVolume.profile.TryGet(out lens);
        postVolume.profile.TryGet(out color);

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }

        // Assign button listeners
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void Update()
    {
        if (isGameOver) return;

        sanityTimer += Time.deltaTime;
        if (sanityTimer >= sanityDecreaseDelay)
        {
            sanity -= Time.deltaTime * 0.1f;
            sanity = Mathf.Clamp(sanity, 0, 100);
        }

        if (sanity <= 90f && !isFrozen)
        {
            StartCoroutine(FreezePlayer(freezeTime));
            isFrozen = true;
        }

        if (sanity <= 90f && !animationTriggered)
        {
            TriggerSanityAnimation();
            animationTriggered = true;
        }

        if (sanity <= 50f && !mildDistortionActive && sanity > 25f)
        {
            mildDistortionActive = true;
            distortionTimer = distortionDuration;
        }

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

        if (sanity <= 25f)
        {
            intenseDistortionActive = true;
        }

        if (intenseDistortionActive)
        {
            float pulse = Mathf.Sin(Time.time * 6f);
            chroma.intensity.value = 1f;
            lens.intensity.value = -0.7f + pulse * 0.1f;
            color.saturation.value = -90f + pulse * 10f;
            color.hueShift.value = Mathf.PingPong(Time.time * 200f, 360f);
        }

        if (sanity <= 0 && !isGameOver)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu"); // Replace with your actual scene name
    }

    IEnumerator FreezePlayer(float freezeDuration)
    {
        if (playerController != null)
            playerController.enabled = false;

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
            StartCoroutine(ResetSanityAnimation(freezeTime));
        }
        else
        {
            Debug.LogWarning("Animator not assigned!");
        }
    }

    IEnumerator ResetSanityAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("IsSane", true);
    }
}
