using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
	private Animator _animator;

	private static readonly int IsWalking = Animator.StringToHash("isWalking");
	private static readonly int IsRunning = Animator.StringToHash("isRunning");
	
	private void Start() => _animator = GetComponent<Animator>();

	private void Update()
	{
		bool isWalking = _animator.GetBool(IsWalking);
		bool isForwardPressed = Input.GetKey(KeyCode.W);
		bool isRunPressed = Input.GetKey("left shift");
		
		if (!isWalking && isForwardPressed)
		{
			_animator.SetBool(IsWalking, true);
		}

		if (isForwardPressed && isRunPressed)
		{
			_animator.SetBool(IsRunning,true);
		}
		
		if (isWalking && !isForwardPressed)
		{
			_animator.SetBool(IsWalking,false);
		}	
		if (!isForwardPressed || !isRunPressed)
		{
			_animator.SetBool(IsRunning,false);
		}
	}
}
