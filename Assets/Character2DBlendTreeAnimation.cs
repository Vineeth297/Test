using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DBlendTreeAnimation : MonoBehaviour
{
	private Animator _animator;

	private static readonly int VelocityX = Animator.StringToHash("Velocity X");
	private static readonly int VelocityZ = Animator.StringToHash("Velocity Z");

	public float velocityX;
	public float velocityZ;

	private float _currentWalkVelocity = 0.2f;
	private float _currentRunVelocity = 1f;
	
	public float acceleration = 0.1f;
	public float deceleration = 0.1f;
	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		var isForwardPressed = Input.GetKey(KeyCode.W);
		var isLeftPressed = Input.GetKey(KeyCode.A);
		var isRightPressed = Input.GetKey(KeyCode.D);
		var isRunPressed = Input.GetKey(KeyCode.LeftShift);
		
		print(isLeftPressed && VelocityX > -0.2f && !isRunPressed);

		var currentMaxVelocity = isRunPressed ? _currentRunVelocity : _currentWalkVelocity; 
			
		ChangeVelocity(isForwardPressed, isLeftPressed, isRightPressed, currentMaxVelocity);
		LockOrResetVelocity(isForwardPressed, isLeftPressed, isRightPressed, isRunPressed, currentMaxVelocity);
			
		_animator.SetFloat(VelocityZ,velocityZ);
		_animator.SetFloat(VelocityX,velocityX);
	}

	private void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed,
		float currentMaxVelocity)
	{
		if (forwardPressed && velocityZ < currentMaxVelocity)
			velocityZ += Time.deltaTime * acceleration;
		
		if (leftPressed && velocityX > -currentMaxVelocity)
			velocityX -= Time.deltaTime * acceleration;
		
		if (rightPressed && velocityX < currentMaxVelocity)
			velocityX += Time.deltaTime * acceleration;

		if (!forwardPressed && velocityZ > 0f)
			velocityZ -= Time.deltaTime * deceleration;
		
		if(!leftPressed && velocityX < 0f)
			velocityX += Time.deltaTime * deceleration;
		
		if(!rightPressed && velocityX > 0f)
			velocityX -= Time.deltaTime * deceleration;
	}

	private void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed,
		float currentMaxVelocity)
	{
		if (!forwardPressed && velocityZ < 0f)
			velocityZ = 0f;

		if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
			velocityX = 0f;

		if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
			velocityZ = currentMaxVelocity;
		else if(forwardPressed && velocityZ > currentMaxVelocity)
		{
			velocityZ -= Time.deltaTime * deceleration;
			if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
			{
				velocityZ = currentMaxVelocity;
			}
		}
		else if(forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
		{
			velocityZ = currentMaxVelocity;
		}
		
		//// Left Lock
		if (leftPressed && rightPressed && velocityX < -currentMaxVelocity)
			velocityX = -currentMaxVelocity;
		
		else if(leftPressed && velocityX < -currentMaxVelocity)
		{
			velocityX += Time.deltaTime * deceleration;
			if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
				velocityX = -currentMaxVelocity;
		}
		else if(leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
		{
			velocityX = -currentMaxVelocity;
		}
		
		//// Right Lock
		if (rightPressed && runPressed && velocityX > currentMaxVelocity)
			velocityZ = currentMaxVelocity;
		
		else if(rightPressed && velocityX > currentMaxVelocity)
		{
			velocityX -= Time.deltaTime * deceleration;
			if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
				velocityX = currentMaxVelocity;
		}
		else if(rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
		{
			velocityX = currentMaxVelocity;
		}
	}
}
