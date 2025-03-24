using UnityEngine;

public class NPCIllusion : MonoBehaviour
{
    public bool isRealNPC = false;
    public DialogueUI dialogueSystem; // 👈 Bu satır eksikse hata olur

    private bool playerInRange = false;

    void Update()
    {
        if (isRealNPC && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueSystem != null)
            {
                dialogueSystem.ShowDialogue("Altın ışık seni uyandırır.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!isRealNPC)
            {
                Debug.Log("Sahte NPC – kayboluyor.");
                Destroy(gameObject, 2f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
