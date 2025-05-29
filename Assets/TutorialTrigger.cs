using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [TextArea]
    public string tutorialMessage;

    public float hideDelay = 3f;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            TutorialUI.Instance.ShowMessage(tutorialMessage);
            Invoke("HideMessage", hideDelay); 
        }
    }

    void HideMessage()
    {
        TutorialUI.Instance.HideMessage();
    }
}
