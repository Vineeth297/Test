using UnityEngine;

public class YBotWalkState : YBotBaseState
{
	private bool _isAtDestination;
	private Transform _currentTarget;
	private Vector3 _direction;
	
	public override void EnterState(YBotStateMachine yBot)
	{
		yBot.animator.SetBool(WalkingHash, true);
		yBot.activityDelayTime = 0f;
	}

	public override void UpdateState(YBotStateMachine yBot)
	{
		if (!_isAtDestination)
		{
			_currentTarget = yBot.activityLocation;
			MoveThePlayer(_currentTarget,yBot);
		}
		else
		{
			_currentTarget = yBot.startPosition;
			MoveThePlayer(_currentTarget,yBot);
		}
	}

	private void MoveThePlayer(Transform destination,YBotStateMachine yBot)
	{
		_direction = destination.position - yBot.transform.position;
		if (Vector3.Distance(yBot.transform.position, destination.position) > 1f)
			yBot.player.Move(_direction * (yBot.movementSpeed * Time.deltaTime));
		else
		{
			_isAtDestination = !_isAtDestination;
			yBot.SwitchState(yBot.workState);
		}
	}
}
