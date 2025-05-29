using UnityEngine;

public class PlatformSpellUnlockTrigger : MonoBehaviour
{
    public GameObject spellSlot3Image; // Canvas i�indeki Image (SpellSlot3)

    private void Start()
    {
        if (spellSlot3Image != null)
            spellSlot3Image.SetActive(false); // Ba�ta g�r�nmesin
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (spellSlot3Image != null)
                spellSlot3Image.SetActive(true); // Oyuncu girince aktif et
        }
    }
}
