using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementControl : MonoBehaviour
{
	private PlayerControlInput _playerControlInput;

	private Vector2 _playerMovementInput;
	private Vector3 _playerMovement;
	private Vector3 _playerSprintMovement;
	private bool _isMovementKeyPressed;
	private bool _isSprintKeyPressed;
	private bool _isCrouchKeyPressed;
	
	private CharacterController _player;

	private Animator _animator;
	private static readonly int IsRunningHash = Animator.StringToHash("isRunning");
	private static readonly int IsSprintingHash = Animator.StringToHash("isSprinting");
	private static readonly int IsCrouching = Animator.StringToHash("isCrouching");
	private static readonly int IsCrouchWalking = Animator.StringToHash("isCrouchWalking");
	private static readonly int IsCrouchingTrigger = Animator.StringToHash("isCrouchingTrigger");
	private static readonly int IsStanding = Animator.StringToHash("isStanding");
	private bool _isRunning;
	private bool _isSprinting;
	private bool _isCrouching;
	private bool _isCrouchWalking;

	private int _revertAnimInt;

	[SerializeField] private float rotationSpeed = 0.1f;
	[SerializeField] private float movementSpeed = 1.5f;
	[SerializeField] private float sprintingSpeed = 3f;
	
	[SerializeField] private float groundedGravity = 0.1f;
	[SerializeField] private float gravity = -9.8f;

	private void Awake()
	{
		_playerControlInput = new PlayerControlInput();
		_player = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();

		_playerControlInput.PlayerControl.Movement.started += OnMovementInput;
		_playerControlInput.PlayerControl.Movement.canceled += OnMovementInput;
		_playerControlInput.PlayerControl.Movement.performed += OnMovementInput;
		_playerControlInput.PlayerControl.Sprint.started += OnRun;
		_playerControlInput.PlayerControl.Sprint.canceled += OnRun;
		_playerControlInput.PlayerControl.Crouching.started += OnCrouch;
		_playerControlInput.PlayerControl.Crouching.canceled += OnCrouch;
	}

	private void OnEnable() => _playerControlInput.PlayerControl.Enable();

	private void Update()
	{
		HandlePlayerRotation();
		HandleAnimations();
		HandleGravity();

		if (_isSprintKeyPressed) 
			_player.Move(_playerSprintMovement * Time.deltaTime);
		else
			_player.Move(_playerMovement * Time.deltaTime);
	}

	private void OnMovementInput(InputAction.CallbackContext context)
	{
		_playerMovementInput = context.ReadValue<Vector2>();
		_playerMovement.x = _playerMovementInput.x * movementSpeed;
		_playerMovement.z = _playerMovementInput.y * movementSpeed;
		_playerSprintMovement.x = _playerMovementInput.x * sprintingSpeed;
		_playerSprintMovement.z = _playerMovementInput.y * sprintingSpeed;
		_isMovementKeyPressed = _playerMovement.x != 0 || _playerMovement.z != 0;
	}

	private void OnRun(InputAction.CallbackContext context) => _isSprintKeyPressed = context.ReadValueAsButton();

	private void OnCrouch(InputAction.CallbackContext context)
	{
		
		_isCrouchKeyPressed = context.ReadValueAsButton();
		print(context.ReadValueAsButton());
	}

	private void HandlePlayerRotation()
	{
		Vector3 positionToLookAt;
		
		positionToLookAt.x = _playerMovement.x;
		positionToLookAt.y = 0f;
		positionToLookAt.z = _playerMovement.z;
		
		var currentRotation = transform.rotation;

		if (!_isMovementKeyPressed) return;
		
		var targetRotation = Quaternion.LookRotation(positionToLookAt);
		transform.rotation = Quaternion.Slerp(currentRotation,targetRotation,rotationSpeed * Time.deltaTime);
	}

	private void HandleAnimations()
	{
		_isRunning = _animator.GetBool(IsRunningHash);
		_isSprinting = _animator.GetBool(IsSprintingHash);
		_isCrouching = _animator.GetBool(IsCrouching);
		_isCrouchWalking = _animator.GetBool(IsCrouchWalking);

		if(_isMovementKeyPressed && !_isRunning)
			_animator.SetBool(IsRunningHash, true);
		else if(!_isMovementKeyPressed && _isRunning)
			_animator.SetBool(IsRunningHash,false);
		
		if((_isMovementKeyPressed && _isSprintKeyPressed) && !_isSprinting)
			_animator.SetBool(IsSprintingHash,true);
		else if((!_isMovementKeyPressed || !_isSprintKeyPressed) && _isRunning)
			_animator.SetBool(IsSprintingHash,false);

		if (_isCrouchKeyPressed && !_isCrouching)
		{
			_animator.SetBool(IsCrouching,true);
			_revertAnimInt = 1;
		}
		else if ((_revertAnimInt == 1 && _isCrouching) && !_isCrouchKeyPressed)
		{
			_animator.SetBool(IsCrouching,false);
			_revertAnimInt = 0;
		}
		
		
		/*if (_revertAnimInt == 1 && _isCrouchKeyPressed)
		{
			_animator.SetTrigger(IsStanding);
			_revertAnimInt = 0;
		}
		else if (_isCrouchKeyPressed)
		{
			_animator.SetTrigger(IsCrouchingTrigger);
			_revertAnimInt = 1;
			print(_isCrouchKeyPressed);
			print(_revertAnimInt);
		}
		*/

		
	}

	private void HandleGravity()
	{
		if (_player.isGrounded)
		{
			_playerMovement.y = groundedGravity;
			_playerSprintMovement.y = groundedGravity;
		}
		else
		{
			_playerMovement.y = gravity;
			_playerSprintMovement.y = gravity;
		}
	}
	
	private void OnDisable() => _playerControlInput.PlayerControl.Disable();
}
