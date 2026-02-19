using UnityEngine;
using System.Collections;

public class MenuFader : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float fadeDuration = 0.8f; // How long the fade takes

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0; // Ensure it starts invisible
    }

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float counter = 0;
        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            // Gradually increases alpha from 0 to 1
            canvasGroup.alpha = Mathf.Lerp(0, 1, counter / fadeDuration);
            yield return null;
        }
    }
}
