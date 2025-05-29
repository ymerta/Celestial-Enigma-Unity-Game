using UnityEngine;

public class PlatformSpellGiverNPC : MonoBehaviour
{
    public PlatformMagic playerScript; // Oyuncunun büyü kontrolü burada
    public GameObject interactionUI;   // "E’ye bas" mesajı
    public GameObject platformSpellSlotUI; // SpellSlot3 (ikon GameObject’i)
    public DialogueUI dialogueSystem;  // Diyalog sistemi

    private bool playerInRange = false;
    private bool hasGivenPlatformSpell = false;

    private string[] finalLines = new string[]
    {
        "This is the last gift I can offer you.",
        "With this spell, you can move what was once immovable.",
        "The path ahead won't reveal itself... unless you reshape it.",
        "This labyrinth is the final test.",
        "Not of your strength — but of your understanding.",
        "Move the stones. Find the light. End the cycle.",
        "Go now, child of light... finish what your bloodline began."
    };

    private int currentLineIndex = 0;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueSystem == null)
                return;

            if (currentLineIndex < finalLines.Length)
            {
                dialogueSystem.ShowDialogue(finalLines[currentLineIndex]);

                // 2. satırda büyü ver
                if (currentLineIndex == 1 && !hasGivenPlatformSpell)
                {
                    hasGivenPlatformSpell = true;

                    if (playerScript != null)
                    {
                        playerScript.hasPlatformSpell = true;

                        if (playerScript.platformSpellSlotUI != null)
                            playerScript.platformSpellSlotUI.SetActive(true);
                    }

                    Debug.Log("✅ Platform Movement Spell granted!");
                }

                currentLineIndex++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!hasGivenPlatformSpell && interactionUI != null)
                interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactionUI != null)
                interactionUI.SetActive(false);
        }
    }
}
