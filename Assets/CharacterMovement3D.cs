using UnityEngine;

public class CharacterMovement3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f; // ��melme h�z�
    public float jumpForce = 10f;
    public float gravity = 20f;
    private float originalHeight;
    public float crouchHeight = 1f; // ��melince boy

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isCrouching = false;

    public bool isIsometric = false; // �zometrik kontrol aktif mi?

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height; // Oyuncunun orijinal y�ksekli�ini kaydet
    }

    void Update()
    {
        if (isIsometric)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Vector3 input = new Vector3(moveX, 0, moveZ).normalized;

            // Kamera y�n�ne g�re input'u d�nd�r
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            // Y d�zleminde d�zle�tir
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 move = camForward * input.z + camRight * input.x;
            move = move.normalized * (isCrouching ? crouchSpeed : moveSpeed);

            moveDirection.x = move.x;
            moveDirection.z = move.z;
        }

        else
        {
            // 2.5D mod: sadece X ekseninde hareket
            float moveInput = Input.GetAxis("Horizontal");
            moveDirection.x = moveInput * (isCrouching ? crouchSpeed : moveSpeed);
            moveDirection.z = 0; // Z ekseni kilitli
        }

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump") && !isCrouching) // ��melirken z�playamaz
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime; // Yer�ekimi
        }

        controller.Move(moveDirection * Time.deltaTime);

        HandleCrouch(); // ��melme fonksiyonunu �a��r
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) // "CTRL" tu�una bas�nca ��mel
        {
            isCrouching = true;
            controller.height = crouchHeight; // Boyu k���lt
            Debug.Log("Oyuncu ��meldi!");
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)) // "CTRL" b�rak�l�nca normale d�n
        {
            isCrouching = false;
            controller.height = originalHeight; // Boyu eski haline getir
            Debug.Log("Oyuncu ��melmeyi b�rakt�!");
        }
    }

    public void TeleportTo(Vector3 newPosition)
    {
        controller.enabled = false;
        transform.position = newPosition;
        controller.enabled = true;


        // Kamera de�i�tirme
        FindFirstObjectByType<CameraSwitch>().SwitchToIsometric();
    }
}
