using UnityEngine;

public class LightOrbGiverNPC : MonoBehaviour
{
    public PlayerLightOrb playerScript;
    public GameObject interactionUI; // "E’ye bas" mesajı (isteğe bağlı)
    private bool playerInRange = false;
    private bool hasGivenOrb = false;


    void Update()
    {
        if (playerInRange && !hasGivenOrb && Input.GetKeyDown(KeyCode.E))
        {
            playerScript.LearnLightOrb();
            hasGivenOrb = true;

            if (interactionUI != null)
                interactionUI.SetActive(false);

        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (interactionUI != null)
                interactionUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactionUI != null)
                interactionUI.SetActive(false);
        }
    }
}
