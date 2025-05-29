using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI bileşenlerini kullanabilmek için
using System.Collections;
using TMPro; // TextMeshPro kullanımı için



public class PlayerLightOrb : MonoBehaviour
{
    [Header("Orb Ayarları")]
    public GameObject lightOrbPrefab;
    public float shootForce = 10f;
    public float cooldown = 3f;
    public float orbLifetime = 3f;
    public Image lightOrbIcon; // Inspector'dan atanacak
    private float lastUsedTime;
    private bool hasLearnedOrb = false;
    public TMP_Text orbCooldownText; // Inspector'dan atayacaksın
    public string hideZoneTag = "HideZone"; // Inspector'dan ayarlanabilir
    public Material highlightMaterial; // Highlight için
    void Start()
    {
        // Eğer bu MazeshiftRealm sahnesiyse, büyüyü otomatik öğrenmiş olarak başlasın
        if (SceneManager.GetActiveScene().name == "MazeShiftRealm")
        {
            LearnLightOrb();
        }
        if (SceneManager.GetActiveScene().name == "HidingFromEnemy")
        {
            LearnLightOrb();
        }
    }

    void Update()
    {
        // Büyü öğrenilmemişse hiçbir şey yapma
        if (!hasLearnedOrb)
            return;

        // Q tuşuna basıldığında ve cooldown geçtiğinde
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= lastUsedTime + cooldown)
        {
            ShootLightOrb();
            lastUsedTime = Time.time;
        }
    }
    IEnumerator SetOrbIconCooldown(float duration)
    {
        if (lightOrbIcon != null)
        {
            lightOrbIcon.color = new Color(0.3f, 0.3f, 0.3f, 1f); // Daha gri/siyaha yakın

        }

        if (orbCooldownText != null)
        {
            orbCooldownText.gameObject.SetActive(true);
        }

        float remaining = duration;

        while (remaining > 0)
        {
            if (orbCooldownText != null)
            {
                orbCooldownText.text = Mathf.CeilToInt(remaining).ToString(); // 3, 2, 1
            }

            yield return new WaitForSeconds(1f);
            remaining -= 1f;
        }

        if (lightOrbIcon != null)
        {
            lightOrbIcon.color = Color.white;
        }

        if (orbCooldownText != null)
        {
            orbCooldownText.gameObject.SetActive(false);
        }
    }


    public void LearnLightOrb()
    {
        hasLearnedOrb = true;
        Debug.Log("LightOrb büyüsü öğrenildi!");
    }

    public GameObject correctPortal; // Bunu Inspector'dan atayacaksın

    void ShootLightOrb()
    {
        Vector3 spawnPos = transform.position + Vector3.up * 1.2f;

        // 🔍 En yakın kelebek / hide zone’u bul
        GameObject targetZone = FindClosestHideZone(spawnPos);

        Vector3 direction;

        if (targetZone != null)
        {
            direction = (targetZone.transform.position - spawnPos).normalized;
            HighlightHideZone(targetZone); // ✅ Kelebeği parlat
        }
        else if (correctPortal != null)
        {
            direction = (correctPortal.transform.position - spawnPos).normalized;
        }
        else
        {
            direction = Vector3.right;
        }

        GameObject orb = Instantiate(lightOrbPrefab, spawnPos, Quaternion.LookRotation(direction));

        Rigidbody rb = orb.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * shootForce, ForceMode.VelocityChange);
        }

        StartCoroutine(SetOrbIconCooldown(cooldown));
        Destroy(orb, orbLifetime);
    }
    GameObject FindClosestHideZone(Vector3 fromPosition)
    {
        GameObject[] hideZones = GameObject.FindGameObjectsWithTag("HideZone");
        GameObject closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject zone in hideZones)
        {
            float dist = Vector3.Distance(fromPosition, zone.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = zone;
            }
        }

        return closest;
    }


    void HighlightHideZone(GameObject zone)
    {
        Renderer rend = zone.GetComponent<Renderer>();
        if (rend != null && highlightMaterial != null)
        {
            Material originalMat = rend.material; // eski hali sakla
            rend.material = highlightMaterial;

            // 3 saniye sonra geri döndür
            StartCoroutine(ResetMaterialAfterDelay(rend, originalMat, 3f));
        }
        else
        {
            Debug.LogWarning("Highlight başarısız: Renderer ya da Material eksik.");
        }
    }
    IEnumerator ResetMaterialAfterDelay(Renderer rend, Material originalMat, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (rend != null)
            rend.material = originalMat;
    }


}
