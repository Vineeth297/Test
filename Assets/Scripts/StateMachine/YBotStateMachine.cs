using UnityEngine;

public class YBotStateMachine : MonoBehaviour
{
	private YBotBaseState _currentState;
	
	public YBotIdleState idleState = new YBotIdleState();
	public YBotWalkState walkState = new YBotWalkState();
	public YBotWorkState workState = new YBotWorkState();
	
	[HideInInspector]
	public Animator animator;

	public float activityDelayTime = 0f;

	public CharacterController player;
	public float movementSpeed = 1f;

	public Transform activityLocation;
	public Transform startPosition;

	private void Start()
	{
		_currentState = idleState;
		_currentState.EnterState(this);

		animator = GetComponent<Animator>();

		player = GetComponent<CharacterController>();
	}

	private void Update() => _currentState.UpdateState(this);

	public void SwitchState(YBotBaseState state)
	{
		_currentState = state;
		state.EnterState(this);
	}
}
