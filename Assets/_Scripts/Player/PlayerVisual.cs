using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisual : MonoBehaviour, IPlayerInteractAnimation
{
    [SerializeField] private Animator animator;
    private const string IS_HIT = "IsHit";
    public event EventHandler OnInteractAnimation;
    public event EventHandler OnEndInteractAnimation;



    public void StartAnimation()
    {
        animator.SetTrigger(IS_HIT);
    }
    public void InteractAnimationEvent()
    {

        OnInteractAnimation?.Invoke(this, EventArgs.Empty);
    }
    public void EndInteractAnimationEvent()
    {
        OnEndInteractAnimation?.Invoke(this, EventArgs.Empty);
    }
}
