using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    public static TutorialUI Instance;

    public GameObject panel;
    public TextMeshProUGUI tutorialText;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        tutorialText.text = message;
        panel.SetActive(true);
    }

    public void HideMessage()
    {
        panel.SetActive(false);
    }
}
