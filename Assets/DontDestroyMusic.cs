using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject); // Ayný müzikten 2 tane olmasýn
        }

        DontDestroyOnLoad(this.gameObject); // Sahne geçiþinde yok olmasýn
    }
}
