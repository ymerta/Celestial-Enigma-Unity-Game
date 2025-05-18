using UnityEngine;

public class CharacterMovement3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;
    public float jumpForce = 10f;
    public float gravity = 20f;
    private float originalHeight;
    public float crouchHeight = 1f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isCrouching = false;

    public bool isIsometric = false;

    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        animator = GetComponentInChildren<Animator>(); // Animator, "kiz" alt objesinde ise
    }

    void Update()
    {
        if (isIsometric)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Vector3 input = new Vector3(moveX, 0, moveZ).normalized;

            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 move = camForward * input.z + camRight * input.x;
            move = move.normalized * (isCrouching ? crouchSpeed : moveSpeed);

            moveDirection.x = move.x;
            moveDirection.z = move.z;

            // Yürüyorsa animasyonu aç
            bool isWalking = moveDirection.x != 0 || moveDirection.z != 0;
            animator.SetBool("isWalking", isWalking);
        }
        else
        {
            float moveInput = Input.GetAxis("Horizontal");
            moveDirection.x = moveInput * (isCrouching ? crouchSpeed : moveSpeed);
            moveDirection.z = 0;

            // 2.5D yürüyüþ animasyonu
            bool isWalking = Mathf.Abs(moveDirection.x) > 0.01f;
            animator.SetBool("isWalking", isWalking);
        }

        if (controller.isGrounded)
        {
            animator.SetBool("isJumping", false); // Yerdeyse zýplama kapanýr

            if (Input.GetButtonDown("Jump") && !isCrouching)
            {
                moveDirection.y = jumpForce;
                animator.SetBool("isJumping", true); // Zýplama animasyonu baþlat
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);

        HandleCrouch();
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            controller.height = crouchHeight;
            animator.SetBool("isCrouching", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            controller.height = originalHeight;
            animator.SetBool("isCrouching", false);
        }
    }

    public void TeleportTo(Vector3 newPosition)
    {
        controller.enabled = false;
        transform.position = newPosition;
        controller.enabled = true;

        FindFirstObjectByType<CameraSwitch>().SwitchToIsometric();
    }
}
