using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float rotationSpeed = 2f;

    public Transform cameraTransform;

    private float yRotation;
    private CharacterController controller;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        yRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        // Girar 180° con botón 
        if (Input.GetKeyDown(KeyCode.F))
        {
            yRotation += 180f;
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Movimiento relativo a la cámara
        Vector3 move = cameraTransform.forward * input.z + cameraTransform.right * input.x;
        move.y = 0;
        float speedMultiplier = Input.GetKey(KeyCode.LeftShift) ? 10f : 1f;
        controller.Move(move.normalized * moveSpeed * speedMultiplier * Time.deltaTime);

        // Rotación con el mouse horizontal
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        yRotation += mouseX;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        // Animaciones
        bool isWalkingBackwards = input.z < -0.1f;
        bool isWalkingForward = input.z > 0.1f;

        animator.SetBool("isBackwards", isWalkingBackwards);
        animator.SetBool("isWalking", isWalkingForward);
    }
}


