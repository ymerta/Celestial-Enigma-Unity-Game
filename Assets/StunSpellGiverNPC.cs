using UnityEngine;

public class StunSpellGiverNPC : MonoBehaviour
{
    public PlayerStunSpell playerStunScript;
    public GameObject interactionUI; // "E'ye bas" yazısı
    private bool playerInRange = false;
    private bool hasGivenStun = false;
    public GameObject stunSpellSlotUI; // Inspector'dan SpellSlot2 atanacak

    void Update()
    {
        if (playerInRange && !hasGivenStun && Input.GetKeyDown(KeyCode.E))
        {
            hasGivenStun = true;

            if (interactionUI != null)
                interactionUI.SetActive(false);

            if (stunSpellSlotUI != null)
                stunSpellSlotUI.SetActive(true); // 🔹 UI'da stun slotunu görünür yap
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
