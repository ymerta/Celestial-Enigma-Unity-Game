using UnityEngine;

public class CrackAndFall : MonoBehaviour
{
    public GameObject platformToFall;
    public float delay = 1f;
    public float fallSpeed = 2f;
    public float destroyDelay = 3f;
    private bool shouldFall = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Oyuncu trigger'a girdi."); // test logu
            Invoke(nameof(StartFalling), delay);
        }
    }

    void StartFalling()
    {
        shouldFall = true;
        Destroy(platformToFall, destroyDelay);
    }

    void Update()
    {
        if (shouldFall && platformToFall != null)
        {
            platformToFall.transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
    }
}
