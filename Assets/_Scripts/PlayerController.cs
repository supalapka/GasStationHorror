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


    [SerializeField] Camera playerCamera;    // Камера игрока
    private float currentXRotation = 0f;  // Текущий угол поворота по оси X

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Считываем данные для движения
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Считываем данные для вращения
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
        moveDirection.y = 0f;  // Отключаем движение по оси Y

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);


        if (!characterController.isGrounded)
            velocity.y += -20f * Time.deltaTime;
        else
            velocity.y = 0f; 
        characterController.Move(velocity * Time.deltaTime);
    }

    // Обработка вращения
    private void Look()
    {
        // Вращаем камеру по горизонтали (влево/вправо)
        float rotationX = lookInput.x * mouseSensitivity;

        transform.Rotate(Vector3.up * rotationX);

        // Вращаем камеру по вертикали (вверх/вниз)
        float rotationY = lookInput.y * mouseSensitivity;
        currentXRotation -= rotationY;
        currentXRotation = Mathf.Clamp(currentXRotation, -verticalLookLimit, verticalLookLimit); // Ограничиваем угол вращения

        playerCamera.transform.localRotation = Quaternion.Euler(currentXRotation, 0f, 0f); // Применяем ограничение к вертикальному повороту
    }
}
