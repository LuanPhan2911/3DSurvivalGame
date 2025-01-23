using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 50f;

    private float xRotation = 0f;
    private float YRotation = 0f;

    public static MouseMovement Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private bool isLookAround = true;

    private void Start()
    {
        //Locking the cursor to the middle of the screen and making it invisible
        EnableLookAround();
    }

    public void EnableLookAround()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isLookAround = true;
    }
    public void DisabledLookAround()
    {
        Cursor.lockState = CursorLockMode.None;
        isLookAround = false;
    }

    private void Update()
    {

        HandleLookAround();
    }
    private void HandleLookAround()
    {
        if (!isLookAround)
        {
            return;
        }
        Vector2 inputVector = GameInput.Instance.GetLookAroundVector();
        float mouseX = inputVector.x * mouseSensitivity * Time.deltaTime;
        float mouseY = inputVector.y * mouseSensitivity * Time.deltaTime;

        //control rotation around x axis (Look up and down)
        xRotation -= mouseY;

        //we clamp the rotation so we cant Over-rotate (like in real life)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //control rotation around y axis (Look up and down)
        YRotation += mouseX;

        //applying both rotations
        transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
    }
}
