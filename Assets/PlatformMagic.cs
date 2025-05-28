using UnityEngine;

public class PlatformMagic : MonoBehaviour
{
    public float magicRange = 5f;
    public float moveSpeed = 3f;

    private GameObject selectedPlatform = null;

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.F))
{
    GameObject[] platforms = GameObject.FindGameObjectsWithTag("Selectable");
    GameObject closest = null;
    float minDistance = Mathf.Infinity;

    foreach (GameObject platform in platforms)
    {
        if (platform == selectedPlatform) continue; // Aynı platform tekrar seçilmesin

        float distance = Vector3.Distance(transform.position, platform.transform.position);
        if (distance < minDistance && distance < magicRange)
        {
            minDistance = distance;
            closest = platform;
        }
    }

    if (closest != null)
    {
        selectedPlatform = closest;
        Debug.Log("Yeni platform seçildi: " + closest.name);
    }
}


        // Ctrl tuşuna basılırsa seçimi bırak
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (selectedPlatform != null)
            {
                Debug.Log("Platform bırakıldı: " + selectedPlatform.name);
                selectedPlatform = null;
            }
        }

        // Ok tuşlarıyla platformu hareket ettir
        if (selectedPlatform != null)
        {
            Vector3 movement = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow))
                movement += Vector3.up;
            if (Input.GetKey(KeyCode.DownArrow))
                movement += Vector3.down;
            if (Input.GetKey(KeyCode.LeftArrow))
                movement += Vector3.left;
            if (Input.GetKey(KeyCode.RightArrow))
                movement += Vector3.right;

            selectedPlatform.transform.position += movement * moveSpeed * Time.deltaTime;
        }
    }
}
