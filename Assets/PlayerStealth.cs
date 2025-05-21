using UnityEngine;

public class PlayerStealth : MonoBehaviour
{
    public GameObject characterModel; // ← buraya "export_dbe..." objesini at

    private CharacterMovement3D movementScript;
    private bool isHidden = false;
    private bool canExit = false;

    void Start()
    {
        movementScript = GetComponent<CharacterMovement3D>();
    }

    void Update()
    {
        if (isHidden && canExit && Input.GetKeyDown(KeyCode.E))
        {
            ExitHiding();
        }
    }

    public void EnterHiding()
    {
        isHidden = true;
        canExit = true;

        characterModel.SetActive(false); // kız modelini gizle
        movementScript.enabled = false;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        Debug.Log("Oyuncu GİZLENDİ! 'E' tuşuna basarak çıkabilirsin.");
    }

    public void ExitHiding()
    {
        isHidden = false;
        canExit = false;

        characterModel.SetActive(true); // tekrar görünür yap
        movementScript.enabled = true;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

        Debug.Log("Oyuncu ÇIKTI ve tekrar göründü!");
    }

    public bool IsHidden()
    {
        return isHidden;
    }
}
