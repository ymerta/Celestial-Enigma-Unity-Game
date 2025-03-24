using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLogic : MonoBehaviour
{
    public enum PortalType { Correct, Loop }
    public PortalType portalType = PortalType.Loop;

    public string nextSceneName = "Level2_RiftWorld"; // Doğru portal sonrası
    public string loopSceneName = "LoopRoom";         // Yanlış portal sonrası

    private static int wrongTryCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

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
