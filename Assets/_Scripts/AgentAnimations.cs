using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object must have a
[RequireComponent(typeof(Animator))]
public class AgentAnimations : MonoBehaviour
{
    protected Animator agentAnimator;

    private void Awake() 
    {
        agentAnimator = GetComponent<Animator>();
    }

    public void SetWalkAnimation (bool isWalking) 
    {
        agentAnimator.SetBool("Walk", isWalking);
    }

    public void AnimatePlayer (float velocity)
    {
        // Passes true or false dependant on if velocity > 0
        SetWalkAnimation(velocity > 0);
    }
}
