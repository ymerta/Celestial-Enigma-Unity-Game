using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject); // Ayn� m�zikten 2 tane olmas�n
        }

        DontDestroyOnLoad(this.gameObject); // Sahne ge�i�inde yok olmas�n
    }
}
