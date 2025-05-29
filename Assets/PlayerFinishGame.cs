using UnityEngine;

public class PlayerFinishGame : MonoBehaviour
{
    public GameObject endGameCanvas;  // Inspector’dan Canvas atanacak

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hat"))  // "Hat" tag'li objeye çarptýysa
        {
            EndGame();
        }
    }

    void EndGame()
    {
        if (endGameCanvas != null)
        {
            endGameCanvas.SetActive(true); // Paneli göster
        }

        Time.timeScale = 0f; // Oyunu durdur
        Cursor.lockState = CursorLockMode.None; // Fareyi serbest býrak
        Cursor.visible = true;
    }
}
