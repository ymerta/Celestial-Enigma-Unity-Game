using UnityEngine;

public class NPCIllusion : MonoBehaviour
{
    public bool isRealNPC = false;
    public int npcID = 0;
    public DialogueUI dialogueSystem;

    public GameObject spellPanel; // ✅ Büyü paneli veya ikonu (SpellPanel)

    private bool playerInRange = false;
    private bool hasGivenOrb = false;

    // Çok satırlı diyalog için
    private string[] realNpcLines = new string[]
    {
        "I knew your grandmother... long before all of this began.",
        "She told me that one day, you'd come seeking the truth.",
        "When that time came, I was to pass on the gift.",
        "The Light Orb is not just magic — it’s a memory, a key.",
        "Use it wisely. It will reveal what hides in plain sight."
    };
    private int currentLineIndex = 0;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueSystem == null) return;

            if (isRealNPC)
            {
                if (currentLineIndex < realNpcLines.Length)
                {
                    dialogueSystem.ShowDialogue(realNpcLines[currentLineIndex]);

                    // ✅ 4. cümleden sonra büyü panelini göster (index 3)
                    if (currentLineIndex == 3 && spellPanel != null)
                    {
                        spellPanel.SetActive(true);
                    }

                    currentLineIndex++;
                }

                // Orb'u sadece 1 kez verelim
                if (!hasGivenOrb && currentLineIndex == realNpcLines.Length)
                {
                    hasGivenOrb = true;
                    // Büyü verme fonksiyonu burada çağrılır
                    Object.FindFirstObjectByType<PlayerLightOrb>().enabled = true;
                    Debug.Log("Light Orb ability granted!");
                }
            }
            else
            {
                // Sahte NPC'ler
                switch (npcID)
                {
                    case 1:
                        dialogueSystem.ShowDialogue("Light is a trick of the mind. Trust the shadow instead.");
                        break;
                    case 2:
                        dialogueSystem.ShowDialogue("Every light leads somewhere... but not all places are worth reaching.");
                        break;
                    default:
                        dialogueSystem.ShowDialogue("Sometimes the brightest path leads to the darkest end.");
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
