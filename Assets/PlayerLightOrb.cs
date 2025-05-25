using UnityEngine;

public class PlayerLightOrb : MonoBehaviour
{
    [Header("Orb Ayarlar�")]
    public GameObject lightOrbPrefab;
    public float shootForce = 10f;
    public float cooldown = 3f;
    public float orbLifetime = 3f;

    private float lastUsedTime;
    private bool hasLearnedOrb = false;

    void Update()
    {
        // B�y� ��renilmemi�se hi�bir �ey yapma
        if (!hasLearnedOrb)
            return;

        // Q tu�una bas�ld���nda ve cooldown ge�ti�inde
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= lastUsedTime + cooldown)
        {
            ShootLightOrb();
            lastUsedTime = Time.time;
        }
    }

    public void LearnLightOrb()
    {
        hasLearnedOrb = true;
        Debug.Log("LightOrb b�y�s� ��renildi!");
    }

    public GameObject correctPortal; // Bunu Inspector'dan atayacaks�n

    void ShootLightOrb()
    {
        if (correctPortal == null) return;

        Vector3 spawnPos = transform.position + Vector3.up * 1.2f;
        Vector3 direction = (correctPortal.transform.position - spawnPos).normalized;

        GameObject orb = Instantiate(lightOrbPrefab, spawnPos, Quaternion.LookRotation(direction));

        Rigidbody rb = orb.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * shootForce, ForceMode.VelocityChange);
        }

        Destroy(orb, orbLifetime);
    }


}
