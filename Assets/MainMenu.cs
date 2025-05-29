using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("DreamTemple"); // Ýlk sahnenin adýný buraya yaz
    }

    public void QuitGame()
    {
        
        Application.Quit(); // Build alýnca çalýþýr
    }
}
