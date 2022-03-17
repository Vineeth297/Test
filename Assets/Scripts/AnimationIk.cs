using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimationIk : MonoBehaviour
{

	[SerializeField] private GameObject target;
	[SerializeField] private Animator animator;
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnAnimatorIK()
	{
		animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
		animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);
		animator.SetIKPosition(AvatarIKGoal.RightHand,target.transform.localPosition);
	}
	
}
