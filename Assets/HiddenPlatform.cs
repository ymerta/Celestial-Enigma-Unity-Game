using UnityEngine;
using System.Collections;

public class HiddenPlatform : MonoBehaviour
{
    private Collider col;
    private Renderer rend;

    void Awake()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
        SetVisibility(false);
    }

    public void RevealTemporarily(float duration)
    {
        SetVisibility(true);
        StartCoroutine(HideAfterDelay(duration));
    }

    private void SetVisibility(bool visible)
    {
        if (col != null) col.enabled = visible;
        if (rend != null) rend.enabled = visible;
    }

    IEnumerator HideAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        SetVisibility(false);
    }
}
