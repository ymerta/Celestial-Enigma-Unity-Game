using UnityEngine;

public class LightOrbTrigger : MonoBehaviour
{
    public HiddenPlatform linkedPlatform;
    public float revealDuration = 3f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("LightOrb")) return;

        LightOrb orb = other.GetComponent<LightOrb>();
        if (orb == null) return;

        // E�er Orb do�al� �ok az olduysa (�rne�in 0.1 saniyeden az), g�rmezden gel
        if (Time.time - orb.spawnTime < 0.1f) return;

        if (linkedPlatform != null)
        {
            linkedPlatform.RevealTemporarily(revealDuration);
        }

        Destroy(other.gameObject);
    }

}
