using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    [TextArea(2, 5)]
    public string hintMessage = "Altın ışık seni uyandırır.";

    private bool isActive = false;

   public void ShowDialogue(string message)
{
    dialogueText.text = message;
    dialoguePanel.SetActive(true); // 👈 Paneli göster
    isActive = true;
}

void Update()
{
    if (isActive && Input.GetKeyDown(KeyCode.Return)) // ⏎ Enter ile kapat
    {
        dialoguePanel.SetActive(false); // 👈 Paneli gizle
        isActive = false;
    }
}
    
}
