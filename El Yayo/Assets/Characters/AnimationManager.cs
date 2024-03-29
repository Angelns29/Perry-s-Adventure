using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");

    }
    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }
    public void UpdateAnimatorValues(float horizontalMove, float verticalMove, bool isSprinting, bool isCrouching)
    {
        float snappedHorizontal;
        float snappedVertical;

        //Snapped Horizontal
        if (horizontalMove > 0 && horizontalMove < 0.55f) snappedHorizontal = 0.5f;
        else if (horizontalMove > 0.55f) snappedHorizontal = 1;
        else if (horizontalMove < 0 && horizontalMove > -0.55f) snappedHorizontal = -0.5f;
        else if (horizontalMove < -0.55f) snappedHorizontal = -1;
        else snappedHorizontal = 0;

        //Snapped Vertical
        if (verticalMove > 0 && verticalMove < 0.55f) snappedVertical = 0.5f;
        else if (verticalMove > 0.55f) snappedVertical = 1;
        else if (verticalMove < 0 && verticalMove > -0.55f) snappedVertical = -0.5f;
        else if (verticalMove < -0.55f) snappedVertical = -1;
        else snappedVertical = 0;

        if (isSprinting)
        {
            snappedHorizontal = horizontalMove;
            snappedVertical = 2;
        }
        if (isCrouching)
        {
            snappedHorizontal = horizontalMove;
            snappedVertical = -1;
        }
        animator.SetFloat(horizontal, snappedHorizontal, 0.1f,Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);

    }
}
