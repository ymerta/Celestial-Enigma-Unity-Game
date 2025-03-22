using UnityEngine;

public class CharacterMovement3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f; // Çömelme hýzý
    public float jumpForce = 10f;
    public float gravity = 20f;
    private float originalHeight;
    public float crouchHeight = 1f; // Çömelince boy

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isCrouching = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height; // Oyuncunun orijinal yüksekliðini kaydet
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Saða-Sola hareket
        moveDirection.x = moveInput * (isCrouching ? crouchSpeed : moveSpeed); // Eðer çömeliyorsa hýz düþer

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump") && !isCrouching) // Çömelirken zýplayamaz
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime; // Yerçekimi
        }

        moveDirection.z = 0; // Z ekseninde hareketi engelle

        controller.Move(moveDirection * Time.deltaTime);

        HandleCrouch(); // Çömelme fonksiyonunu çaðýr
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) // "CTRL" tuþuna basýnca çömel
        {
            isCrouching = true;
            controller.height = crouchHeight; // Boyu küçült
            Debug.Log("Oyuncu çömeldi!");
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)) // "CTRL" býrakýlýnca normale dön
        {
            isCrouching = false;
            controller.height = originalHeight; // Boyu eski haline getir
            Debug.Log("Oyuncu çömelmeyi býraktý!");
        }
    }
}
