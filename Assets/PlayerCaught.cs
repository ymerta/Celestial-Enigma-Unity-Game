using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCaught : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Çarpýþma Algýlandý: " + other.gameObject.name); // Çarpýþma kontrolü

        if (other.CompareTag("Player"))
        {
            Debug.Log("Oyuncu yakalandý! Sahne sýfýrlanýyor...");
            // Eðer sahne yükleme baþarýsýz olursa hata mesajý alacaðýz
            try
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("Sahne Baþarýyla Yeniden Yüklendi!");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Sahne yüklenirken hata oluþtu: " + e.Message);
            }
        }
    }
}
