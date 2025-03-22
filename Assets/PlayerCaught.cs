using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCaught : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�arp��ma Alg�land�: " + other.gameObject.name); // �arp��ma kontrol�

        if (other.CompareTag("Player"))
        {
            Debug.Log("Oyuncu yakaland�! Sahne s�f�rlan�yor...");
            // E�er sahne y�kleme ba�ar�s�z olursa hata mesaj� alaca��z
            try
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("Sahne Ba�ar�yla Yeniden Y�klendi!");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Sahne y�klenirken hata olu�tu: " + e.Message);
            }
        }
    }
}
