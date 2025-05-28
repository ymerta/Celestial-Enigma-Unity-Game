using UnityEngine;

public class PlatformSelector : MonoBehaviour
{
    public float selectionRange = 5f; // Önünde 5 birimlik alan taranacak
    private GameObject selectedPlatform;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TrySelectPlatform();
        }
    }

    void TrySelectPlatform()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, selectionRange))
        {
            if (hit.collider.CompareTag("Selectable"))
            {
                selectedPlatform = hit.collider.gameObject;
                Debug.Log("Platform seçildi: " + selectedPlatform.name);

                // Burada büyü uygulanabilir. Örnek:
                MovePlatformUp(selectedPlatform);
            }
            else
            {
                Debug.Log("Seçilen obje 'Selectable' değil.");
            }
        }
    }

    void MovePlatformUp(GameObject platform)
    {
        // Basit yukarı zıplatma (test için)
        platform.transform.position += Vector3.up * 2f;
    }
}
