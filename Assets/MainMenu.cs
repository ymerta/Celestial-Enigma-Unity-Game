using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("DreamTemple"); // �lk sahnenin ad�n� buraya yaz
    }

    public void QuitGame()
    {
        
        Application.Quit(); // Build al�nca �al���r
    }
}
