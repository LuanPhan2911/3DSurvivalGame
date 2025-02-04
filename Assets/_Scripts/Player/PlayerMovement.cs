using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private float speed = 8f;
    private float defaultSpeed;
    [SerializeField] private float sprintSpeed = 16f;
    [SerializeField] private float overweightSpeed = 2f;
    [SerializeField] private float midOverWeightSpeed = 4f;
    [SerializeField] private float gravity = -9.81f * 2;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float jumHeightWhenMidOverweight = 1.5f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float staminaConsumeWhenMidOverweight = -20f;
    [SerializeField] private float staminaConsume = -10f;
    [SerializeField] private float staminaRestoreWhenWalking = 5f;
    [SerializeField] private float staminaRestore = 10f;

    private Vector3 velocity;

    private bool isGrounded;
    private bool isWalking;
    private bool isSprinting;
    private float delayRestoreStaminaTimerMax = 3f;
    private float delayRestoreStaminaTimer;


    private void Awake()
    {
        defaultSpeed = speed;
    }

    private void Start()
    {
        InvokeRepeating(nameof(RestoreStamina), 1f, 1f);
    }
    private void OnDestroy()
    {
        CancelInvoke();
    }

    private void RestoreStamina()
    {
        if (!PlayerStatus.Instance.IsStaminaFull() && !isSprinting)
        {
            if (delayRestoreStaminaTimer > 0f)
            {
                return;
            }
            if (isWalking)
            {
                PlayerStatus.Instance.SetStamina(staminaRestoreWhenWalking);
            }
            else
            {
                PlayerStatus.Instance.SetStamina(staminaRestore);
            }
        }

    }

    // Update is called once per frame
    private void Update()
    {
        HandleSprint();
        HandleMovement();
        HandleOverweight();
    }
    private void HandleOverweight()
    {
        if (PlayerStatus.Instance.IsOverweight())
        {
            speed = overweightSpeed;
        }
        else if (PlayerStatus.Instance.IsMidOverweight())
        {
            speed = midOverWeightSpeed;
        }
        else
        {
            speed = defaultSpeed;
        }
    }
    private void HandleSprint()
    {
        if (PlayerStatus.Instance.IsOverweight())
        {
            return;
        }
        if (GameInput.Instance.IsSprintActionPressed())
        {
            if (isWalking && PlayerStatus.Instance.CanSprint())
            {
                delayRestoreStaminaTimer = delayRestoreStaminaTimerMax;
                speed = sprintSpeed;
                if (PlayerStatus.Instance.IsMidOverweight())
                {
                    PlayerStatus.Instance.SetStamina(staminaConsumeWhenMidOverweight * Time.deltaTime);
                }
                else
                {
                    PlayerStatus.Instance.SetStamina(staminaConsume * Time.deltaTime);
                }

                isSprinting = true;
                isWalking = false;
            }
        }
        else
        {
            //restore stamina when not sprinting;
            speed = defaultSpeed;
            isSprinting = false;
            delayRestoreStaminaTimer -= Time.deltaTime;

        }
    }

    private void HandleMovement()
    {
        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        float x = inputVector.x;
        float z = inputVector.y;

        //right is the red Axis, foward is the blue axis
        Vector3 move = transform.right * x + transform.forward * z;
        if (move != Vector3.zero)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        controller.Move(move * speed * Time.deltaTime);

        //check if the player is on the ground so he can jump
        if (IsPlayerCanJump())
        {
            //the equation for jumping
            if (PlayerStatus.Instance.IsMidOverweight())
            {
                velocity.y = Mathf.Sqrt(jumHeightWhenMidOverweight * -2f * gravity);
            }
            else
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    private bool IsPlayerCanJump()
    {
        return GameInput.Instance.IsJumpActionPressed() && isGrounded &&
        !PlayerStatus.Instance.IsOverweight();
    }

    public bool GetIsSprinting()
    {
        return isSprinting;
    }
}
