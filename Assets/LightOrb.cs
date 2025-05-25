using UnityEngine;
using System.Collections;

public class LightOrb : MonoBehaviour
{
    public float duration = 3f;
    public GameObject correctPortal;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == correctPortal)
        {
            Renderer rend = other.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.enabled = true;
                StartCoroutine(DisableAfterSeconds(rend, duration));
            }

            Destroy(gameObject); // ? sadece do�ru portala �arpt���nda yok ol
        }
    }

    IEnumerator DisableAfterSeconds(Renderer rend, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (rend != null)
            rend.enabled = false;
    }
}
