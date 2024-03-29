using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    AgenteP _plControls;
    PlayerMovement _playerMovement;
    AnimationManager animationManager;

    public Vector2 movementInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool sprintInput;
    public bool jumpInput;
    public bool danceInput;
    public bool crouchInput;
    public bool aimInput;
    public bool attackInput;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        animationManager = GetComponent<AnimationManager>();
    }

    private void OnEnable()
    {
        if (_plControls == null)
        {
            _plControls = new AgenteP();
            _plControls.Player.Move.performed += i => movementInput = i.ReadValue<Vector2>();

            //Run button event
            _plControls.Player.Run.performed += i => sprintInput = true;
            _plControls.Player.Run.canceled += i => sprintInput = false;
            //Jump button event
            _plControls.Player.Jump.performed += i => jumpInput = true;
            //dance button event
            _plControls.Player.Dance.performed += i => danceInput = true;
            _plControls.Player.Dance.canceled += i => danceInput = false;
            //Crouch button input
            _plControls.Player.Crouch.performed += i => crouchInput = true;
            _plControls.Player.Crouch.canceled += i => crouchInput = false;
            //Aim button input
            _plControls.Player.Apuntar.performed += i => aimInput = true;
            _plControls.Player.Apuntar.canceled += i => aimInput = false;
            //Attack button input
            _plControls.Player.Attack.performed += i => attackInput = true;
            _plControls.Player.Attack.canceled += i => attackInput = false;

        }
        _plControls.Enable();
    }
    private void OnDisable()
    {
        _plControls.Disable();
    }
    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleCrouchingInput();
        HandleJumpingInput();
        HandleDanceInput();
        HandleAimInput();
        HandleAttackInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y; 
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animationManager.UpdateAnimatorValues(0, moveAmount,_playerMovement.isSprinting,_playerMovement.isCrouching);
    }
    private void HandleSprintingInput()
    {
        if (sprintInput && moveAmount > 0.5) _playerMovement.isSprinting = true;
        else _playerMovement.isSprinting = false;
    }
    private void HandleCrouchingInput()
    {
        if (crouchInput) _playerMovement.isCrouching = true;
        else _playerMovement.isCrouching = false;
    }
    private void HandleAimInput()
    {
        if (aimInput) _playerMovement.isAiming = true;
        else _playerMovement.isAiming = false;
    }
    private void HandleAttackInput()
    {
        if (attackInput) _playerMovement.isAttacking = true;
        else _playerMovement.isAttacking = false;
    }
    private void HandleJumpingInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            _playerMovement.HandleJump();
        }
    }

    private void HandleDanceInput()
    {
        if (danceInput)
        {
            animationManager.animator.SetBool("dance", true);
            if (animationManager.animator.GetLayerWeight(1) == 1)
            {
                animationManager.animator.SetLayerWeight(1, 0);
                StartCoroutine(ReturnLayer());
            }
            animationManager.PlayTargetAnimation("Baile", danceInput);
        }
        else animationManager.animator.SetBool("dance", false);
    }
    IEnumerator ReturnLayer()
    {
        yield return new WaitForSeconds(2);
        animationManager.animator.SetLayerWeight(1, 1);
    }

}
