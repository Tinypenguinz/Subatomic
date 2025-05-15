using UnityEngine;

// orignal is 3x states to switch through, new ver. linear interpolation between 3x states.
public class ShrinkableObject : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 targetScale;
    public float transitionSpeed = 5f; // can be editied in inspector.
    private bool isTransitioning = false; // check if transitioning

    void Start()
    {
        // Original scale, just to make neutral work.
        originalScale = transform.localScale;
        targetScale = originalScale; // reset on start.
        
    }

    void Update()
    {
        if (isTransitioning)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime* transitionSpeed);
                // delta time is based on frame timings
            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
            {
                transform.localScale = targetScale; // force exact scale... could cause choppiness?
                isTransitioning = false;
            }
        }
    }

    public void Shrink()
    {
        targetScale = originalScale * 0.5f;
        isTransitioning = true;
        Debug.Log("Shrink");

    }

    public void Grow()
    {
        targetScale = originalScale * 2f;
        isTransitioning = true;
        Debug.Log("Grow");

    }

    public void Neutral()
    {
        targetScale = originalScale; 
        Debug.Log("Neutral");

    }
}
