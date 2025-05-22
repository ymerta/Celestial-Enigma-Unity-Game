using UnityEngine;

public class LightOrb : MonoBehaviour
{
    public float duration = 3f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Revealable"))
        {
            Renderer rend = other.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.enabled = true;
                StartCoroutine(DisableAfterSeconds(rend, duration));
            }
        }

        Destroy(gameObject); // ýþýk küresi kaybolsun
    }

    System.Collections.IEnumerator DisableAfterSeconds(Renderer rend, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        rend.enabled = false;
    }
}
