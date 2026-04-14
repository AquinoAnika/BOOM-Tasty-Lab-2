using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class Playermovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float crouchSpeed = 3f;
    private float defaultWalkSpeed, defaultRunSpeed;

    [Header("Stamina System")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDrain = 20f;
    [SerializeField] private float staminaRegen = 15f;
    [SerializeField] private Image staminaBarFill;
    private float currentStamina;

    [Header("Physics & Camera")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float lookXLimit = 45f;
    [SerializeField] private float defaultHeight = 2f;
    [SerializeField] private float crouchHeight = 1f;

    [Header("Unlimited Ability System")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwCooldown = 0.4f; // Time between shots
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private GameObject inventoryIcon;

    private bool hasUnlockedThrowable = false; // Permanent unlock
    private float nextThrowTime = 0f;
    private int totalScore = 0;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        defaultWalkSpeed = walkSpeed;
        defaultRunSpeed = runSpeed;
        currentStamina = maxStamina;

        UpdateUI();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleStamina();
        HandleMovement();
        HandleRotation();
        HandleCrouch();

        // Handle Throwing: Checks if unlocked and if cooldown has passed
        if (Input.GetMouseButton(0) && hasUnlockedThrowable && Time.time >= nextThrowTime)
        {
            ThrowObject();
            nextThrowTime = Time.time + throwCooldown;
        }
    }

    private void HandleStamina()
    {
        bool isMoving = characterController.velocity.magnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving && currentStamina > 0;

        if (isRunning) currentStamina -= staminaDrain * Time.deltaTime;
        else if (currentStamina < maxStamina) currentStamina += staminaRegen * Time.deltaTime;

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        if (staminaBarFill != null) staminaBarFill.fillAmount = currentStamina / maxStamina;
    }

    private void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && currentStamina > 5f;

        float curSpeedX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && characterController.isGrounded) moveDirection.y = jumpPower;
        else moveDirection.y = movementDirectionY;

        if (!characterController.isGrounded) moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleRotation()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.R))
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = defaultWalkSpeed;
            runSpeed = defaultRunSpeed;
        }
    }

    void ThrowObject()
    {
        GameObject proj = Instantiate(projectilePrefab, throwPoint.position, throwPoint.rotation);

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint = Physics.Raycast(ray, out RaycastHit hit) ? hit.point : ray.GetPoint(75);
        Vector3 direction = (targetPoint - throwPoint.position).normalized;

        if (proj.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(direction * 20f, ForceMode.VelocityChange);
        }
    }

    public void UnlockThrowable()
    {
        hasUnlockedThrowable = true;
        totalScore++; // Still increment score for picking up the "power up"
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + totalScore;

        if (statusText != null)
            statusText.text = hasUnlockedThrowable ? "Ammo: Infinite" : "Find an item!";

        if (inventoryIcon != null)
            inventoryIcon.SetActive(hasUnlockedThrowable);
    }
}