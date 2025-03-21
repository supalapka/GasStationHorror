using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;

    private Vector2 moveInput;
    private Vector2 lookInput;

    public float moveSpeed = 5f;
    public float mouseSensitivity = 0.15f;
    public float verticalLookLimit = 70f;

    private Vector3 velocity;


    [SerializeField] Camera playerCamera;    // ������ ������
    private float currentXRotation = 0f;  // ������� ���� �������� �� ��� X

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // ��������� ������ ��� ��������
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // ��������� ������ ��� ��������
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        moveDirection = playerCamera.transform.TransformDirection(moveDirection);
        moveDirection.y = 0f;  // ��������� �������� �� ��� Y

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);


        if (!characterController.isGrounded)
            velocity.y += -20f * Time.deltaTime;
        else
            velocity.y = 0f; 
        characterController.Move(velocity * Time.deltaTime);
    }

    // ��������� ��������
    private void Look()
    {
        // ������� ������ �� ����������� (�����/������)
        float rotationX = lookInput.x * mouseSensitivity;

        transform.Rotate(Vector3.up * rotationX);

        // ������� ������ �� ��������� (�����/����)
        float rotationY = lookInput.y * mouseSensitivity;
        currentXRotation -= rotationY;
        currentXRotation = Mathf.Clamp(currentXRotation, -verticalLookLimit, verticalLookLimit); // ������������ ���� ��������

        playerCamera.transform.localRotation = Quaternion.Euler(currentXRotation, 0f, 0f); // ��������� ����������� � ������������� ��������
    }
}
