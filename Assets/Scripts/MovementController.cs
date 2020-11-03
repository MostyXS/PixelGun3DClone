using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float sensivityX = 400f;
    [SerializeField] float sensivityY = 200f;

    [SerializeField] Transform cam = null;


    [SerializeField] float speed = 5f;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float jumpHeight = 3f;

    [SerializeField] float distanceToGround = .5f;
    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    CharacterController controller;

    float xRotation;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MouseLook();
        if (velocity.y < 0 && IsGrounded())
        {
            velocity.y = -2f;
        }
        Movement();
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        Gravity();
    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensivityY * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(mouseX * Vector2.up);
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z);
        controller.Move(move *speed * Time.deltaTime);
    }
    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down * distanceToGround, out hit, distanceToGround, groundMask);
        return hit.collider;
    }

}
