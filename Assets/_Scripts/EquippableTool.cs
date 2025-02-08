using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippableTool : MonoBehaviour
{
    [SerializeField] private Animator animator;


    private const string IS_HIT = "IsHit";


    public void StartAnimation()
    {

        animator.SetTrigger(IS_HIT);

    }
}
