using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public CanvasGroup canvasGroup;
    public float displayTime = 3f;

    private Coroutine currentRoutine;

    public void ShowDialogue(string message)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(message));
    }

    private IEnumerator ShowRoutine(string message)
    {
        // ✅ Aktif et
        dialoguePanel.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        canvasGroup.alpha = 1f;

        dialogueText.text = message;

        yield return new WaitForSeconds(displayTime);

        // ✅ Kapat
        dialogueText.text = "";
        dialogueText.gameObject.SetActive(false);
        canvasGroup.alpha = 0f;
        
    }
}
