using UnityEngine;

public class StunSpellGiverNPC : MonoBehaviour
{
    public PlayerStunSpell playerStunScript;
    public GameObject interactionUI;
    public GameObject stunSpellSlotUI;
    public DialogueUI dialogueSystem;

    private bool playerInRange = false;
    private bool hasGivenStun = false;

    private string[] stunLines = new string[]
    {
        "This place was once silent...",
        "But the restless dead began to stir.",
        "Take this power — a stun spell to halt them in their path.",
        "Use it sparingly. Its energy takes time to recover."
    };

    private int currentLineIndex = 0;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueSystem == null || hasGivenStun)
                return;

            if (currentLineIndex < stunLines.Length)
            {
                dialogueSystem.ShowDialogue(stunLines[currentLineIndex]);

                // 3. satıra geldiğinde (index 2) büyüyü ver
                if (currentLineIndex == 2)
                {
                    GiveStunSpell();
                }

                currentLineIndex++;
            }
        }
    }

    void GiveStunSpell()
    {
        hasGivenStun = true;

        if (playerStunScript != null)
            playerStunScript.hasStunSpell = true;

        if (stunSpellSlotUI != null)
            stunSpellSlotUI.SetActive(true);

        Debug.Log("✅ Stun büyüsü verildi!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!hasGivenStun && interactionUI != null)
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
