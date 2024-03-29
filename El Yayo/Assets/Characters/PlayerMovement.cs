using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    PlayerController playerController;
    AnimationManager animationManager;
    Animator animator;

    Vector3 moveDirection;
    public Transform cameraObject;
    Rigidbody rb;
    [Header("Falling")]
    public float inAirTimer =  0;
    public float leapingVelocity;
    public float fallingSpeed;
    public float raycastHeightOffset = 0.5f;
    public float maxDistance = 1;
    public LayerMask groundLayer;

    [Header("Movement Speeds")]
    public float walkingSpeed;
    public float speed;
    public float sprintingSpeed;
    public float rotationSpeed;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;

    public bool isSprinting;
    public bool isCrouching;
    public bool isGrounded;
    public bool isJumping;
    public bool isAiming;
    public bool isAttacking;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animationManager = GetComponent<AnimationManager>();
        playerController = GetComponent<PlayerController>();
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
    }
    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        //if (playerController.isInteracting) return;
        HandleMovement();
        HandleRotation();
        HandleAim();
        HandleAttack();
    }
    public void HandleMovement()
    {
        //if (isJumping) return;

        moveDirection = GetDirection();

        if (isCrouching) moveDirection *= (walkingSpeed/4);
        if(isSprinting)
        {
            moveDirection *= sprintingSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f) moveDirection *= speed;
            else moveDirection *= walkingSpeed;
        }

        rb.velocity = moveDirection + Vector3.up * rb.velocity.y;
    }
    public void HandleRotation()
    {
        if (isJumping) return;

        Vector3 targetDirection = GetDirection();

        if (targetDirection == Vector3.zero) targetDirection = transform.forward;

        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed + Time.deltaTime);

        transform.rotation = playerRotation;
    }

    public Vector3 GetDirection()
    {
        Vector3 direction;
        direction = transform.forward * inputManager.verticalInput;
        direction = direction + cameraObject.right * inputManager.horizontalInput;
        direction.Normalize();
        direction.y = 0;
        return direction;
    }
    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        Vector3 targetPosition;
        raycastOrigin.y += raycastHeightOffset;
        targetPosition = transform.position;


        if (!isGrounded)// && !isJumping)
        {
            if (!playerController.isInteracting) animationManager.PlayTargetAnimation("Falling", true); //animator.SetBool("isFalling",true);
            
            inAirTimer += Time.deltaTime;
            rb.AddForce(transform.forward * leapingVelocity);
            rb.AddForce(fallingSpeed * inAirTimer * -Vector3.up);
        }
        else
        {
            inAirTimer = 0;
        }
        
        if(Physics.SphereCast(raycastOrigin,0.5f,-Vector3.up, out hit, maxDistance, groundLayer)) 
        {
            if (!isGrounded && playerController.isInteracting) animationManager.PlayTargetAnimation("Land", true);
            Vector3 raycastHitPoint = hit.point;
            targetPosition.y = raycastHitPoint.y;
            
            inAirTimer = 0;
            isGrounded = true;
        }
        else isGrounded = false;

        if (isGrounded && !isJumping)
        {
            if (playerController.isInteracting || inputManager.moveAmount > 0) transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            else transform.position = targetPosition;
        }
    }
    public void HandleJump()
    {
        if (isGrounded)
        {
            animationManager.animator.SetBool("isJumping", true);
            SoundManagerScript.instance.PlaySFX(SoundManagerScript.instance.jump);

            animationManager.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            rb.velocity = playerVelocity;
        }

    }
    public void HandleAim()
    {
        if (isAiming && GameController.instance.sword)
        {
            animationManager.animator.SetBool("isAiming", true);
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animationManager.animator.SetBool("isAiming", false);
            animator.SetLayerWeight(1, 0);
        }
    }
    public void HandleAttack()
    {
        if (isAiming && isAttacking)
        {
            animationManager.animator.SetBool("isAttacking", true);
            SoundManagerScript.instance.PlaySFX(SoundManagerScript.instance.attack);
        }
        else animationManager.animator.SetBool("isAttacking", false);
    }
    
}