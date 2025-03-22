using UnityEngine;

public class PlayerStealth : MonoBehaviour
{
    private Renderer playerRenderer;
    private CharacterMovement3D movementScript;
    private Collider playerCollider;
    private bool isHidden = false;
    private bool canExit = false;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        movementScript = GetComponent<CharacterMovement3D>();
        playerCollider = GetComponent<Collider>();
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
        playerRenderer.enabled = false; // Oyuncuyu görünmez yap
        movementScript.enabled = false; // Hareketi kapat

        // **NPC'nin oyuncuyla çarpýþmasýný kapat**
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        Debug.Log("Oyuncu GÝZLENDÝ! 'E' tuþuna basarak çýkabilirsin.");
    }

    public void ExitHiding()
    {
        isHidden = false;
        canExit = false;
        playerRenderer.enabled = true; // Oyuncuyu tekrar görünür yap
        movementScript.enabled = true; // Hareketi tekrar aç

        // **NPC'nin oyuncuyla çarpýþmasýný tekrar aç**
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

        Debug.Log("Oyuncu ÇIKTI ve tekrar göründü!");
    }

    public bool IsHidden()
    {
        return isHidden;
    }
}
