using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputManager inputManager;
    PlayerMovement playerMovement;
    Animator animator;
    public Transform camera;

    public bool isInteracting;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        inputManager.HandleAllInputs();
        //transform.rotation = Quaternion.Euler(0f, camera.eulerAngles.y, 0f);
        if (!animator.GetBool("dance") && !animator.GetBool("isAiming")) transform.rotation = Quaternion.Euler(0f, camera.eulerAngles.y, 0f);

    }
    private void FixedUpdate()
    {
        playerMovement.HandleAllMovement();
    }
    private void LateUpdate()
    {
        isInteracting = animator.GetBool("isInteracting");
        playerMovement.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerMovement.isGrounded);
    }
}
