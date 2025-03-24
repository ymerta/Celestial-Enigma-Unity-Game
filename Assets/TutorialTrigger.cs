using UnityEngine;
using TMPro;

public class TutorialTrigger : MonoBehaviour
{
    public string tutorialMessage = "W ve D ile hareket et.";
    public GameObject tutorialUI; // Text UI nesnesi

    private void Start()
    {
        tutorialUI.SetActive(false); // Baþta görünmesin
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialUI.SetActive(true);
            tutorialUI.GetComponentInChildren<TextMeshProUGUI>().text = tutorialMessage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialUI.SetActive(false);
        }
    }
}
