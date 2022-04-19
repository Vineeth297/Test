using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
	private PlayerInput _playerInput;

	private Vector2 _currentMovementInput;
	private Vector3 _currentMovement;
	private Vector3 _currentRunMovement;
	private bool _isMovementPressed;
	private bool _isRunPressed;

	private CharacterController _player;

	private Animator _animator;
	private static readonly int WalkingHash = Animator.StringToHash("isWalking");
	private static readonly int RunningHash = Animator.StringToHash("isRunning");
	private bool _isWalking;
	private bool _isRunning;

	[SerializeField] private float sprintSpeed = 3f;
	[SerializeField] private float rotationSpeed = 1f;

	private float _groundedGravity = 0.1f;
	private float _gravity = -9.8f;
	
	private void Awake()
	{
		_playerInput = new PlayerInput();

		_player = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
		
		_playerInput.CharacterControls.Locomotion.started += OnMovementInput;
		_playerInput.CharacterControls.Locomotion.canceled += OnMovementInput;
		_playerInput.CharacterControls.Locomotion.performed += OnMovementInput;
		_playerInput.CharacterControls.Run.started += OnRunInput;
		_playerInput.CharacterControls.Run.canceled += OnRunInput;
	}
    private void Update()
	{
		HandleAnimation();
		HandleRotation();
		HandleGravity();
		
		if (_isRunPressed)
			_player.Move(_currentRunMovement * Time.deltaTime);
		else
			_player.Move(_currentMovement * Time.deltaTime);
	}

	private void OnEnable()
	{
		_playerInput.CharacterControls.Enable();
	}

	private void OnDisable()
	{
		_playerInput.CharacterControls.Disable();
	}

	private void OnMovementInput(InputAction.CallbackContext context)
	{
		_currentMovementInput = context.ReadValue<Vector2>();
		_currentMovement.x = _currentMovementInput.x;
		_currentMovement.z = _currentMovementInput.y;
		_currentRunMovement.x = _currentMovementInput.x * sprintSpeed;
		_currentRunMovement.z = _currentMovementInput.y * sprintSpeed;
		_isMovementPressed = _currentMovement.x != 0 || _currentMovement.z != 0;
	}

	private void OnRunInput(InputAction.CallbackContext context)
	{
		_isRunPressed = context.ReadValueAsButton();
	}
	private void HandleAnimation()
	{
		_isWalking = _animator.GetBool(WalkingHash);
		_isRunning = _animator.GetBool(RunningHash);

		if (_isMovementPressed && !_isWalking)
			_animator.SetBool(WalkingHash, true);
		if(!_isMovementPressed && _isWalking)
			_animator.SetBool(WalkingHash,false);
		if((_isRunPressed && _isMovementPressed) && !_isRunning)
			_animator.SetBool(RunningHash,true);
		if((!_isMovementPressed || !_isRunPressed) && _isRunning)
			_animator.SetBool(RunningHash,false);
	}

	private void HandleRotation()
	{
		Vector3 positionToLookAt;

		positionToLookAt.x = _currentMovement.x;
		positionToLookAt.y = 0f;
		positionToLookAt.z = _currentMovement.z;

		var currentRotation = transform.rotation;

		if (!_isMovementPressed) return;
		
		var targetRotation = Quaternion.LookRotation(positionToLookAt);
		transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	private void HandleGravity()
	{
		if (_player.isGrounded)
		{
			_currentMovement.y = _groundedGravity;
			_currentRunMovement.y = _groundedGravity;
		}
		else
		{
			_currentMovement.y = _gravity;
			_currentRunMovement.y = _gravity;
		}
	}
}
