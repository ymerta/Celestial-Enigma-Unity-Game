using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLogic : MonoBehaviour
{
    public enum PortalType { Correct, Loop }
    public PortalType portalType = PortalType.Loop;

    public string nextSceneName = "Level2_RiftWorld";
    public string loopSceneName = "LoopRoom";

    private static int wrongTryCount = 0;
    private bool isPlayerNear = false;

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (portalType == PortalType.Correct)
            {
                Debug.Log("Doğru portal! İleri gidiliyor...");
                wrongTryCount = 0;
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                wrongTryCount++;
                Debug.Log("Yanlış portal! Sayı: " + wrongTryCount);

                if (wrongTryCount >= 3)
                {
                    Debug.Log("3 yanlış → başa dön!");
                    wrongTryCount = 0;
                    SceneManager.LoadScene("DreamTemple");
                }
                else
                {
                    SceneManager.LoadScene(loopSceneName);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = false;
    }
}
