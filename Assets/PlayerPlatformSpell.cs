using UnityEngine;

public class PlayerPlatformSpell : MonoBehaviour
{
    public float castRange = 10f;  // Büyü menzili
    private PuzzlePlatform selectedPlatform;

    [Header("Spell Activation")]
    public bool hasPlatformSpell = false; // ✅ Başta kilitli, NPC verince açılacak

    void Update()
    {
        if (!hasPlatformSpell) return; // 🔒 Büyü henüz açılmadıysa hiçbir şey yapma

        // 🔹 F tuşuna basıldığında ray gönder, platform seç
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, castRange))
            {
                PuzzlePlatform platform = hit.collider.GetComponent<PuzzlePlatform>();
                if (platform != null)
                {
                    selectedPlatform = platform;
                    Debug.Log("✅ Platform selected: " + platform.name);
                }
                else
                {
                    Debug.Log("❌ Seçilen nesne bir platform değil.");
                }
            }
        }

        // 🔹 Seçilen platformu hareket ettirme
        if (selectedPlatform != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
                selectedPlatform.MoveInDirection(Vector3.forward);
            else if (Input.GetKeyDown(KeyCode.S))
                selectedPlatform.MoveInDirection(Vector3.back);
            else if (Input.GetKeyDown(KeyCode.A))
                selectedPlatform.MoveInDirection(Vector3.left);
            else if (Input.GetKeyDown(KeyCode.D))
                selectedPlatform.MoveInDirection(Vector3.right);
            else if (Input.GetKeyDown(KeyCode.Space))
                selectedPlatform.MoveInDirection(Vector3.up);
            else if (Input.GetKeyDown(KeyCode.LeftControl))
                selectedPlatform.MoveInDirection(Vector3.down);
        }
    }
}
