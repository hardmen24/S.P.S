using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Scale Settings")]
    public float resizeDuration = 0.5f; // How fast it grows/shrinks
    private bool isVisible = false;      // Tracks if it is currently appeared (true) or disappeared (false)
    private Coroutine resizeCoroutine;   // Keeps track of the running routine so we can stop it safely

    public void ToggleYScale()
{
    // If a resize is already happening, stop it first so they don't fight
    if (resizeCoroutine != null)
    {
        StopCoroutine(resizeCoroutine);
    }

    // Toggle the state
    isVisible = !isVisible;

    // Determine the target Y scale based on the state
    float targetY = isVisible ? 2.5f : 0f;

    // Start the gradual scaling
    resizeCoroutine = StartCoroutine(ResizeYOverTime(targetY));
}

// The internal worker function that changes the size frame-by-frame
private IEnumerator ResizeYOverTime(float targetY)
{
    Vector3 currentScale = transform.localScale;
    float startY = currentScale.y;
    float elapsedTime = 0f;

    while (elapsedTime < resizeDuration)
    {
        elapsedTime += Time.deltaTime;
        float percentage = elapsedTime / resizeDuration;

        // Smoothly blend only the Y value
        float newY = Mathf.Lerp(startY, targetY, percentage);
        
        // Apply it back to the transform while keeping X and Z exactly the same
        transform.localScale = new Vector3(currentScale.x, newY, currentScale.z);

        yield return null; // Wait until the next frame
    }

    // Ensure it snaps perfectly to the final target value
    transform.localScale = new Vector3(currentScale.x, targetY, currentScale.z);
    resizeCoroutine = null; 
}
}
