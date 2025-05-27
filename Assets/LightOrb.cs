using UnityEngine;
using System.Collections;

public class LightOrb : MonoBehaviour
{
    public GameObject caster; // Orb'u atan oyuncu (Player)

    public float duration = 3f;
    public GameObject correctPortal;

    public float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
    }


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

            Destroy(gameObject); // ? sadece doðru portala çarptýðýnda yok ol
        }
    }

    IEnumerator DisableAfterSeconds(Renderer rend, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (rend != null)
            rend.enabled = false;
    }
}
