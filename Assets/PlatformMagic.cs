using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlatformMagic : MonoBehaviour
{
    public float magicRange = 5f;
    public float moveSpeed = 3f;

    private CharacterMovement3D characterMovement;
    private GameObject selectedPlatform = null;
    private Material originalMaterial;

    [Header("Highlight")]
    public Material highlightMaterial;

    [Header("Arrow UI")]
    public GameObject arrowUp;
    public GameObject arrowDown;
    public GameObject arrowLeft;
    public GameObject arrowRight;
    public Color enabledColor = Color.white;
    public Color disabledColor = Color.gray;
    [Header("Platform Spell UI")]
    public GameObject platformSpellSlotUI; // UI’daki SpellSlot3 objesi

    [Header("UI Panel")]
    public GameObject directionPanel;

    [Header("Stun UI")]
    public Image stunIcon;
    public TMP_Text stunCooldownText;
    public float stunRange = 5f;
    public float stunDuration = 3f;
    public float stunCooldown = 5f;
    private float lastStunTime;
    [Header("Spell Unlock")]
    public bool hasPlatformSpell = false;

  void Start()
{
    characterMovement = FindFirstObjectByType<CharacterMovement3D>();

    if (directionPanel != null)
        directionPanel.SetActive(false);

    if (stunCooldownText != null)
        stunCooldownText.gameObject.SetActive(false);

   
}


    void Update()
    {

        HandleStunSpell(); // her zaman çalışır



        if (!hasPlatformSpell) return; // 🔒 Büyü henüz alınmamışsa hiçbir şey yapılmaz
        // === PLATFORM SEÇME ===
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Selectable");
            GameObject closest = null;
            float minDistance = Mathf.Infinity;

            foreach (GameObject platform in platforms)
            {
                if (platform == selectedPlatform) continue;

                float distance = Vector3.Distance(transform.position, platform.transform.position);
                if (distance < minDistance && distance < magicRange)
                {
                    minDistance = distance;
                    closest = platform;
                }
            }

            if (closest != null)
            {
                if (selectedPlatform != null && originalMaterial != null)
                {
                    Renderer prevRenderer = selectedPlatform.GetComponent<Renderer>();
                    if (prevRenderer != null)
                        prevRenderer.material = originalMaterial;
                }

                selectedPlatform = closest;
                Renderer newRenderer = selectedPlatform.GetComponent<Renderer>();
                if (newRenderer != null)
                {
                    originalMaterial = newRenderer.material;
                    newRenderer.material = highlightMaterial;
                }

                if (characterMovement != null)
                {
                    characterMovement.isControllable = false;
                    AttachPlayerIfOnPlatform(closest);
                }

                ShowPlatformDirectionIndicators(closest);
            }
        }

        // === PLATFORM BIRAKMA ===
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (selectedPlatform != null)
            {
                Renderer rend = selectedPlatform.GetComponent<Renderer>();
                if (rend != null && originalMaterial != null)
                    rend.material = originalMaterial;

                selectedPlatform = null;
                originalMaterial = null;

                if (characterMovement != null)
                {
                    characterMovement.transform.SetParent(null);
                    characterMovement.isControllable = true;
                }

                HideAllDirectionIndicators();
            }
        }

        // === PLATFORM HAREKET ===
        if (selectedPlatform != null)
        {
            Vector3 movement = Vector3.zero;
            PlatformMovementLimits limits = selectedPlatform.GetComponent<PlatformMovementLimits>();

            if (Input.GetKey(KeyCode.UpArrow) && (limits == null || limits.allowUp))
                movement += Vector3.up;
            if (Input.GetKey(KeyCode.DownArrow) && (limits == null || limits.allowDown))
                movement += Vector3.down;
            if (Input.GetKey(KeyCode.LeftArrow) && (limits == null || limits.allowLeft))
                movement += Vector3.left;
            if (Input.GetKey(KeyCode.RightArrow) && (limits == null || limits.allowRight))
                movement += Vector3.right;

            selectedPlatform.transform.position += movement * moveSpeed * Time.deltaTime;
        }

        // === STUN BÜYÜSÜ ===
        if (Input.GetKeyDown(KeyCode.R) && Time.time >= lastStunTime + stunCooldown)
        {
            CastStun();
            lastStunTime = Time.time;

            if (stunIcon != null)
                StartCoroutine(SetStunIconCooldown(stunCooldown));
        }
    }

    void CastStun()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, stunRange);

        foreach (Collider hit in hits)
        {
            EnemyAI enemy = hit.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.Stun(stunDuration);
            }
        }

        Debug.Log("Stun büyüsü kullanıldı!");
    }

    IEnumerator SetStunIconCooldown(float duration)
    {
        if (stunIcon != null)
            stunIcon.color = new Color(0.3f, 0.3f, 0.3f, 1f);

        if (stunCooldownText != null)
            stunCooldownText.gameObject.SetActive(true);

        float remaining = duration;

        while (remaining > 0)
        {
            if (stunCooldownText != null)
                stunCooldownText.text = Mathf.CeilToInt(remaining).ToString();

            yield return new WaitForSeconds(1f);
            remaining -= 1f;
        }

        if (stunIcon != null)
            stunIcon.color = Color.white;

        if (stunCooldownText != null)
            stunCooldownText.gameObject.SetActive(false);
    }

    void AttachPlayerIfOnPlatform(GameObject platform)
    {
        if (characterMovement == null) return;

        Transform playerTransform = characterMovement.transform;
        Ray ray = new Ray(playerTransform.position + Vector3.up * 0.1f, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.5f))
        {
            if (hit.collider.gameObject == platform)
            {
                playerTransform.SetParent(platform.transform);
            }
        }
    }

    void ShowPlatformDirectionIndicators(GameObject platform)
    {
        if (directionPanel != null)
            directionPanel.SetActive(true);

        PlatformMovementLimits limits = platform.GetComponent<PlatformMovementLimits>();

        SetArrowState(arrowUp, limits != null && limits.allowUp);
        SetArrowState(arrowDown, limits != null && limits.allowDown);
        SetArrowState(arrowLeft, limits != null && limits.allowLeft);
        SetArrowState(arrowRight, limits != null && limits.allowRight);
    }

    void SetArrowState(GameObject arrow, bool isEnabled)
    {
        if (arrow != null)
        {
            arrow.SetActive(true);
            Image img = arrow.GetComponent<Image>();
            if (img != null)
                img.color = isEnabled ? enabledColor : disabledColor;
        }
    }
    void HandleStunSpell()
    {
        if (Input.GetKeyDown(KeyCode.R) && Time.time >= lastStunTime + stunCooldown)
        {
            CastStun();
            lastStunTime = Time.time;

            if (stunIcon != null)
                StartCoroutine(SetStunIconCooldown(stunCooldown));
        }
    }

    void HideAllDirectionIndicators()
    {
        if (arrowUp != null) arrowUp.SetActive(false);
        if (arrowDown != null) arrowDown.SetActive(false);
        if (arrowLeft != null) arrowLeft.SetActive(false);
        if (arrowRight != null) arrowRight.SetActive(false);
        if (directionPanel != null)
            directionPanel.SetActive(false);
    }
}
